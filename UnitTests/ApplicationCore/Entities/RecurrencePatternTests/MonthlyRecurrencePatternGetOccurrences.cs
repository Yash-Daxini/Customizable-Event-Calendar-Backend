using Core.Entities.RecurrecePattern;
using FluentAssertions;
using UnitTests.Builders.EntityBuilder;

namespace UnitTests.ApplicationCore.Entities.RecurrencePatternTests
{
    public class MonthlyRecurrencePatternGetOccurrences
    {

        [Fact]
        public void Should_Return_EmptyList_When_ItIsMonthlyRecurringEventWithNullMonthDayWeekDayAndWeekOrder()
        {
            RecurrencePattern recurrencePattern = new MonthlyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 10, 20))
                                                  .WithByWeekDay(null)
                                                  .WithByMonthDay(null)
                                                  .WithWeekOrder(null)
                                                  .WithInterval(2)
                                                  .Build();

            List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

            actualResult.Should().BeEmpty();
        }

        [Fact]
        public void Should_Return_EmptyList_When_ItIsMonthlyRecurringEventUsingWeekOrderWithNullWeekDayAndNullMonthDay()
        {
            RecurrencePattern recurrencePattern = new MonthlyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 10, 20))
                                                  .WithByWeekDay(null)
                                                  .WithByMonthDay(null)
                                                  .WithWeekOrder(5)
                                                  .WithInterval(2)
                                                  .Build();

            List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

            actualResult.Should().BeEmpty();
        }

        [Fact]
        public void Should_Return_EmptyList_When_ItIsMonthlyRecurringEventUsingWeekOrderWithNullWeekOrderAndNullMonthDay()
        {
            RecurrencePattern recurrencePattern = new MonthlyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 10, 20))
                                                  .WithByWeekDay([7])
                                                  .WithByMonthDay(null)
                                                  .WithWeekOrder(null)
                                                  .WithInterval(2)
                                                  .Build();

            List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

            actualResult.Should().BeEmpty();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(32)]
        public void Should_Return_EmptyList_When_ItIsMonthlyRecurringEventUsingMonthDayWithInvalidMonthDay(int monthDay)
        {
            RecurrencePattern recurrencePattern = new MonthlyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2025, 5, 20))
                                                  .WithInterval(2)
                                                  .WithWeekOrder(null)
                                                  .WithByMonthDay(monthDay)
                                                  .WithByWeekDay(null)
                                                  .Build();

            List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

            actualResult.Should().BeEmpty();
        }

        [Fact]
        public void Should_Return_EmptyList_When_ItIsMonthlyRecurringEventUsingWeekOrderWithZeroValueOfWeekOrder()
        {
            RecurrencePattern recurrencePattern = new MonthlyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 10, 20))
                                                  .WithByWeekDay([7])
                                                  .WithByMonthDay(null)
                                                  .WithWeekOrder(0)
                                                  .WithInterval(2)
                                                  .Build();

            List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

            actualResult.Should().BeEmpty();
        }

        [Theory]
        [InlineData(25)]
        [InlineData(21)]
        [InlineData(18)]
        [InlineData(1)]
        public void Should_Return_ListOfOccurrences_When_ItIsMonthlyRecurringEventUsingMonthDayWithNullWeekOrderAndNullByWeekDay(int monthDay)
        {
            RecurrencePattern recurrencePattern = new MonthlyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 10, 20))
                                                  .WithByWeekDay(null)
                                                  .WithByMonthDay(monthDay)
                                                  .WithWeekOrder(null)
                                                  .WithInterval(2)
                                                  .Build();

            List<DateOnly> expectedResult = [new DateOnly(2024, 5, monthDay), new DateOnly(2024, 7, monthDay), new DateOnly(2024, 9, monthDay)];

            List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }
        [Fact]
        public void Should_Return_ListOfOccurrences_When_ItIsMonthlyRecurringEventUsingMonthDayForLastDayOfMonth()
        {
            RecurrencePattern recurrencePattern = new MonthlyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 10, 20))
                                                  .WithByWeekDay(null)
                                                  .WithByMonthDay(31)
                                                  .WithWeekOrder(null)
                                                  .WithInterval(2)
                                                  .Build();

            List<DateOnly> expectedResult = [new DateOnly(2024, 5, 31), new DateOnly(2024, 7, 31), new DateOnly(2024, 9, 30)];

            List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void Should_Return_ListOfOccurrences_When_ItIsMonthlyRecurringEventUsingWeekOrder()
        {
            RecurrencePattern recurrencePattern = new MonthlyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 10, 20))
                                                  .WithByWeekDay([7])
                                                  .WithByMonthDay(null)
                                                  .WithWeekOrder(5)
                                                  .WithInterval(2)
                                                  .Build();

            List<DateOnly> expectedResult = [new DateOnly(2024, 7, 28), new DateOnly(2024, 9, 29)];

            List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void Should_Return_ListWithLastOccurrence_When_ItIsMonthlyRecurringEventUsingWeekOrderWithWeekOrderIsGreaterThan5()
        {
            RecurrencePattern recurrencePattern = new MonthlyRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 5, 31))
                                                  .WithEndDate(new DateOnly(2024, 7, 31))
                                                  .WithByWeekDay([7])
                                                  .WithByMonthDay(null)
                                                  .WithWeekOrder(10)
                                                  .WithInterval(1)
                                                  .Build();

            List<DateOnly> expectedResult = [new DateOnly(2024, 6, 30), new DateOnly(2024, 7, 28)];

            List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}