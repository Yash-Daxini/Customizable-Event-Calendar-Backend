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

public class GetEventsWithinGivenDate : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContextEvent;
    private readonly IMapper _mapper;

    public GetEventsWithinGivenDate(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Theory]
    [InlineData("6-6-24", "10-6-24", 1)]
    public async Task Should_Return_ListOfEvents_When_EventOccurWithInGivenDate(string startDate, string endDate, int userId)
    {
        _dbContextEvent = await new EventRepositoryDBContext().GetDatabaseContext();

        UserDataModel userDataModel1 = new UserDataModelBuilder()
                              .WithId(1)
                              .WithUserName("a")
                              .WithEmail("a@gmail.com")
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

        List<EventCollaboratorDataModel> eventCollaboratorDataModels2 = new EventCollaboratorDataModelListBuilder(1)
                                                                       .WithOrganizer(1, new DateOnly(2024, 6, 5))
                                                                       .Build();

        EventDataModel eventDataModel2 = new EventDataModelBuilder()
                                        .WithTitle("Test")
                                        .WithDescription("Test")
                                        .WithLocation("Test")
                                        .WithUserId(1)
                                        .WithStartHour(1)
                                        .WithEndHour(2)
                                        .WithStartDate(new DateOnly(2024, 6, 5))
                                        .WithEndDate(new DateOnly(2024, 6, 5))
                                        .WithFrequency("None")
                                        .WithInterval(1)
                                        .WithByMonth(null)
                                        .WithByMonthDay(null)
                                        .WithWeekOrder(null)
                                        .WithEventCollaborators(eventCollaboratorDataModels2)
                                        .Build();

        await new DatabaseBuilder(_dbContextEvent)
             .WithUser(userDataModel1)
             .WithEvent(eventDataModel1)
             .WithEvent(eventDataModel2)
             .Build();

        DateOnly start = DateOnly.Parse(startDate);
        DateOnly end = DateOnly.Parse(endDate);

        User user1 = new UserBuilder(1)
                    .WithName("a")
                    .WithEmail("a@gmail.com")
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

        EventRepository eventRepository = new(_dbContextEvent, _mapper);

        List<Event> actualResult = await eventRepository.GetEventsWithinGivenDateByUserId(userId, start, end);

        actualResult.Should().BeEquivalentTo(expectedResult, option => option.For(e => e.EventCollaborators).Exclude(e => e.Id));
    }
}
