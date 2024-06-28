using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Core.Entities.RecurrecePattern;
using FluentAssertions;
using UnitTests.Builders.EntityBuilder;
using Core.Entities.Enums;

namespace UnitTests.Infrastructure.Repositories.EventRepositoryTests;

public class GetAllEventsByUserId : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContextEvent;
    private readonly IMapper _mapper;

    public GetAllEventsByUserId(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Theory]
    [InlineData(1)]
    public async Task Should_Return_EventList_When_EventAvailableWithGivenUserId(int userId)
    {
        //Arrange

        _dbContextEvent = await new EventRepositoryDBContext().GetDatabaseContext();

        SingleInstanceRecurrencePattern singleInstanceRecurrencePattern = new SingleInstanceRecurrencePatternBuilder()
                                                                          .WithStartDate(new DateOnly(2024, 6, 7))
                                                                          .WithEndDate(new DateOnly(2024, 6, 7))
                                                                          .WithInterval(1)
                                                                          .Build();

        User user1 = new UserBuilder(1)
                    .WithName("a")
                    .WithEmail("a")
                    .Build();

        User user2 = new UserBuilder(2)
                    .WithName("b")
                    .WithEmail("b")
                    .Build();

        Event event1 = new EventBuilder()
                       .WithId(1)
                       .WithTitle("Test1")
                       .WithDescription("Test1")
                       .WithLocation("Test1")
                       .WithDuration(new Duration(1, 2))
                       .WithRecurrencePattern(singleInstanceRecurrencePattern)
                       .WithEventCollaborators(new EventCollaboratorListBuilder(1)
                                               .WithOrganizer(user1, new DateOnly(2024, 6, 7))
                                               .Build())
                       .Build();

        DailyRecurrencePattern dailyRecurrencePattern1 = new DailyRecurrencePatternBuilder()
                                                         .WithStartDate(new DateOnly(2024, 6, 7))
                                                         .WithEndDate(new DateOnly(2024, 6, 7))
                                                         .WithInterval(1)
                                                         .WithByWeekDay([])
                                                         .Build();

        Event event2 = new EventBuilder()
                       .WithId(2)
                       .WithTitle("Test1")
                       .WithDescription("Test1")
                       .WithLocation("Test1")
                       .WithDuration(new Duration(2, 3))
                       .WithRecurrencePattern(dailyRecurrencePattern1)
                       .WithEventCollaborators(new EventCollaboratorListBuilder(2)
                                               .WithOrganizer(user1, new DateOnly(2024, 6, 7))
                                               .WithParticipant(user2, ConfirmationStatus.Pending, new DateOnly(2024, 6, 7), null)
                                               .Build())
                       .Build();

        DailyRecurrencePattern dailyRecurrencePattern2 = new DailyRecurrencePatternBuilder()
                                                         .WithStartDate(new DateOnly(2024, 6, 7))
                                                         .WithEndDate(new DateOnly(2024, 6, 7))
                                                         .WithInterval(1)
                                                         .WithByWeekDay([1, 2])
                                                         .Build();

        Event event3 = new EventBuilder()
                       .WithId(3)
                       .WithTitle("Test1")
                       .WithDescription("Test1")
                       .WithLocation("Test1")
                       .WithDuration(new Duration(2, 3))
                       .WithRecurrencePattern(dailyRecurrencePattern2)
                       .WithEventCollaborators(new EventCollaboratorListBuilder(3)
                                               .WithOrganizer(user1, new DateOnly(2024, 6, 7))
                                               .Build())
                       .Build();

        WeeklyRecurrencePattern weeklyRecurrencePattern1 = new WeeklyRecurrencePatternBuilder()
                                                           .WithStartDate(new DateOnly(2024, 6, 7))
                                                           .WithEndDate(new DateOnly(2024, 6, 7))
                                                           .WithInterval(1)
                                                           .WithByWeekDay([1, 2])
                                                           .Build();

        Event event4 = new EventBuilder()
                       .WithId(4)
                       .WithTitle("Test1")
                       .WithDescription("Test1")
                       .WithLocation("Test1")
                       .WithDuration(new Duration(2, 3))
                       .WithRecurrencePattern(weeklyRecurrencePattern1)
                       .WithEventCollaborators(new EventCollaboratorListBuilder(4)
                                               .WithOrganizer(user1, new DateOnly(2024, 6, 7))
                                               .Build())
                       .Build();

        WeeklyRecurrencePattern weeklyRecurrencePattern2 = new WeeklyRecurrencePatternBuilder()
                                                           .WithStartDate(new DateOnly(2024, 6, 7))
                                                           .WithEndDate(new DateOnly(2024, 6, 7))
                                                           .WithInterval(1)
                                                           .WithByWeekDay([])
                                                           .Build();

        Event event5 = new EventBuilder()
                       .WithId(5)
                       .WithTitle("Test1")
                       .WithDescription("Test1")
                       .WithLocation("Test1")
                       .WithDuration(new Duration(2, 3))
                       .WithRecurrencePattern(weeklyRecurrencePattern2)
                       .WithEventCollaborators(new EventCollaboratorListBuilder(5)
                                               .WithOrganizer(user1, new DateOnly(2024, 6, 7))
                                               .Build())
                       .Build();

        MonthlyRecurrencePattern monthlyRecurrencePattern1 = new MonthlyRecurrencePatternBuilder()
                                                            .WithStartDate(new DateOnly(2024, 6, 7))
                                                            .WithEndDate(new DateOnly(2024, 6, 7))
                                                            .WithInterval(1)
                                                            .WithByWeekDay(null)
                                                            .WithByMonthDay(31)
                                                            .WithByWeekDay([])
                                                            .Build();

        Event event6 = new EventBuilder()
                       .WithId(6)
                       .WithTitle("Test1")
                       .WithDescription("Test1")
                       .WithLocation("Test1")
                       .WithDuration(new Duration(2, 3))
                       .WithRecurrencePattern(monthlyRecurrencePattern1)
                       .WithEventCollaborators(new EventCollaboratorListBuilder(6)
                                               .WithOrganizer(user1, new DateOnly(2024, 6, 7))
                                               .Build())
                       .Build();

        MonthlyRecurrencePattern monthlyRecurrencePattern2 = new MonthlyRecurrencePatternBuilder()
                                                            .WithStartDate(new DateOnly(2024, 6, 7))
                                                            .WithEndDate(new DateOnly(2024, 6, 7))
                                                            .WithInterval(1)
                                                            .WithByWeekDay([7])
                                                            .WithByMonthDay(null)
                                                            .WithWeekOrder(5)
                                                            .Build();

        Event event7 = new EventBuilder()
                       .WithId(7)
                       .WithTitle("Test1")
                       .WithDescription("Test1")
                       .WithLocation("Test1")
                       .WithDuration(new Duration(2, 3))
                       .WithRecurrencePattern(monthlyRecurrencePattern2)
                       .WithEventCollaborators(new EventCollaboratorListBuilder(7)
                                               .WithOrganizer(user1, new DateOnly(2024, 6, 7))
                                               .Build())
                       .Build();

        YearlyRecurrencePattern yearlyRecurrencePattern1 = new YearlyRecurrencePatternBuilder()
                                                           .WithStartDate(new DateOnly(2024, 6, 7))
                                                           .WithEndDate(new DateOnly(2024, 6, 7))
                                                           .WithInterval(1)
                                                           .WithByWeekDay(null)
                                                           .WithByMonthDay(31)
                                                           .WithByMonth(12)
                                                           .WithWeekOrder(5)
                                                           .WithByWeekDay([])
                                                           .Build();

        Event event8 = new EventBuilder()
                       .WithId(8)
                       .WithTitle("Test1")
                       .WithDescription("Test1")
                       .WithLocation("Test1")
                       .WithDuration(new Duration(2, 3))
                       .WithRecurrencePattern(yearlyRecurrencePattern1)
                       .WithEventCollaborators(new EventCollaboratorListBuilder(8)
                                               .WithOrganizer(user1, new DateOnly(2024, 6, 7))
                                               .Build())
                       .Build();

        YearlyRecurrencePattern yearlyRecurrencePattern2 = new YearlyRecurrencePatternBuilder()
                                                            .WithStartDate(new DateOnly(2024, 6, 7))
                                                            .WithEndDate(new DateOnly(2024, 6, 7))
                                                            .WithInterval(1)
                                                            .WithByWeekDay([7])
                                                            .WithByMonthDay(null)
                                                            .WithByMonth(12)
                                                            .WithWeekOrder(5)
                                                            .Build();

        Event event9 = new EventBuilder()
                       .WithId(9)
                       .WithTitle("Test1")
                       .WithDescription("Test1")
                       .WithLocation("Test1")
                       .WithDuration(new Duration(2, 3))
                       .WithRecurrencePattern(yearlyRecurrencePattern2)
                       .WithEventCollaborators(new EventCollaboratorListBuilder(9)
                                               .WithOrganizer(user1, new DateOnly(2024, 6, 7))
                                               .WithParticipant(user2, ConfirmationStatus.Proposed, new DateOnly(2024, 6, 7), new Duration(3, 4))
                                               .Build())
                       .Build();

        List<Event> expectedResult = [event1, event2, event3, event4, event5, event6, event7, event8, event9];

        EventRepository eventRepository = new(_dbContextEvent, _mapper);

        //Act
        List<Event> actualResult = await eventRepository.GetAllEventsByUserId(userId);

        //Assert
        actualResult.Should().BeEquivalentTo(expectedResult, options => options.For(e => e.EventCollaborators).Exclude(e => e.Id));
    }
}
