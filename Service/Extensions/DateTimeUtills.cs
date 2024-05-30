namespace Core.Extensions;

public static class DateTimeUtills
{
    public static DateOnly GetStartDateOfWeek(this DateTime todayDate)
    {
        DateOnly todayDateOnly = DateOnly.FromDateTime(todayDate);
        return todayDateOnly.AddDays(-(int)(todayDate.DayOfWeek - 1));
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
        return ConvertToDateOnly(new(todayDate.Year, todayDate.Month, DateTime.DaysInMonth(todayDate.Year, todayDate.Month)));
    }

    public static DateOnly ConvertToDateOnly(this DateTime dateTime)
    {
        return DateOnly.FromDateTime(dateTime);
    }

    public static DateTime ConvertToDateTime(this DateOnly dateOnly)
    {
        return DateTime.Parse(dateOnly.ToString());
    }

    public static bool IsDateInRange(this DateOnly dateOnly, DateOnly startDate, DateOnly endDate)
    {
        return dateOnly >= startDate && dateOnly <= endDate;
    }

    public static DateOnly GetStartDateOfWeek(this DateOnly todayDate)
    {
        return todayDate.AddDays(-(int)(todayDate.DayOfWeek - 1));
    }

    public static int GetDayNumberFromWeekDay(this DateOnly date)
    {
        int dayNumber = Convert.ToInt32(date.DayOfWeek.ToString("d"));
        return dayNumber == 0
               ? 7
               : dayNumber;
    }
}
