
namespace Core.Entities;

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
        return ((EndDate.Year - StartDate.Year) / Interval) + 1;
    }

    private List<DateOnly> GetOccurrencesOfEventsUsingMonthDay()
    {
        if (ByMonth is null)
            return [];

        DateOnly startDateOfEvent = StartDate;

        DateOnly currentDate = new(startDateOfEvent.Year, 
                                  (int)ByMonth , 
                                  GetMinimumDayFromGivenMonthAndDay((int)ByMonthDay,
                                                                    (int)ByMonth,
                                                                    startDateOfEvent.Year));

        int totalOccurrences = GetOccurrencesCount();

        return GetOccurrencesUsingMonthDay(currentDate, totalOccurrences);
    }

    private List<DateOnly> GetOccurrencesUsingMonthDay(DateOnly currentDate,
                                                       int totalOccurrences)
    {
        return [..Enumerable.Range(0, totalOccurrences)
                                  .Select(weekOffset =>
                                  {
                                      DateOnly date = currentDate.AddYears(weekOffset*Interval);
                                      return new DateOnly(date.Year,
                                                          date.Month,
                                                          GetMinimumDayFromGivenMonthAndDay((int)ByMonthDay,
                                                                                            date.Month,
                                                                                            date.Year));
                                  } )];
    }

    private static int GetMinimumDayFromGivenMonthAndDay(int day, int month, int year)
    {
        int daysInMonth = DateTime.DaysInMonth(year, month);

        return Math.Min(day, daysInMonth);
    }

    private List<DateOnly> GetOccurrencesOfEventsUsingWeekOrderAndWeekDay()
    {
        if (ByWeekDay is null || ByMonth is null || WeekOrder is null || WeekOrder <= 0)
            return [];

        int weekDay = ByWeekDay[0] % 7;
        DayOfWeek dayOfWeek = (DayOfWeek)weekDay;

        int month = (int)ByMonth;

        DateOnly currentDate = new(StartDate.Year, month, 1);

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
                                      DateOnly date = currentDate.AddYears(weekOffset* Interval);
                                      return GetNthWeekDayDate(date.Year,date.Month,dayOfWeek);
                                } )];
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
