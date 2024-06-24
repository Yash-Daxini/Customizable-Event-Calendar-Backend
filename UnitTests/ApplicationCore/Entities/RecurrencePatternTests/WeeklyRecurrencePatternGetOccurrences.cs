using Core.Entities.RecurrecePattern;
using FluentAssertions;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Entities.RecurrencePatternTests
{
    public class WeeklyRecurrencePatternGetOccurrences
    {

        [Fact]
        public void Should_ReturnEmptyList_When_ItIsWeeklyRecurringEventWithEmptyByWeekDay()
        {
            RecurrencePattern recurrencePattern = new WeeklyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 6, 20))
                                                  .WithInterval(2)
                                                  .WithByWeekDay([])
                                                  .Build();

            List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

            actualResult.Should().BeEmpty();
        }

        [Fact]
        public void Should_ReturnEmptyList_When_ItIsWeeklyRecurringEventWithNullByWeekDay()
        {
            RecurrencePattern recurrencePattern = new WeeklyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 6, 20))
                                                  .WithInterval(2)
                                                  .WithByWeekDay(null)
                                                  .Build();

            List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

            actualResult.Should().BeEmpty();
        }

        [Fact]
        public void Should_ReturnListOfOccurrences_When_ItIsWeeklyRecurringEvenAndIntervalIs0()
        {
            RecurrencePattern recurrencePattern = new WeeklyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 6, 20))
                                                  .WithInterval(0)
                                                  .WithByWeekDay(null)
                                                  .Build();

            List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

            actualResult.Should().BeEmpty();
        }
        [Fact]
        public void Should_ReturnListOfOccurrences_When_ItIsWeeklyRecurringEventWithNotNullByWeekDay()
        {
            RecurrencePattern recurrencePattern = new WeeklyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 6, 20))
                                                  .WithInterval(2)
                                                  .WithByWeekDay([6, 7])
                                                  .Build();

            List<DateOnly> expectedResult = [new DateOnly(2024, 6, 1), new DateOnly(2024, 6, 2),
                                         new DateOnly(2024, 6, 15), new DateOnly(2024, 6, 16)];

            List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}