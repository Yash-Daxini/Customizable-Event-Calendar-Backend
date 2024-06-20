using Core.Entities;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Core.Entities.RecurrecePattern;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Services.EventServiceTests;

public class GetSharedEvents
{
    private readonly IEventRepository _eventRepository;

    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly IOverlappingEventService _overlappingEventService;
    private readonly ISharedCalendarService _sharedCalendarService;
    private readonly IEventService _eventService;
    private readonly List<Event> _events;

    public GetSharedEvents()
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
                            EventDate = new DateOnly(),
                            User = new User
                            {
                                Id = 48,
                            },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
                            EventDate = new DateOnly(),
                            User = new User
                            {
                                Id = 49,
                            },
                        },
            ]
        }
        ];
    }

    [Fact]
    public async Task Should_ReturnListOfEvent_When_SharedCalendarWithIdAvailable()
    {
        SharedCalendar sharedCalendar = new(1, new() { Id = 1, Name = "a", Email = "a@gmail.com", Password = "a" },
                                               new() { Id = 2, Name = "b", Email = "b@gmail.com", Password = "b" },
                                               new DateOnly(),
                                               new DateOnly());

        _sharedCalendarService.GetSharedCalendarById(48).Returns(sharedCalendar);

        _eventRepository.GetSharedEvents(sharedCalendar).Returns(_events);

        List<Event> events = await _eventService.GetSharedEvents(48);

        events.Should().BeEquivalentTo(_events);

        await _eventRepository.Received().GetSharedEvents(sharedCalendar);
    }

    [Fact]
    public async Task Should_ReturnEmptyList_When_SharedCalendarWithIdNotAvailable()
    {
        SharedCalendar sharedCalendar = new(1, new() { Id = 1, Name = "a", Email = "a@gmail.com", Password = "a" },
                                               new() { Id = 2, Name = "b", Email = "b@gmail.com", Password = "b" },
                                               new DateOnly(),
                                               new DateOnly());

        _sharedCalendarService.GetSharedCalendarById(48).ReturnsNull();

        _eventRepository.GetSharedEvents(sharedCalendar).Returns([]);

        List<Event> events = await _eventService.GetSharedEvents(48);

        events.Should().BeEmpty();

        await _eventRepository.DidNotReceive().GetSharedEvents(sharedCalendar);
    }

    [Fact]
    public async Task Should_ThrowException_When_SharedCalendarWithIdNotValid()
    {
        SharedCalendar sharedCalendar = new(1, new() { Id = 1, Name = "a", Email = "a@gmail.com", Password = "a" },
                                               new() { Id = 2, Name = "b", Email = "b@gmail.com", Password = "b" },
                                               new DateOnly(),
                                               new DateOnly());

        _sharedCalendarService.GetSharedCalendarById(-1).ReturnsNull();

        _eventRepository.GetSharedEvents(sharedCalendar).Returns([]);

        var action = async () => await _eventService.GetSharedEvents(-1);

        await action.Should().ThrowAsync<ArgumentException>();

        await _eventRepository.DidNotReceive().GetSharedEvents(sharedCalendar);
    }
}
