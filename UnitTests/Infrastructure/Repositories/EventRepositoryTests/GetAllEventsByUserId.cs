using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Core.Entities.RecurrecePattern;
using FluentAssertions;
using UnitTests.Builders.EntityBuilder;
using Core.Entities.Enums;
using Infrastructure.DataModels;
using UnitTests.Builders.DataModelBuilder;

namespace UnitTests.Infrastructure.Repositories.EventRepositoryTests;

public class GetAllEventsByUserId : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;

    public GetAllEventsByUserId(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Theory]
    [InlineData(1)]
    public async Task Should_Return_EventList_When_EventAvailableWithGivenUserIdAndEventIsNonRecurringEvent(int userId)
    {
        //Arrange

        UserDataModel userDataModel1 = new UserDataModelBuilder()
                              .WithId(1)
                              .WithUserName("a")
                              .WithEmail("a@gmail.com")
                              .Build();


        _dbContext = new DatabaseBuilder()
                     .WithUser(userDataModel1)
                     .Build();

        SingleInstanceRecurrencePattern singleInstanceRecurrencePattern = new SingleInstanceRecurrencePatternBuilder()
                                                                          .WithStartDate(new DateOnly(2024, 6, 7))
                                                                          .WithEndDate(new DateOnly(2024, 6, 7))
                                                                          .WithInterval(1)
                                                                          .Build();

        User user1 = new UserBuilder(1)
                    .WithName("a")
                    .WithEmail("a@gmail.com")
                    .Build();

        Event nonRecurringEvent = new EventBuilder()
                                .WithId(1)
                                .WithTitle("Test")
                                .WithDescription("Test")
                                .WithLocation("Test")
                                .WithDuration(new Duration(1, 2))
                                .WithRecurrencePattern(singleInstanceRecurrencePattern)
                                .WithEventCollaborators(new EventCollaboratorListBuilder(1)
                                                        .WithOrganizer(user1, new DateOnly(2024, 6, 7))
                                                        .Build())
                                .Build();


        List<Event> expectedResult = [nonRecurringEvent];

        EventRepository eventRepository = new(_dbContext, _mapper);

        await eventRepository.Add(nonRecurringEvent);

        //Act
        List<Event> actualResult = await eventRepository.GetAllEventsByUserId(userId);

        //Assert
        actualResult.Should().BeEquivalentTo(expectedResult, options => options.For(e => e.EventCollaborators).Exclude(e => e.Id));
    }

    [Theory]
    [InlineData(1)]
    public async Task Should_Return_EventList_When_EventAvailableWithGivenUserIdAndEventIsDailyRecurringEvent(int userId)
    {
        UserDataModel userDataModel1 = new UserDataModelBuilder()
                              .WithId(1)
                              .WithUserName("a")
                              .WithEmail("a@gmail.com")
                              .Build();

        UserDataModel userDataModel2 = new UserDataModelBuilder()
                                      .WithId(2)
                                      .WithUserName("b")
                                      .WithEmail("b@gmail.com")
                                      .Build();


        _dbContext = new DatabaseBuilder()
                    .WithUser(userDataModel1)
                    .WithUser(userDataModel2)
                    .Build();

        User user1 = new UserBuilder(1)
                    .WithName("a")
                    .WithEmail("a@gmail.com")
                    .Build();

        User user2 = new UserBuilder(2)
                    .WithName("b")
                    .WithEmail("b@gmail.com")
                    .Build();

        DailyRecurrencePattern dailyRecurrencePatternWithoutWeekDay = new DailyRecurrencePatternBuilder()
                                                                      .WithStartDate(new DateOnly(2024, 6, 7))
                                                                      .WithEndDate(new DateOnly(2024, 6, 7))
                                                                      .WithInterval(1)
                                                                      .WithByWeekDay([])
                                                                      .Build();

        Event dailyEventWithoutWeekDay = new EventBuilder()
                                        .WithId(2)
                                        .WithTitle("Test")
                                        .WithDescription("Test")
                                        .WithLocation("Test")
                                        .WithDuration(new Duration(1, 2))
                                        .WithRecurrencePattern(dailyRecurrencePatternWithoutWeekDay)
                                        .WithEventCollaborators(new EventCollaboratorListBuilder(2)
                                                                .WithOrganizer(user1, new DateOnly(2024, 6, 7))
                                                                .WithParticipant(user2, ConfirmationStatus.Pending, new DateOnly(2024, 6, 7), null)
                                                                .Build())
                                        .Build();

        DailyRecurrencePattern dailyRecurrencePatternWithWeekDay = new DailyRecurrencePatternBuilder()
                                                         .WithStartDate(new DateOnly(2024, 6, 7))
                                                         .WithEndDate(new DateOnly(2024, 6, 7))
                                                         .WithInterval(1)
                                                         .WithByWeekDay([1, 2])
                                                         .Build();

        Event dailyEventWithWeekDay = new EventBuilder()
                                    .WithId(3)
                                    .WithTitle("Test")
                                    .WithDescription("Test")
                                    .WithLocation("Test")
                                    .WithDuration(new Duration(1, 2))
                                    .WithRecurrencePattern(dailyRecurrencePatternWithWeekDay)
                                    .WithEventCollaborators(new EventCollaboratorListBuilder(3)
                                                            .WithOrganizer(user1, new DateOnly(2024, 6, 7))
                                                            .Build())
                                    .Build();

        List<Event> expectedResult = [dailyEventWithoutWeekDay, dailyEventWithWeekDay];

        EventRepository eventRepository = new(_dbContext, _mapper);

        await eventRepository.Add(dailyEventWithWeekDay);

        await eventRepository.Add(dailyEventWithoutWeekDay);

        //Act
        List<Event> actualResult = await eventRepository.GetAllEventsByUserId(userId);

        //Assert
        actualResult.Should().BeEquivalentTo(expectedResult, options => options.For(e => e.EventCollaborators).Exclude(e => e.Id));
    }

    [Theory]
    [InlineData(1)]
    public async Task Should_Return_EventList_When_EventAvailableWithGivenUserIdAndEventIsWeeklyRecurringEvent(int userId)
    {
        UserDataModel userDataModel1 = new UserDataModelBuilder()
                              .WithId(1)
                              .WithUserName("a")
                              .WithEmail("a@gmail.com")
                              .Build();


        _dbContext = new DatabaseBuilder()
                    .WithUser(userDataModel1)
                    .Build();

        User user1 = new UserBuilder(1)
                    .WithName("a")
                    .WithEmail("a@gmail.com")
                    .Build();

        WeeklyRecurrencePattern weeklyRecurrencePatternWithWeekDay = new WeeklyRecurrencePatternBuilder()
                                                                    .WithStartDate(new DateOnly(2024, 6, 7))
                                                                    .WithEndDate(new DateOnly(2024, 6, 7))
                                                                    .WithInterval(1)
                                                                    .WithByWeekDay([1, 2])
                                                                    .Build();

        Event weeklyEventWithWeekDay = new EventBuilder()
                                       .WithId(4)
                                       .WithTitle("Test")
                                       .WithDescription("Test")
                                       .WithLocation("Test")
                                       .WithDuration(new Duration(1, 2))
                                       .WithRecurrencePattern(weeklyRecurrencePatternWithWeekDay)
                                       .WithEventCollaborators(new EventCollaboratorListBuilder(4)
                                                               .WithOrganizer(user1, new DateOnly(2024, 6, 7))
                                                               .Build())
                                       .Build();

        WeeklyRecurrencePattern weeklyRecurrencePatternWithEmptyWeekDay = new WeeklyRecurrencePatternBuilder()
                                                                        .WithStartDate(new DateOnly(2024, 6, 7))
                                                                        .WithEndDate(new DateOnly(2024, 6, 7))
                                                                        .WithInterval(1)
                                                                        .WithByWeekDay([])
                                                                        .Build();

        Event weeklyEventWithEmptyWeekDay = new EventBuilder()
                                           .WithId(5)
                                           .WithTitle("Test")
                                           .WithDescription("Test")
                                           .WithLocation("Test")
                                           .WithDuration(new Duration(1, 2))
                                           .WithRecurrencePattern(weeklyRecurrencePatternWithEmptyWeekDay)
                                           .WithEventCollaborators(new EventCollaboratorListBuilder(5)
                                                                   .WithOrganizer(user1, new DateOnly(2024, 6, 7))
                                                                   .Build())
                                           .Build();

        List<Event> expectedResult = [weeklyEventWithEmptyWeekDay, weeklyEventWithWeekDay];

        EventRepository eventRepository = new(_dbContext, _mapper);

        await eventRepository.Add(weeklyEventWithEmptyWeekDay);

        await eventRepository.Add(weeklyEventWithWeekDay);

        //Act
        List<Event> actualResult = await eventRepository.GetAllEventsByUserId(userId);

        //Assert
        actualResult.Should().BeEquivalentTo(expectedResult, options => options.For(e => e.EventCollaborators).Exclude(e => e.Id));
    }

    [Theory]
    [InlineData(1)]
    public async Task Should_Return_EventList_When_EventAvailableWithGivenUserIdAndEventIsMonthlyRecurringEvent(int userId)
    {
        UserDataModel userDataModel1 = new UserDataModelBuilder()
                              .WithId(1)
                              .WithUserName("a")
                              .WithEmail("a@gmail.com")
                              .Build();


        _dbContext = new DatabaseBuilder()
                     .WithUser(userDataModel1)
                     .Build();

        User user1 = new UserBuilder(1)
                    .WithName("a")
                    .WithEmail("a@gmail.com")
                    .Build();

        MonthlyRecurrencePattern monthlyRecurrencePatternUsingMonthDay = new MonthlyRecurrencePatternBuilder()
                                                                        .WithStartDate(new DateOnly(2024, 6, 7))
                                                                        .WithEndDate(new DateOnly(2024, 6, 7))
                                                                        .WithInterval(1)
                                                                        .WithByWeekDay(null)
                                                                        .WithByMonthDay(31)
                                                                        .WithByWeekDay([])
                                                                        .Build();

        Event monthlyEventUsingMonthDay = new EventBuilder()
                                        .WithId(6)
                                        .WithTitle("Test")
                                        .WithDescription("Test")
                                        .WithLocation("Test")
                                        .WithDuration(new Duration(1, 2))
                                        .WithRecurrencePattern(monthlyRecurrencePatternUsingMonthDay)
                                        .WithEventCollaborators(new EventCollaboratorListBuilder(6)
                                                                .WithOrganizer(user1, new DateOnly(2024, 6, 7))
                                                                .Build())
                                        .Build();

        MonthlyRecurrencePattern monthlyRecurrencePatternUsingWeekOrder = new MonthlyRecurrencePatternBuilder()
                                                                        .WithStartDate(new DateOnly(2024, 6, 7))
                                                                        .WithEndDate(new DateOnly(2024, 6, 7))
                                                                        .WithInterval(1)
                                                                        .WithByWeekDay([7])
                                                                        .WithByMonthDay(null)
                                                                        .WithWeekOrder(5)
                                                                        .Build();

        Event monthlyEventUsingWeekOrder = new EventBuilder()
                                           .WithId(7)
                                           .WithTitle("Test")
                                           .WithDescription("Test")
                                           .WithLocation("Test")
                                           .WithDuration(new Duration(1, 2))
                                           .WithRecurrencePattern(monthlyRecurrencePatternUsingWeekOrder)
                                           .WithEventCollaborators(new EventCollaboratorListBuilder(7)
                                                                   .WithOrganizer(user1, new DateOnly(2024, 6, 7))
                                                                   .Build())
                                           .Build();

        List<Event> expectedResult = [monthlyEventUsingMonthDay, monthlyEventUsingWeekOrder];

        EventRepository eventRepository = new(_dbContext, _mapper);

        await eventRepository.Add(monthlyEventUsingMonthDay);

        await eventRepository.Add(monthlyEventUsingWeekOrder);

        //Act
        List<Event> actualResult = await eventRepository.GetAllEventsByUserId(userId);

        //Assert
        actualResult.Should().BeEquivalentTo(expectedResult, options => options.For(e => e.EventCollaborators).Exclude(e => e.Id));
    }

    [Theory]
    [InlineData(1)]
    public async Task Should_Return_EventList_When_EventAvailableWithGivenUserIdAndEventIsYearlyRecurringEvent(int userId)
    {
        UserDataModel userDataModel1 = new UserDataModelBuilder()
                              .WithId(1)
                              .WithUserName("a")
                              .WithEmail("a@gmail.com")
                              .Build();

        UserDataModel userDataModel2 = new UserDataModelBuilder()
                                      .WithId(2)
                                      .WithUserName("b")
                                      .WithEmail("b@gmail.com")
                                      .Build();


        _dbContext = new DatabaseBuilder()
                    .WithUser(userDataModel1)
                    .WithUser(userDataModel2)
                    .Build();

        User user1 = new UserBuilder(1)
                    .WithName("a")
                    .WithEmail("a@gmail.com")
                    .Build();

        User user2 = new UserBuilder(2)
                    .WithName("b")
                    .WithEmail("b@gmail.com")
                    .Build();

        YearlyRecurrencePattern yearlyRecurrencePatternUsingMonthDay = new YearlyRecurrencePatternBuilder()
                                                                       .WithStartDate(new DateOnly(2024, 6, 7))
                                                                       .WithEndDate(new DateOnly(2024, 6, 7))
                                                                       .WithInterval(1)
                                                                       .WithByWeekDay(null)
                                                                       .WithByMonthDay(31)
                                                                       .WithByMonth(12)
                                                                       .WithWeekOrder(5)
                                                                       .WithByWeekDay([])
                                                                       .Build();

        Event yearlyEventUsingMonthDay = new EventBuilder()
                                        .WithId(8)
                                        .WithTitle("Test")
                                        .WithDescription("Test")
                                        .WithLocation("Test")
                                        .WithDuration(new Duration(1, 2))
                                        .WithRecurrencePattern(yearlyRecurrencePatternUsingMonthDay)
                                        .WithEventCollaborators(new EventCollaboratorListBuilder(8)
                                                                .WithOrganizer(user1, new DateOnly(2024, 6, 7))
                                                                .Build())
                                        .Build();

        YearlyRecurrencePattern yearlyRecurrencePatternUsingWeekOrder = new YearlyRecurrencePatternBuilder()
                                                                        .WithStartDate(new DateOnly(2024, 6, 7))
                                                                        .WithEndDate(new DateOnly(2024, 6, 7))
                                                                        .WithInterval(1)
                                                                        .WithByWeekDay([7])
                                                                        .WithByMonthDay(null)
                                                                        .WithByMonth(12)
                                                                        .WithWeekOrder(5)
                                                                        .Build();

        Event yearlyEventUsingWeekOrder = new EventBuilder()
                                        .WithId(9)
                                        .WithTitle("Test")
                                        .WithDescription("Test")
                                        .WithLocation("Test")
                                        .WithDuration(new Duration(1, 2))
                                        .WithRecurrencePattern(yearlyRecurrencePatternUsingWeekOrder)
                                        .WithEventCollaborators(new EventCollaboratorListBuilder(9)
                                                                .WithOrganizer(user1, new DateOnly(2024, 6, 7))
                                                                .WithParticipant(user2, ConfirmationStatus.Proposed, new DateOnly(2024, 6, 7), new Duration(3, 4))
                                                                .Build())
                                        .Build();

        List<Event> expectedResult = [yearlyEventUsingMonthDay, yearlyEventUsingWeekOrder];

        EventRepository eventRepository = new(_dbContext, _mapper);

        await eventRepository.Add(yearlyEventUsingMonthDay);

        await eventRepository.Add(yearlyEventUsingWeekOrder);

        //Act
        List<Event> actualResult = await eventRepository.GetAllEventsByUserId(userId);

        //Assert
        actualResult.Should().BeEquivalentTo(expectedResult, options => options.For(e => e.EventCollaborators).Exclude(e => e.Id));
    }
}
