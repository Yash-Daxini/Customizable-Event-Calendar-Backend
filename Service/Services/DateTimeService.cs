namespace Core.Services;

public static class DateTimeService
{
    public static DateOnly GetStartDateOfWeek(DateTime todayDate)
    {
        DateOnly todayDateOnly = DateOnly.FromDateTime(todayDate);
        return todayDateOnly.AddDays(-(int)(todayDate.DayOfWeek - 1));
    }

    public static DateOnly GetEndDateOfWeek(DateTime todayDate)
    {
        return GetStartDateOfWeek(todayDate).AddDays(6);
    }

    public static DateOnly GetStartDateOfMonth(DateTime todayDate)
    {
        return ConvertToDateOnly(new(todayDate.Year, todayDate.Month, 1));
    }

    public static DateOnly GetEndDateOfMonth(DateTime todayDate)
    {
        return ConvertToDateOnly(new(todayDate.Year, todayDate.Month, DateTime.DaysInMonth(todayDate.Year, todayDate.Month)));
    }

    public static DateOnly ConvertToDateOnly(DateTime dateTime)
    {
        return DateOnly.FromDateTime(dateTime);
    }
}
