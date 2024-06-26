using Core.Entities;

namespace Core.Services;

public static class MutualTimeCalculatorService
{
    public static Duration FindMaximumMutualTimeBlock(int[] proposedHours,
                                                      Event eventObj)
    {
        int max = proposedHours.Max();
        max = max > 1 ? max : -1;

        int startHour = -1;
        int endHour = -1;

        int timeBlock = eventObj.Duration.EndHour - eventObj.Duration.StartHour;

        for (int i = 0; i < proposedHours.Length; i++)
        {
            if (proposedHours[i] == max && startHour == -1)
            {
                GetTimeBlock(proposedHours, max, out startHour, out endHour, timeBlock, i);

                if ((endHour - startHour) == timeBlock)
                    break;
            }
        }

        if (startHour == -1)
        {
            startHour = eventObj.Duration.StartHour;
            endHour = eventObj.Duration.EndHour;
        }

        return new Duration(startHour, endHour);

    }

    private static void GetTimeBlock(int[] proposedHours,
                                     int max,
                                     out int startHour,
                                     out int endHour,
                                     int timeBlock,
                                     int i)
    {
        startHour = i;
        endHour = i + 1;

        while (ShouldExtendTimeBlock(proposedHours,
                                     max,
                                     startHour,
                                     endHour,
                                     timeBlock))
            endHour++;

    }

    private static bool ShouldExtendTimeBlock(int[] proposedHours,
                                              int max,
                                              int startHour,
                                              int endHour,
                                              int timeBlock)
    {
        return endHour < proposedHours.Length
               && proposedHours[endHour] == max
               && (endHour - startHour) <= timeBlock;
    }
}