using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Core.Entities.RecurrecePattern;
using FluentAssertions;
using UnitTests.Builders;
using Core.Entities.Enums;

namespace UnitTests.Infrastructure.Repositories.EventRepositoryTests;

public class UpdateEvent : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContextEvent;
    private readonly IMapper _mapper;

    public UpdateEvent(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_UpdateEvent_When_EventWithIdAvailable()
    {
        _dbContextEvent = await new EventRepositoryDBContext().GetDatabaseContext();

        EventRepository eventRepository = new(_dbContextEvent, _mapper);

        EventCollaboratorRepository eventCollaboratorRepository = new(_dbContextEvent, _mapper);

        _dbContextEvent.ChangeTracker.Clear();

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
                                                      .WithParticipant(user2,
                                                                       ConfirmationStatus.Proposed,
                                                                       new DateOnly(2024, 6, 7),
                                                                       new Duration(1, 2))
                                                      .Build();


        Event eventToUpdate = new EventBuilder()
                              .WithId(1)
                              .WithTitle("Test1")
                              .WithDescription("Test1")
                              .WithLocation("Test1")
                              .WithDuration(new Duration(3, 4))
                              .WithRecurrencePattern(singleInstanceRecurrencePattern)
                              .WithEventCollaborators(eventCollaborators1)
                              .Build();


        await eventCollaboratorRepository.DeleteEventCollaboratorsByEventId(1);

        await eventRepository.Update(eventToUpdate);

        Event? updatedEvent = await eventRepository.GetEventById(1);

        eventToUpdate.Id = 1;

        updatedEvent.Should().BeEquivalentTo(eventToUpdate, option => option.For(e => e.EventCollaborators).Exclude(e => e.Id));
    }
}
