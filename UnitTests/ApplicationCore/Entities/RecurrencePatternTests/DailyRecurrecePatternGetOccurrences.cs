using Core.Entities.RecurrecePattern;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Entities.RecurrencePatternTests
{
    public class DailyRecurrecePatternGetOccurrences
    {

        [Fact]
        public void Should_ReturnListOfOccurrences_When_ItIsDailyRecurringEventAndIntervalIs0()
        {
            RecurrencePattern recurrencePattern = new DailyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 6, 20))
                                                  .WithFrequency()
                                                  .WithByWeekDay(null)
                                                  .WithInterval(0)
                                                  .Build();

            List<DateOnly> expectedOutput = [];

            List<DateOnly> actualOutput = recurrencePattern.GetOccurrences();

            Assert.Equal(expectedOutput, actualOutput);
        }

        [Fact]
        public void Should_ReturnListOfOccurrences_When_ItIsDailyRecurringEventAndWeekDayIsEmpty()
        {
            RecurrencePattern recurrencePattern = new DailyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 6, 20))
                                                  .WithFrequency()
                                                  .WithByWeekDay([])
                                                  .WithInterval(2)
                                                  .Build();

            List<DateOnly> expectedOutput = [new DateOnly(2024, 5, 31), new DateOnly(2024, 6, 2), new DateOnly(2024, 6, 4), new DateOnly(2024, 6, 6),
                                         new DateOnly(2024, 6, 8),new DateOnly(2024, 6, 10),new DateOnly(2024, 6, 12),new DateOnly(2024, 6, 14),
                                         new DateOnly(2024, 6, 16),new DateOnly(2024, 6, 18),new DateOnly(2024, 6, 20)];

            List<DateOnly> actualOutput = recurrencePattern.GetOccurrences();

            Assert.Equal(expectedOutput, actualOutput);
        }
        [Fact]
        public void Should_ReturnListOfOccurrences_When_ItIsDailyRecurringEventAndWeekDayIsNull()
        {
            RecurrencePattern recurrencePattern = new DailyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 6, 20))
                                                  .WithFrequency()
                                                  .WithByWeekDay(null)
                                                  .WithInterval(2)
                                                  .Build();

            List<DateOnly> expectedOutput = [new DateOnly(2024, 5, 31), new DateOnly(2024, 6, 2), new DateOnly(2024, 6, 4), new DateOnly(2024, 6, 6),
                                         new DateOnly(2024, 6, 8),new DateOnly(2024, 6, 10),new DateOnly(2024, 6, 12),new DateOnly(2024, 6, 14),
                                         new DateOnly(2024, 6, 16),new DateOnly(2024, 6, 18),new DateOnly(2024, 6, 20)];

            List<DateOnly> actualOutput = recurrencePattern.GetOccurrences();

            Assert.Equal(expectedOutput, actualOutput);
        }

        [Fact]
        public void Should_ReturnListOfOccurrences_When_ItIsDailyRecurringEventWithIntervalValue3()
        {
            RecurrencePattern recurrencePattern = new DailyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 6, 20))
                                                  .WithFrequency()
                                                  .WithByWeekDay(null)
                                                  .WithInterval(3)
                                                  .Build();

            List<DateOnly> expectedOutput = [new DateOnly(2024, 5, 31), new DateOnly(2024, 6, 3), new DateOnly(2024, 6, 6),
                                         new DateOnly(2024, 6, 9),new DateOnly(2024, 6, 12),new DateOnly(2024, 6, 15),
                                         new DateOnly(2024, 6, 18)];

            List<DateOnly> actualOutput = recurrencePattern.GetOccurrences();

            Assert.Equal(expectedOutput, actualOutput);
        }

        [Fact]
        public void Should_ReturnListOfOccurrences_When_ItIsDailyRecurringEventWithSameStartDateAndEndDate()
        {
            RecurrencePattern recurrencePattern = new DailyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 5, 31))
                                                  .WithFrequency()
                                                  .WithByWeekDay(null)
                                                  .WithInterval(1)
                                                  .Build();

            List<DateOnly> expectedOutput = [new DateOnly(2024, 5, 31)];

            List<DateOnly> actualOutput = recurrencePattern.GetOccurrences();

            Assert.Equal(expectedOutput, actualOutput);
        }

        [Fact]
        public void Should_ReturnListOfOccurrences_When_ItIsDailyRecurringEventWithWeekDays()
        {
            RecurrencePattern recurrencePattern = new DailyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 6, 20))
                                                  .WithFrequency()
                                                  .WithByWeekDay([6, 7])
                                                  .WithInterval(2)
                                                  .Build();

            List<DateOnly> expectedOutput = [new DateOnly(2024, 6, 2), new DateOnly(2024, 6, 8), new DateOnly(2024, 6, 16)];

            List<DateOnly> actualOutput = recurrencePattern.GetOccurrences();

            Assert.Equal(expectedOutput, actualOutput);
        }
    }
}