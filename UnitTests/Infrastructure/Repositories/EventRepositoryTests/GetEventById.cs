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

public class GetEventById : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;

    public GetEventById(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_Return_Event_When_EventAvailableWithGivenId()
    {
        _dbContext = await new EventRepositoryDBContext().GetDatabaseContext();

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

        _dbContext = new DatabaseBuilder()
             .WithUser(userDataModel1)
             .WithEvent(eventDataModel1)
             .Build();

        EventRepository eventRepository = new(_dbContext, _mapper);

        SingleInstanceRecurrencePattern singleInstanceRecurrencePattern = new SingleInstanceRecurrencePatternBuilder()
                                                                          .WithStartDate(new DateOnly(2024, 6, 7))
                                                                          .WithEndDate(new DateOnly(2024, 6, 7))
                                                                          .WithInterval(1)
                                                                          .Build();

        User user = new UserBuilder(1)
                    .WithName("a")
                    .WithEmail("a@gmail.com")
                    .Build();

        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(1)
                                                     .WithOrganizer(user, new DateOnly(2024, 6, 7))
                                                     .Build();

        Event expectedResult = new EventBuilder()
                               .WithId(1)
                               .WithTitle("Test")
                               .WithDescription("Test")
                               .WithLocation("Test")
                               .WithDuration(new Duration(1, 2))
                               .WithRecurrencePattern(singleInstanceRecurrencePattern)
                               .WithEventCollaborators(eventCollaborators)
                               .Build();


        Event? actualResult = await eventRepository.GetEventById(1);

        actualResult.Should().BeEquivalentTo(expectedResult,option => option.For(e => e.EventCollaborators).Exclude(e => e.Id));
    }

    [Fact]
    public async Task Should_Return_Null_When_EventNotAvailableWithGivenId()
    {
        _dbContext = await new EventRepositoryDBContext().GetDatabaseContext();

        EventRepository eventRepository = new(_dbContext, _mapper);

        Event? actualResult = await eventRepository.GetEventById(10);

        Assert.Null(actualResult);
    }
}
