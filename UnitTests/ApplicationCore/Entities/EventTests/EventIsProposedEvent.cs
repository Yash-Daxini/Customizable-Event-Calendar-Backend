using Core.Entities;
using Core.Entities.Enums;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventIsProposedEvent
{

    private readonly Event _event;

    public EventIsProposedEvent()
    {
        _event = new()
        {
            EventCollaborators = [
                    new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Organizer,
                            ConfirmationStatus = ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            User = new User
                            {
                                Id = 48,
                            },
                        },
                    new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Participant,
                            ConfirmationStatus = ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            User = new User
                            {
                                Id = 49,
                            },
                        },
            ]
        };
    }

    [Fact]
    public void Should_ReturnsFalse_When_NotAnyEventCollaboratorWithProposedOrPendingStatus()
    {
        bool result = _event.IsProposedEvent();
        result.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnsTrue_When_AnyEventCollaboratorWithProposedStatus()
    {
        _event.EventCollaborators.Add(
            new EventCollaborator
            {
                EventCollaboratorRole = EventCollaboratorRole.Participant,
                ConfirmationStatus = ConfirmationStatus.Proposed,
                ProposedDuration = null,
                User = new User
                {
                    Id = 50,
                },
            }
        );

        bool result = _event.IsProposedEvent();

        result.Should().BeTrue();
    }

    [Fact]
    public void Should_ReturnsTrue_When_AnyEventCollaboratorWithPendingStatus()
    {
        _event.EventCollaborators.Add(
            new EventCollaborator
            {
                EventCollaboratorRole = EventCollaboratorRole.Participant,
                ConfirmationStatus = ConfirmationStatus.Pending,
                ProposedDuration = null,
                User = new User
                {
                    Id = 50,
                },
            }
        );

        bool result = _event.IsProposedEvent();
        result.Should().BeTrue();
    }

    [Fact]
    public void Should_ReturnsTrue_When_EventCollaboratorsWithPendingAndProposedStatus()
    {
        _event.EventCollaborators.Add(
            new EventCollaborator
            {
                EventCollaboratorRole = EventCollaboratorRole.Participant,
                ConfirmationStatus = ConfirmationStatus.Pending,
                ProposedDuration = null,
                User = new User
                {
                    Id = 50,
                },
            }
        );

        _event.EventCollaborators.Add(
            new EventCollaborator
            {
                EventCollaboratorRole = EventCollaboratorRole.Participant,
                ConfirmationStatus = ConfirmationStatus.Proposed,
                ProposedDuration = null,
                User = new User
                {
                    Id = 50,
                },
            }
        );
        bool result = _event.IsProposedEvent();
        result.Should().BeTrue();
    }
}
