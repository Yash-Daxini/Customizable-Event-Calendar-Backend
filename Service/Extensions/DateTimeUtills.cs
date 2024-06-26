using System.Globalization;

namespace Core.Extensions;

public static class DateTimeUtills
{
    public static DateOnly GetStartDateOfWeek(this DateTime todayDate)
    {
        DateOnly todayDateOnly = DateOnly.FromDateTime(todayDate);

        int dayOfWeek = (int)todayDate.DayOfWeek - 1;

        if (dayOfWeek == -1)
            dayOfWeek = 6;

        return todayDateOnly.AddDays(-dayOfWeek);
    }

    public static DateOnly GetEndDateOfWeek(this DateTime todayDate)
    {
        return todayDate.GetStartDateOfWeek().AddDays(6);
    }

    public static DateOnly GetStartDateOfMonth(this DateTime todayDate)
    {
        return ConvertToDateOnly(new(todayDate.Year, todayDate.Month, 1));
    }

    public static DateOnly GetEndDateOfMonth(this DateTime todayDate)
    {
        int lastDayOfMonth = DateTime.DaysInMonth(todayDate.Year,
                                                  todayDate.Month);

        return ConvertToDateOnly(new(todayDate.Year,
                                     todayDate.Month,
                                     lastDayOfMonth));
    }

    public static DateOnly ConvertToDateOnly(this DateTime dateTime)
    {
        return DateOnly.FromDateTime(dateTime);
    }

    public static DateTime ConvertToDateTime(this DateOnly dateOnly)
    {
        return DateTime.Parse(dateOnly.ToString(), CultureInfo.CurrentCulture);
    }

    public static bool IsDateInRange(this DateOnly dateOnly,
                                          DateOnly startDate,
                                          DateOnly endDate)
    {
        return dateOnly >= startDate && dateOnly <= endDate;
    }

    public static int GetDayNumberFromWeekDay(this DateOnly date)
    {
        int dayNumber = Convert.ToInt32(date.DayOfWeek.ToString("d"));
        return dayNumber == 0
               ? 7
               : dayNumber;
    }

    public static DateOnly GetMaxDate(this DateOnly dateOnly,
                                      int monthDay,
                                      int? month)
    {
        month ??= dateOnly.Month;

        int daysInMonth = DateTime.DaysInMonth(dateOnly.Year, (int)month);

        int minimumPossibleMonthDay = Math.Min(monthDay, daysInMonth);

        return new DateOnly(dateOnly.Year, (int)month, minimumPossibleMonthDay);
    }

    public static DateOnly GetNthWeekDayDate(this DateOnly dateOnly,
                                             int weekOrder,
                                             DayOfWeek dayOfWeek)
    {
        int month = dateOnly.Month;
        int year = dateOnly.Year;

        DateOnly firstDayOfMonth = new(year, month, 1);

        int daysInMonth = DateTime.DaysInMonth(year, month);

        List<DateOnly> weekDays = [..Enumerable.Range(0, daysInMonth)
                                               .Select(firstDayOfMonth.AddDays)
                                               .GroupBy(date => date.DayOfWeek)
                                               .First(date => date.Key == dayOfWeek)];

        return weekDays.Count < weekOrder
               ? weekDays[^1]
               : weekDays[weekOrder - 1];
    }
}
