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

    public bool IsStatusPending() => ConfirmationStatus == ConfirmationStatus.Pending;

    public bool IsStatusProposed() => ConfirmationStatus == ConfirmationStatus.Proposed;

    public bool IsStatusAccept() => ConfirmationStatus == ConfirmationStatus.Accept;

    public bool IsStatusMaybe() => ConfirmationStatus == ConfirmationStatus.Maybe;

    public bool IsOrganizer() => EventCollaboratorRole == EventCollaboratorRole.Organizer;

    public bool IsParticipant() => EventCollaboratorRole == EventCollaboratorRole.Participant;

    public bool IsProposedDurationNull() => ProposedDuration == null;

    public void ResetProposedDuration()
    {
        ProposedDuration = null;
    }

    public void SetEventCollaboratorRoleAsParticipant()
    {
        EventCollaboratorRole = EventCollaboratorRole.Participant;
    }

    public void SetConfirmationStatus(ConfirmationStatus status)
    {
        ConfirmationStatus = status;
    }
}
