using Core.Domain;
using Core.Interfaces;

namespace Core.Services;

public class RecurrenceService
{
    private readonly IParticipantService _participantService;

    public RecurrenceService(IParticipantService participantService)
    {
        _participantService = participantService;
    }

    public void ScheduleEvents(EventModel eventModel, List<ParticipantModel> participants)
    {
        List<DateOnly> occurrences = GetOccurrencesOfEvent(eventModel);

        foreach (var occurrence in occurrences)
        {
            ScheduleEventsForEachParticipant(eventModel, occurrence, participants);
        }
    }

    public List<DateOnly> GetOccurrencesOfEvent(EventModel eventModel)
    {
        return eventModel.RecurrencePattern.IsNonRecurrenceEvent()
               ? [eventModel.RecurrencePattern.StartDate]
               : GetOccurrencesOfEventUsingFrequency(eventModel);
    }

    private List<DateOnly> GetOccurrencesOfEventUsingFrequency(EventModel eventModel)
    {
        if (eventModel.RecurrencePattern.IsDailyEvent())
            return GetOccurrenceOfDailyEvents(eventModel);
        else if (eventModel.RecurrencePattern.IsWeeklyEvent())
            return GetOccurrencesOfWeeklyEvents(eventModel);
        else if (eventModel.RecurrencePattern.IsMonthlyEvent())
            return GetOccurrencesOfMonthlyEvents(eventModel);
        else
            return GetOccurrencesOfYearlyEvents(eventModel);
    }

    private void ScheduleEventsForEachParticipant(EventModel eventModel,
                                                  DateOnly occurrence,
                                                  List<ParticipantModel> participants)
    {
        foreach (ParticipantModel participant in participants)
        {
            participant.EventDate = occurrence;
            _participantService.AddParticipant(participant, eventModel.Id);
        }

    }

    private List<DateOnly> GetOccurrenceOfDailyEvents(EventModel eventModel)
    {
        List<int> days = [.. eventModel.RecurrencePattern.ByWeekDay ?? ([])];

        DateOnly startDateOfEvent = eventModel.RecurrencePattern.StartDate;
        DateOnly endDateOfEvent = eventModel.RecurrencePattern.EndDate;
        int interval = eventModel.RecurrencePattern.Interval;

        TimeSpan difference = DateTime.Parse(endDateOfEvent.ToString())
                            - DateTime.Parse(startDateOfEvent.ToString());

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
        return IsDateInRange(startDateOfEvent, endDateOfEvent, date)
               && (days.Count == 0 || days.Contains(GetDayNumberFromWeekDay(date)));
    }

    public static int GetDayNumberFromWeekDay(DateOnly date)
    {
        int dayNumber = Convert.ToInt32(date.DayOfWeek.ToString("d"));
        return dayNumber == 0 ? 7 : dayNumber;
    }

    private List<DateOnly> GetOccurrencesOfWeeklyEvents(EventModel eventModel)
    {
        List<DateOnly> occurrences = [];

        List<int> weekDays = [.. eventModel.RecurrencePattern.ByWeekDay ?? ([])];

        DateOnly startDateOfWeek = GetStartDateOfWeek(eventModel.RecurrencePattern.StartDate);

        foreach (var item in weekDays)
        {
            occurrences = [.. occurrences.Concat(GetOccurrencesOfWeekDay(eventModel, startDateOfWeek, item))];
        }

        occurrences.Sort();

        return occurrences;
    }

    public static DateOnly GetStartDateOfWeek(DateOnly todayDate)
    {
        return todayDate.AddDays(-(int)(todayDate.DayOfWeek - 1));
    }

    private List<DateOnly> GetOccurrencesOfWeekDay(EventModel eventModel, DateOnly startDateOfWeek, int item)
    {
        DateOnly startDateOfEvent = eventModel.RecurrencePattern.StartDate;
        DateOnly endDateOfEvent = eventModel.RecurrencePattern.EndDate;

        DateOnly startDateForSpecificWeekday = startDateOfWeek.AddDays(item - 1);

        TimeSpan difference = DateTime.Parse(endDateOfEvent.ToString())
                            - DateTime.Parse(startDateForSpecificWeekday.ToString());

        int interval = eventModel.RecurrencePattern.Interval;

        int totalOccurrences = ((int)difference.TotalDays / 7 * interval) + 1;

        return [..Enumerable.Range(0, totalOccurrences)
                                .Select(weekOffset => startDateForSpecificWeekday.AddDays(7 * weekOffset * interval))
                                .Where(date => IsDateInRange(startDateOfEvent,endDateOfEvent,date))];
    }

