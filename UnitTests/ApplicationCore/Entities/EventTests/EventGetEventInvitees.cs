using Core.Entities;
using Core.Entities.Enums;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventGetEventInvitees
{

    [Fact]
    public void Should_ReturnEmptyList_When_EventCollaboratorsIsNull()
    {
        Event eventObj = new () { EventCollaborators = null };

        List<EventCollaborator> actualResult = eventObj.GetEventInvitees();

        actualResult.Should().BeEmpty();
    }

    [Fact]
    public void Should_ReturnEmptyList_When_EventCollaboratorsIsEmpty()
    {
        Event eventObj = new () { EventCollaborators = [] };

        List<EventCollaborator> actualResult = eventObj.GetEventInvitees();

        actualResult.Should().BeEmpty();
    }

    [Fact]
    public void Should_ReturnListOfEventCollaborators_When_EventCollaboratorsAvailable()
    {
        Event eventObj = new()
        {
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Organizer,
                            ConfirmationStatus = ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(),
                            User = new User
                            {
                                Id = 48,
                            },
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Participant,
                            ConfirmationStatus = ConfirmationStatus.Pending,
                            ProposedDuration = null,
                            EventDate = new DateOnly(),
                            User = new User
                            {
                                Id = 49
                            },
                            EventId = 47
                        }
            ]
        };

        List<EventCollaborator> eventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Participant,
                            ConfirmationStatus = ConfirmationStatus.Pending,
                            ProposedDuration = null,
                            EventDate = new DateOnly(),
                            User = new User
                            {
                                Id = 49
                            },
                            EventId = 47
                        }
                        ];


        List<EventCollaborator> actualResult = eventObj.GetEventInvitees();

        actualResult.Should().BeEquivalentTo(eventCollaborators);
    }

    [Fact]
    public void Should_ReturnListOfEventCollaborators_When_EventCollaboratorsContainsCollaborator()
    {
        Event eventObj = new()
        {
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Organizer,
                            ConfirmationStatus = ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(),
                            User = new User
                            {
                                Id = 48,
                            },
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Participant,
                            ConfirmationStatus = ConfirmationStatus.Pending,
                            ProposedDuration = null,
                            EventDate = new DateOnly(),
                            User = new User
                            {
                                Id = 49
                            },
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Collaborator,
                            ConfirmationStatus = ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(),
                            User = new User
                            {
                                Id = 50
                            },
                            EventId = 47
                        }
            ]
        };

        List<EventCollaborator> eventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Participant,
                            ConfirmationStatus = ConfirmationStatus.Pending,
                            ProposedDuration = null,
                            EventDate = new DateOnly(),
                            User = new User
                            {
                                Id = 49
                            },
                            EventId = 47
                        }
                        ];

        List<EventCollaborator> actualResult = eventObj.GetEventInvitees();

        actualResult.Should().BeEquivalentTo(eventCollaborators);
    }
}
