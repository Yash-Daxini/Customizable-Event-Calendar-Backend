using Core.Entities.RecurrecePattern;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Entities.RecurrencePatternTests
{
    public class SingleInstanceRecurrencePatternGetOccurrences
    {
        [Fact]
        public void Should_ReturnSingleOccurrence_When_ItIsNonRecurringEvent()
        {
            RecurrencePattern recurrencePattern = new SingleInstanceRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 5, 1))
                                                  .WithInterval(1)
                                                  .WithFrequency()
                                                  .WithByWeekDay()
                                                  .Build();

            List<DateOnly> expectedOutput = [new DateOnly(2024, 5, 31)];

            List<DateOnly> actualOutput = recurrencePattern.GetOccurrences();

            Assert.Equal(expectedOutput, actualOutput);
        }
    }
}