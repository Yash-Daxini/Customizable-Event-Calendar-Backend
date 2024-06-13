using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.DataModels;
using Infrastructure.Repositories;
using NSubstitute;

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
                Interval = 1
            },
            DateWiseEventCollaborators =
            [
                    new (){
                        EventDate = new DateOnly(2024, 6, 7),
                        EventCollaborators = [
                            new (){
                                EventDate = new DateOnly(2024, 6, 7),
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                                ProposedDuration = null,
                                User = new(){
                                    Id = 1,
                                    Name = "a",
                                    Email = "a",
                                    Password = "a",
                                }
                            },new (){
                                EventDate = new DateOnly(2024, 6, 7),
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
                                ProposedDuration = null,
                                User = new(){
                                    Id = 2,
                                    Name = "b",
                                    Email = "b",
                                    Password = "b",
                                }
                            }
                            ]
                    }
            ]
        };

        await eventCollaboratorRepository.DeleteEventCollaboratorsByEventId(1);

        await eventRepository.Update(eventToUpdate);

        Event? updatedEvent = await eventRepository.GetEventById(1);

        eventToUpdate.Id = 1;

        eventToUpdate.DateWiseEventCollaborators[0].EventCollaborators[0].Id = 4;
        eventToUpdate.DateWiseEventCollaborators[0].EventCollaborators[0].EventId = 1;
        eventToUpdate.DateWiseEventCollaborators[0].EventCollaborators[1].Id = 5;
        eventToUpdate.DateWiseEventCollaborators[0].EventCollaborators[1].EventId = 1;

        Assert.Equivalent(eventToUpdate, updatedEvent);
    }
}
