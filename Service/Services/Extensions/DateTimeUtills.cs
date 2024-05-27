namespace Core.Services.Extensions;

public static class DateTimeUtills
{
    public static DateOnly GetStartDateOfWeek(this DateTime todayDate)
    {
        DateOnly todayDateOnly = DateOnly.FromDateTime(todayDate);
        return todayDateOnly.AddDays(-(int)(todayDate.DayOfWeek - 1));
    }

    public static DateOnly GetEndDateOfWeek(this DateTime todayDate)
    {
        return GetStartDateOfWeek(todayDate).AddDays(6);
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
}
