using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.DataModels;
using Infrastructure.Repositories;
using NSubstitute;

namespace UnitTests.Infrastructure.Repositories.EventRepositoryTests;

public class UpdateEvent
{
    private DbContextEventCalendar _dbContextEvent;
    private readonly IMapper _mapper;

    public UpdateEvent()
    {
        _mapper = Substitute.For<IMapper>();
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
            RecurrencePattern = new()
            {
                StartDate = new DateOnly(2024, 6, 8),
                EndDate = new DateOnly(2024, 6, 8),
                Frequency = Core.Entities.Enums.Frequency.None,
                Interval = 1,
                ByMonth = null,
                ByMonthDay = null,
                WeekOrder = null,
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

        EventDataModel eventDataModel = new()
        {
            Id = 1,
            Title = "Test1",
            Description = "Test1",
            Location = "Test1",
            EventStartDate = new DateOnly(2024, 6, 8),
            EventEndDate = new DateOnly(2024, 6, 8),
            EventStartHour = 3,
            EventEndHour = 4,
            Frequency = "None",
            Interval = 1,
            ByMonth = null,
            ByMonthDay = null,
            WeekOrder = null,
            EventCollaborators = [new (){
                                EventDate = new DateOnly(2024, 6, 7),
                                ParticipantRole = "Organizer",
                                ConfirmationStatus = "Accept",
                                ProposedStartHour = null,
                                ProposedEndHour = null,
                                UserId = 1
                            },new (){
                                EventDate = new DateOnly(2024, 6, 7),
                                ParticipantRole = "Participant",
                                ConfirmationStatus = "Pending",
                                ProposedStartHour = null,
                                ProposedEndHour = null,
                                UserId = 2
                            }]
        };

        _mapper.Map<EventDataModel>(eventToUpdate).ReturnsForAnyArgs(eventDataModel);

        _mapper.Map<Event>(eventDataModel).ReturnsForAnyArgs(eventToUpdate);

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
