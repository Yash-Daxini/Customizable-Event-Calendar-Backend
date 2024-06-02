using Core.Entities.Enums;
using Core.Interfaces;

namespace Core.Entities;

public class EventCollaborator : IEntity
{

    public EventCollaborator(EventCollaborator eventCollaborator)
    {
        Id = eventCollaborator.Id;
        EventCollaboratorRole = eventCollaborator.EventCollaboratorRole;
        ProposedDuration = eventCollaborator.ProposedDuration;
        User = eventCollaborator.User;
        ConfirmationStatus = eventCollaborator.ConfirmationStatus;
        EventDate = eventCollaborator.EventDate;
        EventId = eventCollaborator.EventId;
    }

    public EventCollaborator()
    {
        
    }

    public int Id { get; set; }

    public int EventId { get; set; }

    public User User { get; set; }

    public EventCollaboratorRole EventCollaboratorRole { get; set; }

    public ConfirmationStatus ConfirmationStatus { get; set; }

    public Duration? ProposedDuration { get; set; }

    public DateOnly EventDate { get; set; }

    public bool IsPendingStatus() => ConfirmationStatus == ConfirmationStatus.Pending;

    public bool IsProposedStatus() => ConfirmationStatus == ConfirmationStatus.Proposed;

    public bool IsAcceptStatus() => ConfirmationStatus == ConfirmationStatus.Accept;

    public bool IsMaybeStatus() => ConfirmationStatus == ConfirmationStatus.Maybe;

    public bool IsEventOrganizer() => EventCollaboratorRole == EventCollaboratorRole.Organizer;

    public bool IsEventParticipant() => EventCollaboratorRole == EventCollaboratorRole.Participant;

    public bool IsNullProposedDuration() => ProposedDuration == null;

    public void SetProposedDurationNull()
    {
        ProposedDuration = null;
    }

    public void AcceptConfirmationStatus()
    {
        ConfirmationStatus = ConfirmationStatus.Accept;
    }

    public void RejectConfirmationStatus()
    {
        ConfirmationStatus = ConfirmationStatus.Reject;
    }

    public void SetConfirmationStatusPending()
    {
        ConfirmationStatus = ConfirmationStatus.Pending;
    }

    public void SetEventCollaboratorRoleAsParticipant()
    {
        EventCollaboratorRole = EventCollaboratorRole.Participant;
    }
}
