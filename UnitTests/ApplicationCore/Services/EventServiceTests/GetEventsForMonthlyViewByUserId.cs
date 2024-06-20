using Core.Entities;
using Core.Extensions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using FluentAssertions;

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
        _events =
        [
            new()
        {
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = DateTime.Now.ConvertToDateOnly(),
                            User = new User
                            {
                                Id = 49,
                            },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = DateTime.Now.ConvertToDateOnly(),
                            User = new User
                        {
                            Id = 48,
                        },
                        }
                    ]
        }
        ];
    }

    [Fact]
    public async Task Should_ReturnListOfEvent_When_UserWithIdAvailable()
    {
        DateOnly startDateOfMonth = DateTimeUtills.GetStartDateOfMonth(DateTime.Now);
        DateOnly endDateOfMonth = DateTimeUtills.GetEndDateOfMonth(DateTime.Now);

        _eventRepository.GetEventsWithinGivenDateByUserId(48, startDateOfMonth, endDateOfMonth).ReturnsForAnyArgs(_events);

        List<Event> events = await _eventService.GetEventsForMonthlyViewByUserId(48);

        events.Should().BeEquivalentTo(_events);

        await _eventRepository.Received().GetEventsWithinGivenDateByUserId(48, startDateOfMonth, endDateOfMonth);
    }
}
