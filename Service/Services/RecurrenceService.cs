using Core.Entities;
using Core.Exceptions;
using Core.Extensions;
using Core.Interfaces.IServices;

namespace Core.Services;

public class RecurrenceService : IRecurrenceService
{
    public List<DateOnly> GetOccurrencesOfEvent(RecurrencePattern recurrencePattern)
    {
        if (recurrencePattern is null)
            throw new InvalidRecurrencePatternException("Recurrence pattern is null !");

        return recurrencePattern.IsNonRecurrenceEvent()
               ? [recurrencePattern.StartDate]
               : GetOccurrencesOfEventUsingFrequency(recurrencePattern);
    }

    private List<DateOnly> GetOccurrencesOfEventUsingFrequency(RecurrencePattern recurrencePattern)
    {
        if (recurrencePattern.IsDailyEvent())
            return GetOccurrenceOfDailyEvents(recurrencePattern);
        else if (recurrencePattern.IsWeeklyEvent())
            return GetOccurrencesOfWeeklyEvents(recurrencePattern);
        else if (recurrencePattern.IsMonthlyEvent())
            return GetOccurrencesOfMonthlyEvents(recurrencePattern);
        else
            return GetOccurrencesOfYearlyEvents(recurrencePattern);
    }

    private List<DateOnly> GetOccurrenceOfDailyEvents(RecurrencePattern recurrencePattern)
    {
        List<int> days = [.. recurrencePattern.ByWeekDay ?? ([])];

        DateOnly startDateOfEvent = recurrencePattern.StartDate;
        DateOnly endDateOfEvent = recurrencePattern.EndDate;
        int interval = recurrencePattern.Interval;

        TimeSpan difference = endDateOfEvent.ConvertToDateTime()
                            - startDateOfEvent.ConvertToDateTime();

        int totalOccurrences = ((int)difference.TotalDays / interval) + 1;

        return [..Enumerable.Range(0, totalOccurrences)
                            .Select(weekOffset => startDateOfEvent.AddDays(weekOffset * interval))
                            .Where(date => IsValidDateForDailyEvent(date, days, startDateOfEvent, endDateOfEvent))];
    }

    private bool IsValidDateForDailyEvent(DateOnly date,
                                          List<int> days,
                                          DateOnly startDateOfEvent,
                                          DateOnly endDateOfEvent)
    {
        return date.IsDateInRange(startDateOfEvent, endDateOfEvent)
               && (days.Count == 0 || days.Contains(date.GetDayNumberFromWeekDay()));
    }

    private List<DateOnly> GetOccurrencesOfWeeklyEvents(RecurrencePattern recurrencePattern)
    {
        List<DateOnly> occurrences = [];

        List<int> weekDays = [.. recurrencePattern.ByWeekDay ?? ([])];

        DateOnly startDateOfWeek = recurrencePattern.StartDate
                                   .GetStartDateOfWeek();

        foreach (var item in weekDays)
        {
            occurrences = [.. occurrences.Concat(GetOccurrencesOfWeekDay(recurrencePattern, startDateOfWeek, item))];
        }

        occurrences.Sort();

        return occurrences;
    }

    private List<DateOnly> GetOccurrencesOfWeekDay(RecurrencePattern recurrencePattern, DateOnly startDateOfWeek, int item)
    {
        DateOnly startDateOfEvent = recurrencePattern.StartDate;
        DateOnly endDateOfEvent = recurrencePattern.EndDate;

        DateOnly startDateForSpecificWeekday = startDateOfWeek.AddDays(item - 1);

        TimeSpan difference = endDateOfEvent.ConvertToDateTime()
                            - startDateForSpecificWeekday.ConvertToDateTime();

        int interval = recurrencePattern.Interval;

        int totalOccurrences = ((int)difference.TotalDays / 7 * interval) + 1;

        return [..Enumerable.Range(0, totalOccurrences)
                            .Select(weekOffset => startDateForSpecificWeekday.AddDays(7 * weekOffset * interval))
                            .Where(date => date.IsDateInRange(startDateOfEvent,endDateOfEvent))];
    }

    private List<DateOnly> GetOccurrencesOfMonthlyEvents(RecurrencePattern recurrencePattern)
    {
        return recurrencePattern.IsMonthDayNull()
               ? GetOccurrencesOfEventsUsingWeekOrderAndWeekDay(recurrencePattern, true)
               : GetOccurrencesOfEventsUsingMonthDay(recurrencePattern, true);
    }

