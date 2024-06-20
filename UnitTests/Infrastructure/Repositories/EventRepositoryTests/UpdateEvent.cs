using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Core.Entities.RecurrecePattern;
using FluentAssertions;

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

        Event eventToUpdate = new()
        {
            Id = 1,
            Title = "Test1",
            Description = "Test1",
            Location = "Test1",
            Duration = new Duration(3, 4),
            RecurrencePattern = new SingleInstanceRecurrencePattern()
            {
                StartDate = new DateOnly(2024, 6, 8),
                EndDate = new DateOnly(2024, 6, 8),
                Frequency = Core.Entities.Enums.Frequency.None,
                Interval = 1,
                ByWeekDay = []
            },
            EventCollaborators =
            [
                new (){
                                EventDate = new DateOnly(2024, 6, 7),
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                                ProposedDuration = null,
                                User = new(){
                                    Id = 1,
                                    Name = "a",
                                    Email = "a"
                                }
                            },
                new (){
                                EventDate = new DateOnly(2024, 6, 7),
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Proposed,
                                ProposedDuration = new Duration(1,2),
                                User = new(){
                                    Id = 2,
                                    Name = "b",
                                    Email = "b"
                                }
                            }
            ]
        };

        await eventCollaboratorRepository.DeleteEventCollaboratorsByEventId(1);

        await eventRepository.Update(eventToUpdate);

        Event? updatedEvent = await eventRepository.GetEventById(1);

        eventToUpdate.Id = 1;

        eventToUpdate.EventCollaborators[0].Id = 13;
        eventToUpdate.EventCollaborators[0].EventId = 1;
        eventToUpdate.EventCollaborators[1].Id = 14;
        eventToUpdate.EventCollaborators[1].EventId = 1;

        updatedEvent.Should().BeEquivalentTo(eventToUpdate);
    }
}
