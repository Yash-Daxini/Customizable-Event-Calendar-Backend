using Core.Domain;

namespace Infrastructure.Mappers;

public class DurationMapper
{
    public DurationModel MapDurationModel(int startHour, int endHour)
    {
        return new DurationModel
        {
            StartHour = startHour,
            EndHour = endHour
        };
    }
}
