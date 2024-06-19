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
        if(Interval <=  0)
            return 0;

        return ((EndDate.Year - StartDate.Year) * 12 + (EndDate.Month - StartDate.Month)) / Interval + 1;
    }

    private List<DateOnly> GetOccurrencesOfEventsUsingMonthDay()
    {
        int month = StartDate.Month;

        DateOnly currentDate = new(StartDate.Year,
                                   month,
                                   GetMinimumDateFromGivenMonthAndDay((int)ByMonthDay,
                                                                      month,
                                                                      StartDate.Year));

        int totalOccurrences = GetOccurrencesCount();

        return GetOccurrencesUsingMonthDay(currentDate, totalOccurrences);
    }

    private List<DateOnly> GetOccurrencesUsingMonthDay(DateOnly currentDate,
                                                       int totalOccurrences)
    {
        return [..Enumerable.Range(0, totalOccurrences)
                                  .Select(weekOffset =>
                                  {
                                      DateOnly date = currentDate.AddMonths(weekOffset* Interval);
                                      return new DateOnly(date.Year,
                                                          date.Month,
                                                          GetMinimumDateFromGivenMonthAndDay((int)ByMonthDay,
                                                                                             date.Month,
                                                                                             date.Year));
                                  } )];
    }

    private static int GetMinimumDateFromGivenMonthAndDay(int day, int month, int year)
    {
        int daysInMonth = DateTime.DaysInMonth(year, month);

        return Math.Min(day, daysInMonth);
    }

    private List<DateOnly> GetOccurrencesOfEventsUsingWeekOrderAndWeekDay()
    {
        if (WeekOrder is null || ByWeekDay is null || WeekOrder is null || WeekOrder <= 0)
            return [];

        int weekDay = ByWeekDay[0] % 7;
        DayOfWeek dayOfWeek = (DayOfWeek)weekDay;

        DateOnly currentDate = new(StartDate.Year, StartDate.Month, 1);

        int totalOccurrences = GetOccurrencesCount();

        return GetOccurrencesUsingWeekOrderAndWeekDay(dayOfWeek, currentDate, totalOccurrences);
    }

    private List<DateOnly> GetOccurrencesUsingWeekOrderAndWeekDay(DayOfWeek dayOfWeek,
                                                                         DateOnly currentDate,
                                                                         int totalOccurrences)
    {
        return [..Enumerable.Range(0, totalOccurrences)
                                .Select(weekOffset =>
                                {
                                      DateOnly date = currentDate.AddMonths(weekOffset*Interval);
                                      return GetNthWeekDayDate(date.Year,date.Month,dayOfWeek);
                                } )
                                .Where(date => date >= StartDate && date <= EndDate)];
    }

    private DateOnly GetNthWeekDayDate(int year, int month, DayOfWeek dayOfWeek)
    {
        DateOnly firstDayOfMonth = new(year, month, 1);

        int weekOrder = (int)WeekOrder;

        List<DateOnly> weekDays = [..Enumerable.Range(0, DateTime.DaysInMonth(year, month))
                                               .Select(firstDayOfMonth.AddDays)
                                               .GroupBy(date => date.DayOfWeek)
                                               .First(date => date.Key == dayOfWeek)];

        return weekDays.Count < weekOrder
               ? weekDays[^1]
               : weekDays[weekOrder - 1];
    }
}
