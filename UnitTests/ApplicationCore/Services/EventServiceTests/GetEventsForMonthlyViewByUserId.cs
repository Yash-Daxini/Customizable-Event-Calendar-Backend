using Core.Entities;
using Core.Extensions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using FluentAssertions;
using Core.Entities.Enums;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Services.EventServiceTests;

public class GetEventsForMonthlyViewByUserId
{
    private readonly IEventRepository _eventRepository;

    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly IOverlappingEventService _overlappingEventService;
    private readonly ISharedCalendarService _sharedCalendarService;
    private readonly IEventService _eventService;
    private readonly List<Event> _events;

    public GetEventsForMonthlyViewByUserId()
    {
        _eventRepository = Substitute.For<IEventRepository>();
        _eventCollaboratorService = Substitute.For<IEventCollaboratorService>();
        _overlappingEventService = Substitute.For<IOverlappingEventService>();
        _sharedCalendarService = Substitute.For<ISharedCalendarService>();
        _eventService = new EventService(_eventRepository, _eventCollaboratorService, _overlappingEventService, _sharedCalendarService);

        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(0)
                                             .WithOrganizer(new UserBuilder(49).Build(), new DateOnly(2024, 5, 31))
                                             .WithParticipant(new UserBuilder(48).Build(),
                                                              ConfirmationStatus.Accept,
                                                              new DateOnly(2024, 5, 31),
                                                              null)
                                             .Build();

        Event eventObj = new EventBuilder()
                       .WithEventCollaborators(eventCollaborators)
                       .Build();

        _events =
        [
            eventObj
        ];
    }

    [Fact]
    public async Task Should_Return_ListOfEvent_When_UserAvailableWithId()
    {
        DateOnly startDateOfMonth = DateTimeUtills.GetStartDateOfMonth(DateTime.Now);
        DateOnly endDateOfMonth = DateTimeUtills.GetEndDateOfMonth(DateTime.Now);

        _eventRepository.GetEventsWithinGivenDateByUserId(48, startDateOfMonth, endDateOfMonth).ReturnsForAnyArgs(_events);

        List<Event> events = await _eventService.GetEventsForMonthlyViewByUserId(48);

        events.Should().BeEquivalentTo(_events);

        await _eventRepository.Received().GetEventsWithinGivenDateByUserId(48, startDateOfMonth, endDateOfMonth);
    }
}
