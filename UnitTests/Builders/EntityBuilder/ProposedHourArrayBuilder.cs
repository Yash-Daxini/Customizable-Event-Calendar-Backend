using Core.Entities;

namespace UnitTests.Builders.EntityBuilder;

public class ProposedHourArrayBuilder
{
    private readonly int[] proposedHour = new int[24];

    public int[] WithDurations(List<Duration> durations)
    {
        foreach (Duration duration in durations)
        {
            for (int j = duration.StartHour; j < duration.EndHour; j++)
            {
                proposedHour[j]++;
            }
        }

        return proposedHour;
    }
}
