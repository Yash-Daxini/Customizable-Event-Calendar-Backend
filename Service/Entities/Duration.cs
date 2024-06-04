namespace Core.Entities;

public class Duration
{
    public int StartHour { get; set; }

    public int EndHour { get; set; }

    private string? ConvertTo12HourFormat(int hour)
    {
        if (hour < 0 || hour >= 24)
            return null;

        string abbreviation = hour >= 12 && hour != 24 ? "PM" : "AM";
        hour = hour % 12 == 0 ? 12 : hour % 12;

        return $"{hour} {abbreviation}";
    }

    public string? GetStartHourIn12HourFormat() => ConvertTo12HourFormat(StartHour);

    public string? GetEndHourIn12HourFormat() => ConvertTo12HourFormat(EndHour);

    public string? GetDurationInFormat()
    {
        string? startHour = ConvertTo12HourFormat(StartHour);
        string? endHour = ConvertTo12HourFormat(EndHour);

        if (startHour is null || endHour is null)
            return null;

        return startHour + " - " + endHour;
    }

    public bool IsOverlappingWith(Duration duration)
    {
        if (duration is null) return false;

        return (this.StartHour >= duration.StartHour && this.StartHour < duration.EndHour)
            || (this.EndHour > duration.StartHour && this.EndHour <= duration.EndHour)
            || (duration.StartHour >= this.StartHour && duration.StartHour < this.EndHour)
            || (duration.EndHour > this.StartHour && duration.EndHour <= this.EndHour);
    }

    public override bool Equals(object? obj)
    {
        if (obj is Duration duration)
        {
            return this.StartHour == duration.StartHour
                && this.EndHour == duration.EndHour;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.StartHour,
                                this.EndHour);
    }
}
