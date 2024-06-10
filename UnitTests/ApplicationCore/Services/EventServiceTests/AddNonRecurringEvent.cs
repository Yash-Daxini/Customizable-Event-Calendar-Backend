using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NullArgumentException = Core.Exceptions.NullArgumentException;

namespace UnitTests.ApplicationCore.Services.EventServiceTests;

public class AddNonRecurringEvent
{

    private readonly IEventRepository _eventRepository;

    private readonly IRecurrenceService _recurrenceService;
    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly IOverlappingEventService _overlappingEventService;
    private readonly ISharedCalendarService _sharedCalendarService;
    private readonly IEventService _eventService;
    private readonly List<Event> _events;

    public AddNonRecurringEvent()
    {
        _eventRepository = Substitute.For<IEventRepository>();
        _recurrenceService = Substitute.For<IRecurrenceService>();
        _eventCollaboratorService = Substitute.For<IEventCollaboratorService>();
        _overlappingEventService = Substitute.For<IOverlappingEventService>();
        _sharedCalendarService = Substitute.For<ISharedCalendarService>();
        _eventService = new EventService(_eventRepository, _recurrenceService, _eventCollaboratorService, _overlappingEventService, _sharedCalendarService);
        _events =
        [
            new()
        {
            Id = 2205,
            Title = "event",
            Location = "event",
            Description = "event",
            Duration = new Duration(1,2),
            RecurrencePattern = new RecurrencePattern()
            {
                StartDate = new DateOnly(2024, 5, 31),
                EndDate = new DateOnly(2024, 8, 25),
                Frequency = Core.Entities.Enums.Frequency.Weekly,
                Interval = 2,
                ByWeekDay = [2, 6],
                WeekOrder = null,
                ByMonthDay = null,
                ByMonth = null
            },
            DateWiseEventCollaborators = [
                new EventCollaboratorsByDate
                {
                    EventDate = new DateOnly(2024, 5, 31),
                    EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User(49,"b","b@gmail.com","b"),
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User(48,"a","a@gmail.com","a"),
                            EventId = 47
                        }
                    ]
                }
            ]
        },
            new()
        {
            Id = 2205,
            Title = "event 1",
            Location = "event 1",
            Description = "event 1",
            Duration = new Duration(1,2),
            RecurrencePattern = new RecurrencePattern()
            {
                StartDate = new DateOnly(2024, 5, 31),
                EndDate = new DateOnly(2024, 8, 25),
                Frequency = Core.Entities.Enums.Frequency.Weekly,
                Interval = 2,
                ByWeekDay = [2, 6],
                WeekOrder = null,
                ByMonthDay = null,
                ByMonth = null
            },
            DateWiseEventCollaborators = [
                new EventCollaboratorsByDate
                {
                    EventDate = new DateOnly(2024, 5, 31),
                    EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User(48,"a","a@gmail.com","a"),
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User(49,"b","b@gmail.com","b"),
                            EventId = 47
                        },
                    ]
                }
            ]
        },
            new()
        {
            Id = 2205,
            Title = "event 2",
            Location = "event 2",
            Description = "event 2",
            Duration = new Duration(1,2),
            RecurrencePattern = new RecurrencePattern()
            {
                StartDate = new DateOnly(2024, 5, 31),
                EndDate = new DateOnly(2024, 8, 25),
                Frequency = Core.Entities.Enums.Frequency.Weekly,
                Interval = 2,
                ByWeekDay = [2, 6],
                WeekOrder = null,
                ByMonthDay = null,
                ByMonth = null
            },
            DateWiseEventCollaborators = [
                new EventCollaboratorsByDate
                {
                    EventDate = new DateOnly(2024, 5, 31),
                    EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User(48,"a","a@gmail.com","a"),
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User(49,"b","b@gmail.com","b"),
                            EventId = 47
                        },
                    ]
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
            Location = "event",
            Description = "event",
            Duration = new Duration(1, 2),
            RecurrencePattern = new RecurrencePattern()
            {
                StartDate = new DateOnly(2024, 5, 31),
                EndDate = new DateOnly(2024, 5, 31),
                Frequency = Core.Entities.Enums.Frequency.None,
                Interval = 1,
                ByWeekDay = null,
                WeekOrder = null,
                ByMonthDay = null,
                ByMonth = null
            },
            DateWiseEventCollaborators = [
                new EventCollaboratorsByDate
                {
                    EventDate = new DateOnly(2024, 5, 31),
                    EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User(49,"b","b@gmail.com","b"),
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User(48,"a","a@gmail.com","a"),
                            EventId = 47
                        }
                    ]
                }
            ]
        };

        _eventService.GetAllEventsByUserId(48).Returns(_events);

        _overlappingEventService.GetOverlappedEventInformation(eventObj, _events).ReturnsNullForAnyArgs();

        _recurrenceService.GetOccurrencesOfEvent(eventObj.RecurrencePattern).Returns([new DateOnly(2024, 5, 31)]);

        _eventRepository.Add(eventObj).Returns(1);

        int id = await _eventService.AddNonRecurringEvent(eventObj, 48);

        Assert.Equal(1, id);

        await _eventRepository.Received().Add(eventObj);

        _overlappingEventService.ReceivedWithAnyArgs().GetOverlappedEventInformation(eventObj, _events);

        _recurrenceService.Received().GetOccurrencesOfEvent(eventObj.RecurrencePattern);
    }

    [Fact]
    public async Task Should_ThrowException_When_EventOverlaps()
    {
        Event eventObj = new()
        {
            Title = "event",
            Location = "event",
            Description = "event",
            Duration = new Duration(1, 2),
            RecurrencePattern = new RecurrencePattern()
            {
                StartDate = new DateOnly(2024, 5, 31),
                EndDate = new DateOnly(2024, 5, 31),
                Frequency = Core.Entities.Enums.Frequency.None,
                Interval = 1,
                ByWeekDay = null,
                WeekOrder = null,
                ByMonthDay = null,
                ByMonth = null
            },
            DateWiseEventCollaborators = [
                new EventCollaboratorsByDate
                {
                    EventDate = new DateOnly(2024, 5, 31),
                    EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User(49,"b","b@gmail.com","b"),
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User(48,"a","a@gmail.com","a"),
                            EventId = 47
                        }
                    ]
                }
            ]
        };

        _eventService.GetAllEventsByUserId(48).Returns(_events);

        _overlappingEventService.GetOverlappedEventInformation(eventObj, _events).Returns("Overlaps");

        _recurrenceService.GetOccurrencesOfEvent(eventObj.RecurrencePattern).Returns([new DateOnly(2024, 5, 31)]);

        _eventRepository.Add(eventObj).Returns(1);

        await Assert.ThrowsAsync<EventOverlapException>(async () => await _eventService.AddNonRecurringEvent(eventObj, 48));

        await _eventRepository.DidNotReceive().Add(eventObj);

        _overlappingEventService.ReceivedWithAnyArgs().GetOverlappedEventInformation(eventObj, _events);

        _recurrenceService.Received().GetOccurrencesOfEvent(eventObj.RecurrencePattern);
    }

    [Fact]
    public async Task Should_ThrowException_When_EventIsNull()
    {
        Event eventObj = null;

        _eventService.GetAllEventsByUserId(48).Returns(_events);

        _overlappingEventService.GetOverlappedEventInformation(eventObj, _events).ReturnsNullForAnyArgs();

        _recurrenceService.GetOccurrencesOfEvent(null).Returns([new DateOnly(2024, 5, 31)]);

        _eventRepository.Add(eventObj).Returns(1);

        await Assert.ThrowsAsync<NullArgumentException>(async () => await _eventService.AddEvent(eventObj, 48));

        await _eventRepository.DidNotReceive().Add(eventObj);

        _overlappingEventService.DidNotReceiveWithAnyArgs().GetOverlappedEventInformation(eventObj, _events);

        _recurrenceService.DidNotReceive().GetOccurrencesOfEvent(null);
    }
}