using Core.Extensions;

namespace Core.Entities.RecurrecePattern;

public class YearlyRecurrencePattern : RecurrencePattern
{
    public int? WeekOrder { get; set; }

    public int? ByMonthDay { get; set; }

    public int? ByMonth { get; set; }

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

        return ((EndDate.Year - StartDate.Year) / Interval) + 1;
    }

    private List<DateOnly> GetOccurrencesOfEventsUsingMonthDay()
    {
        if (!IsValidMonth() || !IsValidMonthDay())
            return [];

        DateOnly currentDate = StartDate.GetMaxDate((int)ByMonthDay, (int)ByMonth);

        int totalOccurrences = GetOccurrencesCount();

        return GetOccurrencesUsingMonthDay(currentDate, totalOccurrences);
    }

    private bool IsValidMonth()
    {
        return ByMonth >= 1
               && ByMonth <= 12;
    }

    private bool IsValidMonthDay()
    {
        return ByMonthDay >= 1
               && ByMonthDay <= 31;
    }

    private List<DateOnly> GetOccurrencesUsingMonthDay(DateOnly currentDate,
                                                       int totalOccurrences)
    {
        return [..Enumerable.Range(0, totalOccurrences)
                            .Select(weekOffset => currentDate
                                                  .AddYears(weekOffset * Interval)
                                                  .GetMaxDate((int)ByMonthDay,
                                                              (int)ByMonth))];
    }

    private List<DateOnly> GetOccurrencesOfEventsUsingWeekOrderAndWeekDay()
    {
        if (ByWeekDay is null
            || ByMonth is null
            || WeekOrder is null
            || WeekOrder <= 0)
            return [];

        int weekDay = ByWeekDay[0] % 7;
        DayOfWeek dayOfWeek = (DayOfWeek)weekDay;

        int month = (int)ByMonth;

        DateOnly currentDate = new(StartDate.Year, month, 1);

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
                                                  .AddYears(weekOffset* Interval)
                                                  .GetNthWeekDayDate((int)WeekOrder,
                                                                     dayOfWeek))];
    }
}
