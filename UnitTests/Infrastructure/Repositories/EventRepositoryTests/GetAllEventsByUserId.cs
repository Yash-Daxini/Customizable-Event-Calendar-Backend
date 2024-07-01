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
    public async Task Should_Return_EventList_When_EventAvailableWithGivenUserId(int userId)
    {
        //Arrange

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

        List<EventCollaboratorDataModel> eventCollaboratorDataModels1 = new EventCollaboratorDataModelListBuilder(1)
                                                                       .WithOrganizer(1, new DateOnly(2024, 6, 7))
                                                                       .Build();

        EventDataModel eventDataModel1 = new EventDataModelBuilder()
                                        .WithTitle("Test")
                                        .WithDescription("Test")
                                        .WithLocation("Test")
                                        .WithUserId(1)
                                        .WithStartHour(1)
                                        .WithEndHour(2)
                                        .WithStartDate(new DateOnly(2024, 6, 7))
                                        .WithEndDate(new DateOnly(2024, 6, 7))
                                        .WithFrequency("None")
                                        .WithInterval(1)
                                        .WithByMonth(null)
                                        .WithByMonthDay(null)
                                        .WithWeekOrder(null)
                                        .WithEventCollaborators(eventCollaboratorDataModels1)
                                        .Build();

        List<EventCollaboratorDataModel> eventCollaboratorDataModels2 = new EventCollaboratorDataModelListBuilder(2)
                                                                       .WithOrganizer(1, new DateOnly(2024, 6, 7))
                                                                       .WithParticipant(2, "Pending", new DateOnly(2024, 6, 7), null, null)
                                                                       .Build();

        EventDataModel eventDataModel2 = new EventDataModelBuilder()
                                        .WithTitle("Test")
                                        .WithDescription("Test")
                                        .WithLocation("Test")
                                        .WithUserId(1)
                                        .WithStartHour(1)
                                        .WithEndHour(2)
                                        .WithStartDate(new DateOnly(2024, 6, 7))
                                        .WithEndDate(new DateOnly(2024, 6, 7))
                                        .WithFrequency("Daily")
                                        .WithInterval(1)
                                        .WithByMonth(null)
                                        .WithByMonthDay(null)
                                        .WithWeekOrder(null)
                                        .WithEventCollaborators(eventCollaboratorDataModels2)
                                        .Build();

        List<EventCollaboratorDataModel> eventCollaboratorDataModels3 = new EventCollaboratorDataModelListBuilder(3)
                                                                       .WithOrganizer(1, new DateOnly(2024, 6, 7))
                                                                       .Build();

        EventDataModel eventDataModel3 = new EventDataModelBuilder()
                                        .WithTitle("Test")
                                        .WithDescription("Test")
                                        .WithLocation("Test")
                                        .WithUserId(1)
                                        .WithStartHour(1)
                                        .WithEndHour(2)
                                        .WithStartDate(new DateOnly(2024, 6, 7))
                                        .WithEndDate(new DateOnly(2024, 6, 7))
                                        .WithFrequency("Daily")
                                        .WithInterval(1)
                                        .WithByWeekDay("1,2")
                                        .WithByMonth(null)
                                        .WithByMonthDay(null)
                                        .WithWeekOrder(null)
                                        .WithEventCollaborators(eventCollaboratorDataModels3)
                                        .Build();

        List<EventCollaboratorDataModel> eventCollaboratorDataModels4 = new EventCollaboratorDataModelListBuilder(4)
                                                                       .WithOrganizer(1, new DateOnly(2024, 6, 7))
                                                                       .Build();

        EventDataModel eventDataModel4 = new EventDataModelBuilder()
                                        .WithTitle("Test")
                                        .WithDescription("Test")
                                        .WithLocation("Test")
                                        .WithUserId(1)
                                        .WithStartHour(1)
                                        .WithEndHour(2)
                                        .WithStartDate(new DateOnly(2024, 6, 7))
                                        .WithEndDate(new DateOnly(2024, 6, 7))
                                        .WithFrequency("Weekly")
                                        .WithInterval(1)
                                        .WithByWeekDay("1,2")
                                        .WithByMonth(null)
                                        .WithByMonthDay(null)
                                        .WithWeekOrder(null)
                                        .WithEventCollaborators(eventCollaboratorDataModels4)
                                        .Build();

        List<EventCollaboratorDataModel> eventCollaboratorDataModels5 = new EventCollaboratorDataModelListBuilder(5)
                                                               .WithOrganizer(1, new DateOnly(2024, 6, 7))
                                                               .Build();

        EventDataModel eventDataModel5 = new EventDataModelBuilder()
                                        .WithTitle("Test")
                                        .WithDescription("Test")
                                        .WithLocation("Test")
                                        .WithUserId(1)
                                        .WithStartHour(1)
                                        .WithEndHour(2)
                                        .WithStartDate(new DateOnly(2024, 6, 7))
                                        .WithEndDate(new DateOnly(2024, 6, 7))
                                        .WithFrequency("Weekly")
                                        .WithInterval(1)
                                        .WithByWeekDay(null)
                                        .WithByMonth(null)
                                        .WithByMonthDay(null)
                                        .WithWeekOrder(null)
                                        .WithEventCollaborators(eventCollaboratorDataModels5)
                                        .Build();

        List<EventCollaboratorDataModel> eventCollaboratorDataModels6 = new EventCollaboratorDataModelListBuilder(6)
                                                               .WithOrganizer(1, new DateOnly(2024, 6, 7))
                                                               .Build();

        EventDataModel eventDataModel6 = new EventDataModelBuilder()
                                        .WithTitle("Test")
                                        .WithDescription("Test")
                                        .WithLocation("Test")
                                        .WithUserId(1)
                                        .WithStartHour(1)
                                        .WithEndHour(2)
                                        .WithStartDate(new DateOnly(2024, 6, 7))
                                        .WithEndDate(new DateOnly(2024, 6, 7))
                                        .WithFrequency("Monthly")
                                        .WithInterval(1)
                                        .WithByWeekDay(null)
                                        .WithByMonth(null)
                                        .WithByMonthDay(31)
                                        .WithWeekOrder(null)
                                        .WithEventCollaborators(eventCollaboratorDataModels6)
                                        .Build();

        List<EventCollaboratorDataModel> eventCollaboratorDataModels7 = new EventCollaboratorDataModelListBuilder(7)
                                                               .WithOrganizer(1, new DateOnly(2024, 6, 7))
                                                               .Build();

        EventDataModel eventDataModel7 = new EventDataModelBuilder()
                                        .WithTitle("Test")
                                        .WithDescription("Test")
                                        .WithLocation("Test")
                                        .WithUserId(1)
                                        .WithStartHour(1)
                                        .WithEndHour(2)
                                        .WithStartDate(new DateOnly(2024, 6, 7))
                                        .WithEndDate(new DateOnly(2024, 6, 7))
                                        .WithFrequency("Monthly")
                                        .WithInterval(1)
                                        .WithByWeekDay("7")
                                        .WithByMonth(null)
                                        .WithByMonthDay(null)
                                        .WithWeekOrder(5)
                                        .WithEventCollaborators(eventCollaboratorDataModels7)
                                        .Build();

        List<EventCollaboratorDataModel> eventCollaboratorDataModels8 = new EventCollaboratorDataModelListBuilder(8)
                                                               .WithOrganizer(1, new DateOnly(2024, 6, 7))
                                                               .Build();

        EventDataModel eventDataModel8 = new EventDataModelBuilder()
                                        .WithTitle("Test")
                                        .WithDescription("Test")
                                        .WithLocation("Test")
                                        .WithUserId(1)
                                        .WithStartHour(1)
                                        .WithEndHour(2)
                                        .WithStartDate(new DateOnly(2024, 6, 7))
                                        .WithEndDate(new DateOnly(2024, 6, 7))
                                        .WithFrequency("Yearly")
                                        .WithInterval(1)
                                        .WithByWeekDay(null)
                                        .WithByMonth(12)
                                        .WithByMonthDay(31)
                                        .WithWeekOrder(null)
                                        .WithEventCollaborators(eventCollaboratorDataModels8)
                                        .Build();

        List<EventCollaboratorDataModel> eventCollaboratorDataModels9 = new EventCollaboratorDataModelListBuilder(9)
                                                                        .WithOrganizer(1, new DateOnly(2024, 6, 7))
                                                                        .WithParticipant(2, "Proposed", new DateOnly(2024, 6, 7), 3, 4)
                                                               .Build();

        EventDataModel eventDataModel9 = new EventDataModelBuilder()
                                        .WithTitle("Test")
                                        .WithDescription("Test")
                                        .WithLocation("Test")
                                        .WithUserId(1)
                                        .WithStartHour(1)
                                        .WithEndHour(2)
                                        .WithStartDate(new DateOnly(2024, 6, 7))
                                        .WithEndDate(new DateOnly(2024, 6, 7))
                                        .WithFrequency("Yearly")
                                        .WithInterval(1)
                                        .WithByWeekDay("7")
                                        .WithByMonth(null)
                                        .WithByMonthDay(null)
                                        .WithWeekOrder(5)
                                        .WithEventCollaborators(eventCollaboratorDataModels9)
                                        .Build();


        _dbContext = new DatabaseBuilder()
                 .WithUser(userDataModel1)
                 .WithUser(userDataModel2)
                 .WithEvent(eventDataModel1)
                 .WithEvent(eventDataModel2)
                 .WithEvent(eventDataModel3)
                 .WithEvent(eventDataModel4)
                 .WithEvent(eventDataModel5)
                 .WithEvent(eventDataModel6)
                 .WithEvent(eventDataModel7)
                 .WithEvent(eventDataModel8)
                 .WithEvent(eventDataModel9)
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

        User user2 = new UserBuilder(2)
                    .WithName("b")
                    .WithEmail("b@gmail.com")
                    .Build();

        Event event1 = new EventBuilder()
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

        DailyRecurrencePattern dailyRecurrencePattern1 = new DailyRecurrencePatternBuilder()
                                                         .WithStartDate(new DateOnly(2024, 6, 7))
                                                         .WithEndDate(new DateOnly(2024, 6, 7))
                                                         .WithInterval(1)
                                                         .WithByWeekDay([])
                                                         .Build();

        Event event2 = new EventBuilder()
                       .WithId(2)
                       .WithTitle("Test")
                       .WithDescription("Test")
                       .WithLocation("Test")
                       .WithDuration(new Duration(1, 2))
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
                       .WithTitle("Test")
                       .WithDescription("Test")
                       .WithLocation("Test")
                       .WithDuration(new Duration(1, 2))
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
                       .WithTitle("Test")
                       .WithDescription("Test")
                       .WithLocation("Test")
                       .WithDuration(new Duration(1, 2))
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
                       .WithTitle("Test")
                       .WithDescription("Test")
                       .WithLocation("Test")
                       .WithDuration(new Duration(1, 2))
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
                       .WithTitle("Test")
                       .WithDescription("Test")
                       .WithLocation("Test")
                       .WithDuration(new Duration(1, 2))
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
                       .WithTitle("Test")
                       .WithDescription("Test")
                       .WithLocation("Test")
                       .WithDuration(new Duration(1, 2))
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
                       .WithTitle("Test")
                       .WithDescription("Test")
                       .WithLocation("Test")
                       .WithDuration(new Duration(1, 2))
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
                       .WithTitle("Test")
                       .WithDescription("Test")
                       .WithLocation("Test")
                       .WithDuration(new Duration(1, 2))
                       .WithRecurrencePattern(yearlyRecurrencePattern2)
                       .WithEventCollaborators(new EventCollaboratorListBuilder(9)
                                               .WithOrganizer(user1, new DateOnly(2024, 6, 7))
                                               .WithParticipant(user2, ConfirmationStatus.Proposed, new DateOnly(2024, 6, 7), new Duration(3, 4))
                                               .Build())
                       .Build();

        List<Event> expectedResult = [event1, event2, event3, event4, event5, event6, event7, event8, event9];

        EventRepository eventRepository = new(_dbContext, _mapper);

        //Act
        List<Event> actualResult = await eventRepository.GetAllEventsByUserId(userId);

        //Assert
        actualResult.Should().BeEquivalentTo(expectedResult, options => options.For(e => e.EventCollaborators).Exclude(e => e.Id));
    }
}
