using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Core.Entities.RecurrecePattern;
using FluentAssertions;
using UnitTests.Builders.EntityBuilder;
using Infrastructure.DataModels;
using UnitTests.Builders.DataModelBuilder;

namespace UnitTests.Infrastructure.Repositories.EventRepositoryTests;

public class GetSharedEvents : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContextEvent;
    private readonly IMapper _mapper;

    public GetSharedEvents(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_Return_ListOfEvents_When_SharedCalendarAvailableWithGivenId()
    {
        //Arrange
        _dbContextEvent = await new EventRepositoryDBContext().GetDatabaseContext();

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

        SharedCalendarDataModel sharedCalendarDataModel = new SharedCalendarDataModelBuilder()
                                                          .WithId(1)
                                                          .WithSenderId(1)
                                                          .WithReceiverId(2)
                                                          .WithFromDate(new DateOnly(2024, 6, 7))
                                                          .WithToDate(new DateOnly(2024, 6, 7))
                                                          .Build();

        await new DatabaseBuilder(_dbContextEvent)
             .WithUser(userDataModel1)
             .WithUser(userDataModel2)
             .WithSharedCalendar(sharedCalendarDataModel)
             .WithEvent(eventDataModel1)
             .Build();

        User user1 = new UserBuilder(1)
            .WithName("a")
            .WithEmail("a@gmail.com")
            .Build();

        User user2 = new UserBuilder(2)
                    .WithName("b")
                    .WithEmail("b@gmail.com")
                    .Build();

        SingleInstanceRecurrencePattern singleInstanceRecurrencePattern = new SingleInstanceRecurrencePatternBuilder()
                                                                  .WithStartDate(new DateOnly(2024, 6, 7))
                                                                  .WithEndDate(new DateOnly(2024, 6, 7))
                                                                  .WithInterval(1)
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


        List<Event> expectedResult = [event1];

        SharedCalendar sharedCalendar = new(
            1,
            user1,
            user2,
            new DateOnly(2024, 6, 7),
            new DateOnly(2024, 6, 7));

        EventRepository eventRepository = new(_dbContextEvent, _mapper);

        //Act
        List<Event> actualResult = await eventRepository.GetSharedEvents(sharedCalendar);

        //Assert
        actualResult.Should().BeEquivalentTo(expectedResult, option => option.For(e => e.EventCollaborators).Exclude(e => e.Id));
    }
}
