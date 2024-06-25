using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Core.Entities.RecurrecePattern;
using FluentAssertions;
using UnitTests.Builders;

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
    public async Task Should_ReturnListOfEvents_When_SharedCalendarAvailableWithGivenId()
    {
        //Arrange
        _dbContextEvent = await new EventRepositoryDBContext().GetDatabaseContext();

        User user1 = new UserBuilder(1)
                     .WithName("a")
                     .WithEmail("a")
                     .Build();

        User user2 = new UserBuilder(2)
                     .WithName("b")
                     .WithEmail("b")
                     .Build();

        SingleInstanceRecurrencePattern singleInstanceRecurrencePattern = new SingleInstanceRecurrencePatternBuilder()
                                                                          .WithStartDate(new DateOnly(2024, 6, 7))
                                                                          .WithEndDate(new DateOnly(2024, 6, 7))
                                                                          .WithInterval(1)
                                                                          .Build();

        List<EventCollaborator> eventCollaborators1 = new EventCollaboratorListBuilder(1)
                                                      .WithOrganizer(user1, new DateOnly(2024, 6, 7))
                                                      .Build();

        Event event1 = new EventBuilder()
                       .WithId(1)
                       .WithTitle("Test")
                       .WithLocation("Test")
                       .WithDescription("Test")
                       .WithDuration(new Duration(1, 2))
                       .WithRecurrencePattern(singleInstanceRecurrencePattern)
                       .WithEventCollaborators(eventCollaborators1)
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
