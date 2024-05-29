using Core.Domain.Enums;
using Core.Interfaces;

namespace Core.Domain.Models;

public class EventCollaborator : IEntity
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public User User { get; set; }

    public ParticipantRole ParticipantRole { get; set; }

    public ConfirmationStatus ConfirmationStatus { get; set; }

    public Duration? ProposedDuration { get; set; }

    public DateOnly EventDate { get; set; }

    public bool IsEventCollaboratorWithPendingStatus() => ConfirmationStatus == ConfirmationStatus.Pending;

    public bool IsEventCollaboratorWithProposedStatus() => ConfirmationStatus == ConfirmationStatus.Proposed;

    public bool IsEventCollaboratorWithAcceptStatus() => ConfirmationStatus == ConfirmationStatus.Accept;

    public bool IsEventCollaboratorWithMaybeStatus() => ConfirmationStatus == ConfirmationStatus.Maybe;

    public bool IsOrganizerOfEvent() => ParticipantRole == ParticipantRole.Organizer;

    public bool IsParticipantOfEvent() => ParticipantRole == ParticipantRole.Participant;

    public bool IsNullProposedDuration() => ProposedDuration == null;
}
