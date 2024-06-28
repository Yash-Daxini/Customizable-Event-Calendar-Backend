using Core.Entities.RecurrecePattern;
using FluentAssertions;
using UnitTests.Builders.EntityBuilder;

namespace UnitTests.ApplicationCore.Entities.RecurrencePatternTests
{
    public class DailyRecurrencePatternGetOccurrences
    {

        [Fact]
        public void Should_Return_EmptyList_When_ItIsDailyRecurringEventAndIntervalIs0()
        {
            RecurrencePattern recurrencePattern = new DailyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 6, 20))
                                                  .WithByWeekDay(null)
                                                  .WithInterval(0)
                                                  .Build();

            List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

            actualResult.Should().BeEmpty();
        }

        [Fact]
        public void Should_Return_ListOfOccurrences_When_ItIsDailyRecurringEventAndWeekDayIsEmpty()
        {
            RecurrencePattern recurrencePattern = new DailyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 6, 20))
                                                  .WithByWeekDay([])
                                                  .WithInterval(2)
                                                  .Build();

            List<DateOnly> expectedResult = [new DateOnly(2024, 5, 31), new DateOnly(2024, 6, 2), new DateOnly(2024, 6, 4), new DateOnly(2024, 6, 6),
                                         new DateOnly(2024, 6, 8),new DateOnly(2024, 6, 10),new DateOnly(2024, 6, 12),new DateOnly(2024, 6, 14),
                                         new DateOnly(2024, 6, 16),new DateOnly(2024, 6, 18),new DateOnly(2024, 6, 20)];

            List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }
        [Fact]
        public void Should_Return_ListOfOccurrences_When_ItIsDailyRecurringEventAndWeekDayIsNull()
        {
            RecurrencePattern recurrencePattern = new DailyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 6, 20))
                                                  .WithByWeekDay(null)
                                                  .WithInterval(2)
                                                  .Build();

            List<DateOnly> expectedResult = [new DateOnly(2024, 5, 31), new DateOnly(2024, 6, 2), new DateOnly(2024, 6, 4), new DateOnly(2024, 6, 6),
                                         new DateOnly(2024, 6, 8),new DateOnly(2024, 6, 10),new DateOnly(2024, 6, 12),new DateOnly(2024, 6, 14),
                                         new DateOnly(2024, 6, 16),new DateOnly(2024, 6, 18),new DateOnly(2024, 6, 20)];

            List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void Should_Return_ListOfOccurrences_When_ItIsDailyRecurringEventWithIntervalValue3()
        {
            RecurrencePattern recurrencePattern = new DailyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 6, 20))
                                                  .WithByWeekDay(null)
                                                  .WithInterval(3)
                                                  .Build();

            List<DateOnly> expectedResult = [new DateOnly(2024, 5, 31), new DateOnly(2024, 6, 3), new DateOnly(2024, 6, 6),
                                         new DateOnly(2024, 6, 9),new DateOnly(2024, 6, 12),new DateOnly(2024, 6, 15),
                                         new DateOnly(2024, 6, 18)];

            List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void Should_Return_ListOfOccurrences_When_ItIsDailyRecurringEventWithSameStartDateAndEndDate()
        {
            RecurrencePattern recurrencePattern = new DailyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 5, 31))
                                                  .WithByWeekDay(null)
                                                  .WithInterval(1)
                                                  .Build();

            List<DateOnly> expectedResult = [new DateOnly(2024, 5, 31)];

            List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void Should_Return_ListOfOccurrences_When_ItIsDailyRecurringEventWithWeekDays()
        {
            RecurrencePattern recurrencePattern = new DailyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 6, 20))
                                                  .WithByWeekDay([6, 7])
                                                  .WithInterval(2)
                                                  .Build();

            List<DateOnly> expectedResult = [new DateOnly(2024, 6, 2), new DateOnly(2024, 6, 8), new DateOnly(2024, 6, 16)];

            List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}