using Core.Entities;

namespace Core.Services;

public static class MutualTimeCalculatorService
{
    public static Duration GetMaximumMutualTimeBlock(int[] proposedHours,
                                                      Event eventObj)
    {
        int max = proposedHours.Max();
        max = max > 1 ? max : -1;
        int timeBlock = eventObj.Duration.EndHour - eventObj.Duration.StartHour;


        int prevStartHour = -1;
        int prevEndHour = -1;

        for (int i = 0; i < proposedHours.Length; i++)
        {
            if (proposedHours[i] == max)
            {
                GetTimeBlock(proposedHours, max, out int startHour, out int endHour, timeBlock, i);


                if (prevEndHour - prevStartHour < endHour - startHour)
                {
                    prevStartHour = startHour;
                    prevEndHour = endHour;
                }

                if ((endHour - startHour) == timeBlock)
                    break;
            }
        }

        if (prevStartHour == -1)
        {
            prevStartHour = eventObj.Duration.StartHour;
            prevEndHour = eventObj.Duration.EndHour;
        }

        return new Duration(prevStartHour, prevEndHour);

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