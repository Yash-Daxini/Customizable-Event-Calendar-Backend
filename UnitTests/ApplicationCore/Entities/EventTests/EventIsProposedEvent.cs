using Core.Entities;
using Core.Entities.Enums;
using FluentAssertions;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventIsProposedEvent
{

    [Fact]
    public void Should_ReturnsFalse_When_NotAnyEventCollaboratorWithProposedOrPendingStatus()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                     .WithOrganizer(new UserBuilder(48).Build(), new DateOnly())
                                                     .WithParticipant(new UserBuilder(49).Build(),
                                                                      ConfirmationStatus.Accept,
                                                                      new DateOnly(),
                                                                      null)
                                                     .Build();

        Event eventObj = new EventBuilder()
                        .WithEventCollaborators(eventCollaborators)
                        .Build();

        bool result = eventObj.IsProposedEvent();
        result.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnsTrue_When_AnyEventCollaboratorWithProposedStatus()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                     .WithOrganizer(new UserBuilder(48).Build(), new DateOnly())
                                                     .WithParticipant(new UserBuilder(49).Build(),
                                                                      ConfirmationStatus.Proposed,
                                                                      new DateOnly(),
                                                                      null)
                                                     .Build();

        Event eventObj = new EventBuilder()
                 .WithEventCollaborators(eventCollaborators)
                 .Build();

        bool result = eventObj.IsProposedEvent();

        result.Should().BeTrue();
    }

    [Fact]
    public void Should_ReturnsTrue_When_AnyEventCollaboratorWithPendingStatus()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                     .WithOrganizer(new UserBuilder(48).Build(), new DateOnly())
                                                     .WithParticipant(new UserBuilder(49).Build(),
                                                                      ConfirmationStatus.Pending,
                                                                      new DateOnly(),
                                                                      null)
                                                     .Build();

        Event eventObj = new EventBuilder()
                 .WithEventCollaborators(eventCollaborators)
                 .Build();

        bool result = eventObj.IsProposedEvent();
        result.Should().BeTrue();
    }

    [Fact]
    public void Should_ReturnsTrue_When_EventCollaboratorsWithPendingAndProposedStatus()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                     .WithOrganizer(new UserBuilder(48).Build(), new DateOnly())
                                                     .WithParticipant(new UserBuilder(49).Build(),
                                                                      ConfirmationStatus.Proposed,
                                                                      new DateOnly(),
                                                                      null)
                                                     .WithParticipant(new UserBuilder(50).Build(),
                                                                      ConfirmationStatus.Pending,
                                                                      new DateOnly(),
                                                                      null)
                                                     .Build();

        Event eventObj = new EventBuilder()
                 .WithEventCollaborators(eventCollaborators)
                 .Build();

        bool result = eventObj.IsProposedEvent();
        result.Should().BeTrue();
    }
}