    private static bool IsDateInRange(DateOnly startDate, DateOnly endDate, DateOnly dateToCheck)
    {
        return dateToCheck >= startDate && dateToCheck <= endDate;
    }

    private List<DateOnly> GetOccurrencesOfMonthlyEvents(EventModel eventModel)
    {
        return eventModel.RecurrencePattern.ByMonthDay is null
               ? GetOccurrencesOfEventsUsingWeekOrderAndWeekDay(eventModel, true)
               : GetOccurrencesOfEventsUsingMonthDay(eventModel, true);
    }

    private List<DateOnly> GetOccurrencesOfYearlyEvents(EventModel eventModel)
    {
        return eventModel.RecurrencePattern.ByMonthDay is null
               ? GetOccurrencesOfEventsUsingWeekOrderAndWeekDay(eventModel, false)
               : GetOccurrencesOfEventsUsingMonthDay(eventModel, false);
    }

    public List<DateOnly> GetOccurrencesOfEventsUsingMonthDay(EventModel eventModel, bool isMonthly)
    {
        int monthDay = (int)eventModel.RecurrencePattern.ByMonthDay;

        DateOnly startDateOfEvent = eventModel.RecurrencePattern.StartDate;
        DateOnly endDateOfEvent = eventModel.RecurrencePattern.EndDate;

        int interval = eventModel.RecurrencePattern.Interval;

        int month = isMonthly ? startDateOfEvent.Month : (int)eventModel.RecurrencePattern.ByMonth;

        DateOnly currentDate = new(startDateOfEvent.Year, month, GetMinimumDateFromGivenMonthAndDay(monthDay, month, startDateOfEvent.Year));

        int totalOccurrences = isMonthly
                               ? GetCountOfMonthlyEventOccurrences(startDateOfEvent, endDateOfEvent, interval)
                               : GetCountOfYearlyEventOccurences(startDateOfEvent, endDateOfEvent, interval);

        return GetOccurrencesUsingMonthDay(monthDay, interval, currentDate, totalOccurrences, isMonthly);
    }

    private static int GetCountOfYearlyEventOccurences(DateOnly startDateOfEvent, DateOnly endDateOfEvent, int interval)
    {
        return (endDateOfEvent.Year - startDateOfEvent.Year) / interval + 1;
    }

    private static int GetCountOfMonthlyEventOccurrences(DateOnly startDateOfEvent, DateOnly endDateOfEvent, int interval)
    {
        return ((endDateOfEvent.Year - startDateOfEvent.Year) * 12 + (endDateOfEvent.Month - startDateOfEvent.Month)) / interval + 1;
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

    private List<DateOnly> GetOccurrencesOfEventsUsingWeekOrderAndWeekDay(EventModel eventModel, bool isMonthly)
    {
        int weekOrder = (int)eventModel.RecurrencePattern.WeekOrder;

        int weekDay = eventModel.RecurrencePattern.ByWeekDay[0];
        if (weekDay == 7) weekDay = 0;

        DayOfWeek dayOfWeek = (DayOfWeek)weekDay;

        DateOnly startDateOfEvent = eventModel.RecurrencePattern.StartDate;
        DateOnly endDateOfEvent = eventModel.RecurrencePattern.EndDate;
        int interval = eventModel.RecurrencePattern.Interval;

        int month = isMonthly
                    ? eventModel.RecurrencePattern.StartDate.Month
                    : (int)eventModel.RecurrencePattern.ByMonth;

        DateOnly currentDate = new(eventModel.RecurrencePattern.StartDate.Year, month, 1);

        int totalOccurrences = isMonthly
                               ? GetCountOfMonthlyEventOccurrences(startDateOfEvent, endDateOfEvent, interval)
                               : GetCountOfYearlyEventOccurences(startDateOfEvent, endDateOfEvent, interval);

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

    public static DateOnly GetNthWeekDayDate(int year, int month, DayOfWeek dayOfWeek, int weekOrder)
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

