namespace Core.Entities;

public class Duration
{
    public int StartHour { get; set; }

    public int EndHour { get; set; }

    private string ConvertTo12HourFormat(int hour)
    {
        string abbreviation = hour >= 12 && hour != 24 ? "PM" : "AM";
        hour = hour % 12 == 0 ? 12 : hour % 12;

        return $"{hour} {abbreviation}";
    }

    public string GetStartHourIn12HourFormat() => ConvertTo12HourFormat(StartHour);

    public string GetEndHourIn12HourFormat() => ConvertTo12HourFormat(EndHour);

    public string GetDurationInFormat() => ConvertTo12HourFormat(StartHour) + " - " + ConvertTo12HourFormat(EndHour);

    public bool IsOverlappingWith(Duration duration)
    {
        return (this.StartHour >= duration.StartHour && this.StartHour < duration.EndHour)
            || (this.EndHour > duration.StartHour && this.EndHour <= duration.EndHour)
            || (duration.StartHour >= this.StartHour && duration.StartHour < this.EndHour)
            || (duration.EndHour > this.StartHour && duration.EndHour <= this.EndHour);
    }
}
