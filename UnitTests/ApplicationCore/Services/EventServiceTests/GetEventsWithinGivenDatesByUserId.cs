using Core.Entities;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using Core.Extensions;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Services.EventServiceTests;

public class GetEventsWithinGivenDatesByUserId
{
    private readonly IEventRepository _eventRepository;

    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly IOverlappingEventService _overlappingEventService;
    private readonly ISharedCalendarService _sharedCalendarService;
    private readonly IEventService _eventService;
    private readonly List<Event> _events;

    public GetEventsWithinGivenDatesByUserId()
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
        },
            new()
            {
                EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                        {
                            Id = 48,
                        },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 49,
                            },
                        },
                    ]
            }
        ];
    }


    [Theory]
    [InlineData(2024, 2024, 5, 6, 30, 1)]
    public async Task Should_ReturnListOfEvent_When_UserWithIdAvailable(int startYear, int endYear, int startMonth, int endMonth, int startDay, int endDay)
    {
        DateOnly startDate = new(startYear, startMonth, startDay);
        DateOnly endDate = new(endYear, endMonth, endDay);

        _eventRepository.GetEventsWithinGivenDateByUserId(48, startDate, endDate).Returns(_events);

        List<Event> events = await _eventService.GetEventsWithinGivenDatesByUserId(48, startDate, endDate);

        events.Should().BeEquivalentTo(_events);

        await _eventRepository.Received().GetEventsWithinGivenDateByUserId(48, startDate, endDate);
    }

    [Theory]
    [InlineData(2024, 2024, 6, 5, 30, 1)]
    public async Task Should_ThrowException_When_StartDateAndEndDateNotValid(int startYear, int endYear, int startMonth, int endMonth, int startDay, int endDay)
    {
        DateOnly startDate = new(startYear, startMonth, startDay);
        DateOnly endDate = new(endYear, endMonth, endDay);

        List<Event> expected = [];

        _eventRepository.GetEventsWithinGivenDateByUserId(48, startDate, endDate).Returns(expected);

        var action = async () => await _eventService.GetEventsWithinGivenDatesByUserId(48, startDate, endDate);

        await action.Should().ThrowAsync<ArgumentException>();

        await _eventRepository.DidNotReceive().GetEventsWithinGivenDateByUserId(48, startDate, endDate);
    }
}
