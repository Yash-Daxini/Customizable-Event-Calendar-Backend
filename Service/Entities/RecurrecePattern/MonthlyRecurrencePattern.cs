using Core.Extensions;

namespace Core.Entities.RecurrecePattern;

public class MonthlyRecurrencePattern : RecurrencePattern
{
    public int? WeekOrder { get; set; }

    public int? ByMonthDay { get; set; }

    public bool IsMonthDayNull() => ByMonthDay == null;

    public override List<DateOnly> GetOccurrences()
    {
        return IsMonthDayNull()
               ? GetOccurrencesOfEventsUsingWeekOrderAndWeekDay()
               : GetOccurrencesOfEventsUsingMonthDay();
    }

    public override int GetOccurrencesCount()
    {
        if (Interval <= 0)
            return 0;

        return (((EndDate.Year - StartDate.Year) * 12
                + (EndDate.Month - StartDate.Month)) / Interval) + 1;
    }

    private List<DateOnly> GetOccurrencesOfEventsUsingMonthDay()
    {
        DateOnly currentDate = StartDate.GetMaxDate((int)ByMonthDay, null);

        int totalOccurrences = GetOccurrencesCount();

        return GetOccurrencesUsingMonthDay(currentDate, totalOccurrences);
    }

    private List<DateOnly> GetOccurrencesUsingMonthDay(DateOnly currentDate,
                                                       int totalOccurrences)
    {
        return [..Enumerable.Range(0, totalOccurrences)
                            .Select(weekOffset => currentDate
                                                  .AddMonths(weekOffset* Interval)
                                                  .GetMaxDate((int)ByMonthDay,null))];
    }

    private List<DateOnly> GetOccurrencesOfEventsUsingWeekOrderAndWeekDay()
    {
        if (WeekOrder is null
            || ByWeekDay is null
            || WeekOrder is null
            || WeekOrder <= 0)
            return [];

        int weekDay = ByWeekDay[0] % 7;
        DayOfWeek dayOfWeek = (DayOfWeek)weekDay;

        DateOnly currentDate = new(StartDate.Year, StartDate.Month, 1);

        int totalOccurrences = GetOccurrencesCount();

        return GetOccurrencesUsingWeekOrderAndWeekDay(dayOfWeek,
                                                      currentDate,
                                                      totalOccurrences);
    }

    private List<DateOnly> GetOccurrencesUsingWeekOrderAndWeekDay(DayOfWeek dayOfWeek,
                                                                  DateOnly currentDate,
                                                                  int totalOccurrences)
    {
        return [..Enumerable.Range(0, totalOccurrences)
                            .Select(weekOffset => currentDate
                                                  .AddMonths(weekOffset*Interval)
                                                  .GetNthWeekDayDate((int)WeekOrder,
                                                                     dayOfWeek))
                            .Where(date => date.IsDateInRange(StartDate,EndDate))];
    }
}
