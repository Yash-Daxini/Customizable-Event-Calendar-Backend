using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using FluentAssertions;
using Core.Entities.RecurrecePattern;

namespace UnitTests.ApplicationCore.Services.EventServiceTests;

public class UpdateEvent
{
    private readonly IEventRepository _eventRepository;

    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly IOverlappingEventService _overlappingEventService;
    private readonly ISharedCalendarService _sharedCalendarService;
    private readonly IEventService _eventService;
    private readonly List<Event> _events;

    public UpdateEvent()
    {
        _eventRepository = Substitute.For<IEventRepository>();
        _eventCollaboratorService = Substitute.For<IEventCollaboratorService>();
        _overlappingEventService = Substitute.For<IOverlappingEventService>();
        _sharedCalendarService = Substitute.For<ISharedCalendarService>();
        _eventService = new EventService(_eventRepository, _eventCollaboratorService, _overlappingEventService, _sharedCalendarService);
        _events =
        [
            new()
        {
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 49,
                            },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 48,
                            },
                        }
            ]
        },
        ];
    }

    [Fact]
    public async Task Should_UpdateEvent_When_EventNotOverlaps()
    {
        Event eventObj = new()
        {
            Id = 1,
            RecurrencePattern = new DailyRecurrencePattern(),
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 49,
                            },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 48,
                            },
                        }
            ]
        };

        _eventService.GetAllEventsByUserId(48).Returns(_events);

        _eventService.GetEventById(1, 48).Returns(eventObj);

        _overlappingEventService.GetOverlappedEventInformation(eventObj, _events).ReturnsNullForAnyArgs();

        await _eventService.UpdateEvent(eventObj, 48);

        await _eventRepository.Received().Update(eventObj);

        _overlappingEventService.ReceivedWithAnyArgs().GetOverlappedEventInformation(eventObj, _events);
    }

    [Fact]
    public async Task Should_ThrowException_When_EventOverlaps()
    {
        Event eventObj = new()
        {
            Id = 1,
            RecurrencePattern = new DailyRecurrencePattern(),
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 49,
                            },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 48,
                            },
                        }
            ]
        };

        _eventService.GetAllEventsByUserId(48).Returns(_events);

        _eventService.GetEventById(1, 48).Returns(eventObj);

        _overlappingEventService.GetOverlappedEventInformation(eventObj, _events).ReturnsForAnyArgs("Overlaps");

        var action = async () => await _eventService.UpdateEvent(eventObj, 48);

        await action.Should().ThrowAsync<EventOverlapException>();

        await _eventRepository.DidNotReceive().Update(eventObj);

        _overlappingEventService.ReceivedWithAnyArgs().GetOverlappedEventInformation(eventObj, _events);
    }

    [Fact]
    public async Task Should_ThrowException_When_EventOverlapsWithEmptyMessage()
    {
        Event eventObj = new()
        {
            Id = 1,
            RecurrencePattern = new DailyRecurrencePattern(),
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 49,
                            },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 48,
                            },
                        }
            ]
        };

        _eventService.GetEventById(1, 48).Returns(eventObj);

        _eventService.GetAllEventsByUserId(48).Returns(_events);

        _overlappingEventService.GetOverlappedEventInformation(eventObj, _events).ReturnsForAnyArgs("");

        var action = async () => await _eventService.UpdateEvent(eventObj, 48);

        await action.Should().ThrowAsync<EventOverlapException>();
    }

    [Fact]
    public async Task Should_ThrowException_When_EventIsNull()
    {
        Event eventObj = null;

        _eventService.GetAllEventsByUserId(48).Returns(_events);

        _eventRepository.Add(eventObj).Returns(1);

        var action = async () => await _eventService.UpdateEvent(eventObj, 48);

        await action.Should().ThrowAsync<NullArgumentException>();

        await _eventRepository.DidNotReceive().Add(eventObj);

        _overlappingEventService.DidNotReceive().GetOverlappedEventInformation(eventObj, _events);
    }
}
