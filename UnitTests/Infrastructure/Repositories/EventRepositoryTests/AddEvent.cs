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

public class AddEvent : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;
    private readonly User _user1;
    private readonly User _user2;
    private readonly List<EventCollaborator> _eventCollaborators;

    public AddEvent(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _user1 = new UserBuilder(1)
                     .WithName("a")
                     .WithEmail("a@gmail.com")
                     .WithPassword("c")
                     .Build();

        _user2 = new UserBuilder(2)
                     .WithName("b")
                     .WithEmail("b@gmail.com")
                     .WithPassword("b")
                     .Build();

        _eventCollaborators = new EventCollaboratorListBuilder(0)
                              .WithOrganizer(_user1, new DateOnly(2024, 6, 8))
                              .WithParticipant(_user2, ConfirmationStatus.Pending, new DateOnly(2024, 6, 8), null)
                              .Build();
    }

    [Fact]
    public async Task Should_Return_AddedEventId_When_EventOccurSingleTime()
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

        EventRepository eventRepository = new(_dbContext, _mapper);

        SingleInstanceRecurrencePattern singleInstanceRecurrencePattern = new SingleInstanceRecurrencePatternBuilder()
                                                  .WithStartDate(new DateOnly(2024, 6, 8))
                                                  .WithEndDate(new DateOnly(2024, 6, 8))
                                                  .WithInterval(1)
                                                  .Build();

        Event eventToAdd = new EventBuilder()
                           .WithTitle("Test2")
                           .WithDescription("Test2")
                           .WithLocation("Test2")
                           .WithDuration(new Duration(3, 4))
                           .WithRecurrencePattern(singleInstanceRecurrencePattern)
                           .WithEventCollaborators(_eventCollaborators)
                           .Build();

        int eventId = await eventRepository.Add(eventToAdd);

        eventToAdd.Id = eventId;

        eventId.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Should_Return_AddedEventId_When_EventIsDailyRecurring()
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

        EventRepository eventRepository = new(_dbContext, _mapper);

        DailyRecurrencePattern dailyRecurrencePattern = new DailyRecurrencePatternBuilder()
                                                        .WithStartDate(new DateOnly(2024, 6, 8))
                                                        .WithEndDate(new DateOnly(2024, 6, 8))
                                                        .WithInterval(1)
                                                        .WithByWeekDay(null)
                                                        .Build();

        Event eventToAdd = new EventBuilder()
                           .WithTitle("Test2")
                           .WithDescription("Test2")
                           .WithLocation("Test2")
                           .WithDuration(new Duration(3, 4))
                           .WithRecurrencePattern(dailyRecurrencePattern)
                           .WithEventCollaborators(_eventCollaborators)
                           .Build();

        int eventId = await eventRepository.Add(eventToAdd);

        eventToAdd.Id = eventId;

        eventId.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Should_Return_AddedEventId_When_EventIsDailyRecurringWithWeekDay()
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

        EventRepository eventRepository = new(_dbContext, _mapper);

        DailyRecurrencePattern dailyRecurrencePattern = new DailyRecurrencePatternBuilder()
                                                        .WithStartDate(new DateOnly(2024, 6, 8))
                                                        .WithEndDate(new DateOnly(2024, 6, 8))
                                                        .WithInterval(1)
                                                        .WithByWeekDay([1, 2])
                                                        .Build();

        Event eventToAdd = new EventBuilder()
                           .WithTitle("Test2")
                           .WithDescription("Test2")
                           .WithLocation("Test2")
                           .WithDuration(new Duration(3, 4))
                           .WithRecurrencePattern(dailyRecurrencePattern)
                           .WithEventCollaborators(_eventCollaborators)
                           .Build();

        int eventId = await eventRepository.Add(eventToAdd);

        eventToAdd.Id = eventId;

        eventId.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Should_Return_AddedEventId_When_EventIsWeeklyRecurring()
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

        EventRepository eventRepository = new(_dbContext, _mapper);

        WeeklyRecurrencePattern weeklyRecurrencePattern = new WeeklyRecurrencePatternBuilder()
                                                        .WithStartDate(new DateOnly(2024, 6, 8))
                                                        .WithEndDate(new DateOnly(2024, 6, 8))
                                                        .WithInterval(1)
                                                        .WithByWeekDay([1, 2])
                                                        .Build();

        Event eventToAdd = new EventBuilder()
                           .WithTitle("Test2")
                           .WithDescription("Test2")
                           .WithLocation("Test2")
                           .WithDuration(new Duration(3, 4))
                           .WithRecurrencePattern(weeklyRecurrencePattern)
                           .WithEventCollaborators(_eventCollaborators)
                           .Build();

        int eventId = await eventRepository.Add(eventToAdd);

        eventToAdd.Id = eventId;

        eventId.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Should_Return_AddedEventId_When_EventIsMonthlyRecurring()
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

        EventRepository eventRepository = new(_dbContext, _mapper);

        MonthlyRecurrencePattern monthlyRecurrencePattern = new MonthlyRecurrencePatternBuilder()
                                                        .WithStartDate(new DateOnly(2024, 6, 8))
                                                        .WithEndDate(new DateOnly(2024, 6, 8))
                                                        .WithInterval(1)
                                                        .WithByMonthDay(31)
                                                        .WithWeekOrder(null)
                                                        .WithByWeekDay(null)
                                                        .Build();

        Event eventToAdd = new EventBuilder()
                           .WithTitle("Test2")
                           .WithDescription("Test2")
                           .WithLocation("Test2")
                           .WithDuration(new Duration(3, 4))
                           .WithRecurrencePattern(monthlyRecurrencePattern)
                           .WithEventCollaborators(_eventCollaborators)
                           .Build();

        int eventId = await eventRepository.Add(eventToAdd);

        eventToAdd.Id = eventId;

        eventId.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Should_Return_AddedEventId_When_EventIsYearlyRecurring()
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

        EventRepository eventRepository = new(_dbContext, _mapper);

        YearlyRecurrencePattern yearlyRecurrencePattern = new YearlyRecurrencePatternBuilder()
                                                .WithStartDate(new DateOnly(2024, 6, 8))
                                                .WithEndDate(new DateOnly(2024, 6, 8))
                                                .WithInterval(1)
                                                .WithByWeekDay(null)
                                                .WithWeekOrder(null)
                                                .WithByMonth(12)
                                                .WithByMonthDay(31)
                                                .Build();

        Event eventToAdd = new EventBuilder()
                           .WithTitle("Test2")
                           .WithDescription("Test2")
                           .WithLocation("Test2")
                           .WithDuration(new Duration(3, 4))
                           .WithRecurrencePattern(yearlyRecurrencePattern)
                           .WithEventCollaborators(_eventCollaborators)
                           .Build();

        int eventId = await eventRepository.Add(eventToAdd);

        eventToAdd.Id = eventId;

        eventId.Should().BeGreaterThan(0);
    }

}
