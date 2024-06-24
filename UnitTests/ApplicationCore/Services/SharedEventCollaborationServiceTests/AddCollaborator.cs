using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using Core.Entities.RecurrecePattern;
using Core.Entities.Enums;
using FluentAssertions;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Services.SharedEventCollaborationServiceTests;

public class AddCollaborator
{

    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly IEventService _eventService;
    private readonly ISharedEventCollaborationService _sharedEventCollaborationService;
    private readonly List<Event> _events;

    public AddCollaborator()
    {
        _eventCollaboratorService = Substitute.For<IEventCollaboratorService>();
        _eventService = Substitute.For<IEventService>();
        _sharedEventCollaborationService = new SharedEventCollaborationService(_eventCollaboratorService, _eventService);

        List<EventCollaborator> eventCollaborators1 = new EventCollaboratorListBuilder(47)
                                                      .WithOrganizer(new UserBuilder().WithId(49).Build(), new DateOnly(2024, 5, 31))
                                                      .WithParticipant(new UserBuilder().WithId(48).Build(),
                                                              ConfirmationStatus.Accept,
                                                              new DateOnly(2024, 5, 31),
                                                              null)
                                                      .Build();

        List<EventCollaborator> eventCollaborators2 = new EventCollaboratorListBuilder(47)
                                                      .WithOrganizer(new UserBuilder().WithId(49).Build(), new DateOnly(2024, 5, 31))
                                                      .WithParticipant(new UserBuilder().WithId(48).Build(),
                                                              ConfirmationStatus.Accept,
                                                              new DateOnly(2024, 5, 31),
                                                              null)
                                                      .Build();

        _events =
        [
            new EventBuilder()
            .WithDuration(new Duration(1,2))
            .WithEventCollaborators(eventCollaborators1)
            .Build(),
            new EventBuilder()
            .WithDuration(new Duration(1,2))
            .WithEventCollaborators(eventCollaborators2)
            .Build(),
        ];
    }

    [Fact]
    public async Task Should_AddCollaborator_When_NotOverlapAndNotAlreadyCollaborated()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                      .WithOrganizer(new UserBuilder().WithId(49).Build(), new DateOnly(2024, 5, 31))
                                                      .WithParticipant(new UserBuilder().WithId(48).Build(),
                                                              ConfirmationStatus.Accept,
                                                              new DateOnly(2024, 5, 31),
                                                              null)
                                                      .Build();

        Event eventObj = new EventBuilder()
                         .WithEventCollaborators(eventCollaborators)
                         .Build();

        User user = new UserBuilder()
                    .WithId(50)
                    .WithName("c")
                    .WithEmail("c@gmail.com")
                    .WithPassword("c")
                    .Build();

        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithId(1)
                                              .WithEventCollaboratorRole(EventCollaboratorRole.Organizer)
                                              .WithConfirmationStatus(ConfirmationStatus.Accept)
                                              .WithEventDate(new DateOnly(2024, 5, 31))
                                              .WithEventId(1)
                                              .WithUser(user)
                                              .Build();

        _eventService.GetEventById(1, 50).Returns(eventObj);

        _eventService.GetNonProposedEventsByUserId(50).Returns(_events);

        await _sharedEventCollaborationService.AddCollaborator(eventCollaborator);