    private List<DateOnly> GetOccurrencesOfYearlyEvents(RecurrencePattern recurrencePattern)
    {
        return recurrencePattern.IsMonthDayNull()
               ? GetOccurrencesOfEventsUsingWeekOrderAndWeekDay(recurrencePattern, false)
               : GetOccurrencesOfEventsUsingMonthDay(recurrencePattern, false);
    }

    private List<DateOnly> GetOccurrencesOfEventsUsingMonthDay(RecurrencePattern recurrencePattern, bool isMonthly)
    {
        int monthDay = (int)recurrencePattern.ByMonthDay;

        DateOnly startDateOfEvent = recurrencePattern.StartDate;

        int interval = recurrencePattern.Interval;

        int month = isMonthly ? startDateOfEvent.Month : (int)recurrencePattern.ByMonth;

        DateOnly currentDate = new(startDateOfEvent.Year, month, GetMinimumDateFromGivenMonthAndDay(monthDay, month, startDateOfEvent.Year));

        int totalOccurrences = isMonthly
                               ? recurrencePattern.GetMonthlyOccurrencesCount()
                               : recurrencePattern.GetYearlyOccurrencesCount();

        return GetOccurrencesUsingMonthDay(monthDay, interval, currentDate, totalOccurrences, isMonthly);
    }

    private List<DateOnly> GetOccurrencesUsingMonthDay(int monthDay,
                                                       int interval,
                                                       DateOnly currentDate,
                                                       int totalOccurrences,
                                                       bool isMonthly)
    {
        return [..Enumerable.Range(0, totalOccurrences)
                                  .Select(weekOffset =>
                                  {
                                      DateOnly date = isMonthly
                                                      ? currentDate.AddMonths(weekOffset* interval)
                                                      : currentDate.AddYears(weekOffset*interval);
                                      return new DateOnly(date.Year, date.Month, GetMinimumDateFromGivenMonthAndDay(monthDay, date.Month, date.Year));
                                  } )];
    }

    private static int GetMinimumDateFromGivenMonthAndDay(int day, int month, int year)
    {
        int daysInMonth = DateTime.DaysInMonth(year, month);

        return Math.Min(day, daysInMonth);
    }

    private List<DateOnly> GetOccurrencesOfEventsUsingWeekOrderAndWeekDay(RecurrencePattern recurrencePattern, bool isMonthly)
    {
        int weekOrder = (int)recurrencePattern.WeekOrder;

        int weekDay = recurrencePattern.ByWeekDay[0];
        if (weekDay == 7) weekDay = 0;

        DayOfWeek dayOfWeek = (DayOfWeek)weekDay;

        int interval = recurrencePattern.Interval;

        int month = isMonthly
                    ? recurrencePattern.StartDate.Month
                    : (int)recurrencePattern.ByMonth;

        DateOnly currentDate = new(recurrencePattern.StartDate.Year, month, 1);

        int totalOccurrences = isMonthly
                               ? recurrencePattern.GetMonthlyOccurrencesCount()
                               : recurrencePattern.GetYearlyOccurrencesCount();

        return GetOccurrencesUsingWeekOrderAndWeekDay(weekOrder, dayOfWeek, currentDate, interval, totalOccurrences, isMonthly);
    }

    private static List<DateOnly> GetOccurrencesUsingWeekOrderAndWeekDay(int weekOrder,
                                                                         DayOfWeek dayOfWeek,
                                                                         DateOnly currentDate,
                                                                         int interval,
                                                                         int totalOccurrences,
                                                                         bool isMonthly)
    {
        return [..Enumerable.Range(0, totalOccurrences)
                                .Select(weekOffset =>
                                {
                                      DateOnly date = isMonthly
                                                      ? currentDate.AddMonths(weekOffset*interval)
                                                      : currentDate.AddYears(weekOffset* interval);
                                      return GetNthWeekDayDate(date.Year,date.Month,dayOfWeek,weekOrder);
                                } )];
    }

    private static DateOnly GetNthWeekDayDate(int year, int month, DayOfWeek dayOfWeek, int weekOrder)
    {
        DateOnly firstDayOfMonth = new(year, month, 1);

        List<DateOnly> weekDays = [..Enumerable.Range(0, DateTime.DaysInMonth(year, month))
                                                   .Select(firstDayOfMonth.AddDays)
                                                   .GroupBy(date => date.DayOfWeek)
                                                   .First(date => date.Key == dayOfWeek)];

        return weekDays.Count < weekOrder
               ? weekDays[^1]
               : weekDays[weekOrder - 1];
    }
}

