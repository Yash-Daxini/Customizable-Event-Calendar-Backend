using Core.Entities;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using Core.Extensions;
using FluentAssertions;
using Core.Entities.Enums;
using UnitTests.Builders;

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
        List<EventCollaborator> eventCollaborators1 = new EventCollaboratorListBuilder(0)
                                             .WithOrganizer(new UserBuilder(49).Build(), new DateOnly(2024, 5, 31))
                                             .WithParticipant(new UserBuilder(48).Build(),
                                                              ConfirmationStatus.Accept,
                                                              new DateOnly(2024, 5, 31),
                                                              null)
                                             .Build();

        Event event1 = new EventBuilder()
                       .WithEventCollaborators(eventCollaborators1)
                       .Build();

        List<EventCollaborator> eventCollaborators2 = new EventCollaboratorListBuilder(0)
                                             .WithOrganizer(new UserBuilder(48).Build(), new DateOnly(2024, 5, 31))
                                             .WithParticipant(new UserBuilder(49).Build(),
                                                              ConfirmationStatus.Accept,
                                                              new DateOnly(2024, 5, 31),
                                                              null)
                                             .Build();

        Event event2 = new EventBuilder()
                       .WithEventCollaborators(eventCollaborators2)
                       .Build();

        _events = [event1, event2];
    }


    [Theory]
    [InlineData(2024, 2024, 5, 6, 30, 1)]
    public async Task Should_Return_ListOfEvent_When_UserAvailableWithId(int startYear, int endYear, int startMonth, int endMonth, int startDay, int endDay)
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
    public async Task Should_Throw_ArgumentException_When_StartDateAndEndDateAreNotValid(int startYear, int endYear, int startMonth, int endMonth, int startDay, int endDay)
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
