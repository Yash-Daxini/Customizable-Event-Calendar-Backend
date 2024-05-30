using Core.Entities.Enums;
using Core.Interfaces;

namespace Core.Entities;

public class EventCollaborator : IEntity
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public User User { get; set; }

    public EventCollaboratorRole EventCollaboratorRole { get; set; }

    public ConfirmationStatus ConfirmationStatus { get; set; }

    public Duration? ProposedDuration { get; set; }

    public DateOnly EventDate { get; set; }

    public bool IsEventCollaboratorWithPendingStatus() => ConfirmationStatus == ConfirmationStatus.Pending;

    public bool IsEventCollaboratorWithProposedStatus() => ConfirmationStatus == ConfirmationStatus.Proposed;

    public bool IsEventCollaboratorWithAcceptStatus() => ConfirmationStatus == ConfirmationStatus.Accept;

    public bool IsEventCollaboratorWithMaybeStatus() => ConfirmationStatus == ConfirmationStatus.Maybe;

    public bool IsOrganizerOfEvent() => EventCollaboratorRole == EventCollaboratorRole.Organizer;

    public bool IsEventCollaboratorOfEvent() => EventCollaboratorRole == EventCollaboratorRole.Participant;

    public bool IsNullProposedDuration() => ProposedDuration == null;
}
