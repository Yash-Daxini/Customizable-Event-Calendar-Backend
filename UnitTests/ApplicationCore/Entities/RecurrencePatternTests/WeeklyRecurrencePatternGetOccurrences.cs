using Core.Entities.RecurrecePattern;
using FluentAssertions;
using UnitTests.Builders.EntityBuilder;

namespace UnitTests.ApplicationCore.Entities.RecurrencePatternTests
{
    public class WeeklyRecurrencePatternGetOccurrences
    {

        [Fact]
        public void Should_Return_EmptyList_When_ItIsWeeklyRecurringEventWithEmptyWeekDay()
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
        public void Should_Return_EmptyList_When_ItIsWeeklyRecurringEventWithNullWeekDay()
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
        public void Should_Return_ListOfOccurrences_When_ItIsWeeklyRecurringEventAndIntervalIs0()
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
        public void Should_Return_ListOfOccurrences_When_ItIsWeeklyRecurringEventWithNotNullWeekDay()
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