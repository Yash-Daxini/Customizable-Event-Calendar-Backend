using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Core.Entities.RecurrecePattern;
using FluentAssertions;
using Core.Entities.Enums;

namespace UnitTests.ApplicationCore.Services.EventServiceTests;

public class AddEvent
{
    private readonly IEventRepository _eventRepository;

    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly IOverlappingEventService _overlappingEventService;
    private readonly ISharedCalendarService _sharedCalendarService;
    private readonly IEventService _eventService;
    private readonly List<Event> _events;

    public AddEvent()
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
                Title = "event",
                EventCollaborators = [
                        new EventCollaborator
                            {
                                EventCollaboratorRole = EventCollaboratorRole.Organizer,
                                ConfirmationStatus = ConfirmationStatus.Accept,
                                EventDate = new DateOnly(2024, 5, 31),
                                User = new User
                                {
                                    Id = 49,
                                },
                            }
                ]
            }
        ];
    }

    [Fact]
    public async Task Should_ReturnAddedEventId_When_EventNotOverlaps()
    {
        Event eventObj = new()
        {
            Title = "event",
            RecurrencePattern = new DailyRecurrencePattern(),
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Organizer,
                            ConfirmationStatus = ConfirmationStatus.Accept,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 49,
                            },
                        }
            ]
        };

        _eventService.GetAllEventsByUserId(48).Returns(_events);

        _overlappingEventService.GetOverlappedEventInformation(eventObj, _events).ReturnsNullForAnyArgs();

        _eventRepository.Add(eventObj).Returns(1);

        int id = await _eventService.AddEvent(eventObj, 48);

        id.Should().Be(1);

        await _eventRepository.Received().Add(eventObj);

        _overlappingEventService.ReceivedWithAnyArgs().GetOverlappedEventInformation(eventObj, _events);
    }

    [Fact]
    public async Task Should_ThrowException_When_EventOverlaps()
    {
        Event eventObj = new()
        {
            Title = "event",
            RecurrencePattern = new DailyRecurrencePattern(),
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Organizer,
                            ConfirmationStatus = ConfirmationStatus.Accept,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 49,
                            },
                        }
            ]
        };

        _eventService.GetAllEventsByUserId(48).Returns(_events);

        _overlappingEventService.GetOverlappedEventInformation(eventObj, _events).ReturnsForAnyArgs("Overlaps");

        var action = async () => await _eventService.AddEvent(eventObj, 48);

        await action.Should().ThrowAsync<EventOverlapException>();
    }

    [Fact]
    public async Task Should_ThrowException_When_EventOverlapsWithEmptyMessage()
    {
        Event eventObj = new()
        {
            Title = "event",
            RecurrencePattern = new DailyRecurrencePattern(),
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Organizer,
                            ConfirmationStatus = ConfirmationStatus.Accept,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 49,
                            },
                        }
            ]
        };

        _eventService.GetAllEventsByUserId(48).Returns(_events);

        _overlappingEventService.GetOverlappedEventInformation(eventObj, _events).ReturnsForAnyArgs("");

        var action = async () => await _eventService.AddEvent(eventObj, 48);

        await action.Should().ThrowAsync<EventOverlapException>();
    }

    [Fact]
    public async Task Should_ThrowException_When_EventIsNull()
    {
        Event eventObj = null;

        _eventService.GetAllEventsByUserId(48).Returns(_events);

        _eventRepository.Add(eventObj).Returns(1);

        var action = async () => await _eventService.AddEvent(eventObj, 48);

        await action.Should().ThrowAsync<NullArgumentException>();

        await _eventRepository.DidNotReceive().Add(eventObj);

        _overlappingEventService.DidNotReceive().GetOverlappedEventInformation(eventObj, _events);
    }
}
