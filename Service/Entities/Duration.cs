
using Core.Exceptions;

namespace Core.Entities;

public class Duration
{
    public Duration(int startHour, int endHour)
    {
        if (!IsValidStartAndEndHour(startHour, endHour))
            throw new InvalidDurationException("Invalid duration !");

        this.StartHour = startHour;
        this.EndHour = endHour;
    }

    private bool IsValidStartAndEndHour(int startHour, int endHour)
    {
        return startHour >= 0 && startHour <= 23
            && endHour >= 0 && endHour <= 23
            && startHour < endHour;
    }

    public int StartHour { get; set; }

    public int EndHour { get; set; }

    private string? ConvertTo12HourFormat(int hour)
    {
        string abbreviation = hour >= 12 && hour != 24 
                              ? "PM" 
                              : "AM";

        hour = hour % 12 == 0 
               ? 12 
               : hour % 12;

        return $"{hour} {abbreviation}";
    }

    public string? GetStartHourIn12HourFormat() => ConvertTo12HourFormat(StartHour);

    public string? GetEndHourIn12HourFormat() => ConvertTo12HourFormat(EndHour);

    public string? GetDurationInFormat()
    {
        string? startHour = ConvertTo12HourFormat(StartHour);
        string? endHour = ConvertTo12HourFormat(EndHour);

        return startHour + " - " + endHour;
    }

    public bool IsOverlappingWith(Duration duration)
    {
        if (duration is null) return false;

        return (this.StartHour >= duration.StartHour 
                && this.StartHour < duration.EndHour)
            || (this.EndHour > duration.StartHour 
                && this.EndHour <= duration.EndHour)
            || (duration.StartHour >= this.StartHour 
                && duration.StartHour < this.EndHour)
            || (duration.EndHour > this.StartHour 
                && duration.EndHour <= this.EndHour);
    }
}