        await _eventCollaboratorService.Received().AddEventCollaborator(eventCollaborator);
    }

    [Fact]
    public async Task Should_ThrowException_When_CollaborationOverlap()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                              .WithOrganizer(new UserBuilder().WithId(49).Build(), new DateOnly(2024, 5, 31))
                                              .WithParticipant(new UserBuilder().WithId(48).Build(),
                                                                ConfirmationStatus.Accept,
                                                                new DateOnly(2024, 5, 31),
                                                                null)
                                              .Build();

        Event eventObj = new EventBuilder()
                         .WithDuration(new Duration(1, 2))
                         .WithEventCollaborators(eventCollaborators)
                         .Build();

        _events[1].EventCollaborators.Add(
                                            new EventCollaboratorBuilder()
                                            .WithEventCollaboratorRole(EventCollaboratorRole.Participant)
                                            .WithConfirmationStatus(ConfirmationStatus.Accept)
                                            .WithEventDate(new DateOnly(2024, 5, 31))
                                            .WithUser(new UserBuilder().WithId(50).Build())
                                            .Build()
                                         );

        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                            .WithEventCollaboratorRole(EventCollaboratorRole.Collaborator)
                                            .WithConfirmationStatus(ConfirmationStatus.Accept)
                                            .WithEventDate(new DateOnly(2024, 5, 31))
                                            .WithUser(new UserBuilder().WithId(50).Build())
                                            .WithEventId(1)
                                            .Build();

        _eventService.GetEventById(1, 50).Returns(eventObj);

        _eventService.GetNonProposedEventsByUserId(50).Returns(_events);

        var action = async () => await _sharedEventCollaborationService.AddCollaborator(eventCollaborator);

        await action.Should().ThrowAsync<CollaborationOverlapException>();

        await _eventCollaboratorService.DidNotReceive().AddEventCollaborator(eventCollaborator);
    }

    [Fact]
    public async Task Should_ThrowException_When_AlreadyCollaborated()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                              .WithOrganizer(new UserBuilder().WithId(49).Build(), new DateOnly(2024, 5, 31))
                                              .WithParticipant(new UserBuilder().WithId(48).Build(),
                                                      ConfirmationStatus.Accept,
                                                      new DateOnly(2024, 5, 31),
                                                      null)
                                              .WithParticipant(new UserBuilder().WithId(50).Build(),
                                                      ConfirmationStatus.Accept,
                                                      new DateOnly(2024, 5, 31),
                                                      null)
                                              .Build();

        Event eventObj = new EventBuilder()
                         .WithEventCollaborators(eventCollaborators)
                         .Build();

        _events.Add(eventObj);

        EventCollaborator eventCollaborator = new()
        {
            Id = 1,
            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Collaborator,
            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
            EventDate = new DateOnly(2024, 5, 31),
            EventId = 1,
            User = new()
            {
                Id = 50,
                Name = "c",
                Email = "c@gmail.com",
                Password = "c"
            },
            ProposedDuration = null
        };

        _eventService.GetEventById(1, 50).Returns(eventObj);

        _eventService.GetNonProposedEventsByUserId(50).Returns(_events);

        var action = async () => await _sharedEventCollaborationService.AddCollaborator(eventCollaborator);

        await action.Should().ThrowAsync<UserAlreadyCollaboratedException>();

        await _eventCollaboratorService.DidNotReceive().AddEventCollaborator(eventCollaborator);
    }

    [Fact]
    public async Task Should_ThrowException_When_EventCollaboratorIsNull()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                              .WithOrganizer(new UserBuilder().WithId(49).Build(), new DateOnly(2024, 5, 31))
                                              .WithParticipant(new UserBuilder().WithId(48).Build(),
                                                      ConfirmationStatus.Accept,
                                                      new DateOnly(2024, 5, 31),
                                                      null)
                                              .WithParticipant(new UserBuilder().WithId(50).Build(),
                                                      ConfirmationStatus.Accept,
                                                      new DateOnly(2024, 5, 31),
                                                      null)
                                              .Build();

        Event eventObj = new EventBuilder()
                         .WithEventCollaborators(eventCollaborators)
                         .Build();

        _events.Add(eventObj);

        EventCollaborator eventCollaborator = null;

        _eventService.GetEventById(1, 50).Returns(eventObj);

        _eventService.GetNonProposedEventsByUserId(50).Returns(_events);

        var action = async () => await _sharedEventCollaborationService.AddCollaborator(eventCollaborator);

        await action.Should().ThrowAsync<NullArgumentException>();

        await _eventCollaboratorService.DidNotReceive().AddEventCollaborator(eventCollaborator);
    }
}
