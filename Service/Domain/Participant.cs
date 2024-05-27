using Core.Domain.Enums;

namespace Core.Domain;

public class Participant
{
    public int Id { get; set; }

    public int EventId {  get; set; }   

    public User User { get; set; }

    public ParticipantRole ParticipantRole { get; set; }

    public ConfirmationStatus ConfirmationStatus { get; set; }

    public Duration? ProposedDuration { get; set; }

    public DateOnly EventDate { get; set; }

    public bool IsParticipantWithPendingStatus() => this.ConfirmationStatus == ConfirmationStatus.Pending;

    public bool IsParticipantWithProposedStatus() => this.ConfirmationStatus == ConfirmationStatus.Proposed;

    public bool IsParticipantWithAcceptStatus() => this.ConfirmationStatus == ConfirmationStatus.Accept;

    public bool IsParticipantWithMaybeStatus() => this.ConfirmationStatus == ConfirmationStatus.Maybe;

    public bool IsOrganizerOfEvent() => this.ParticipantRole == ParticipantRole.Organizer;

    public bool IsParticipantOfEvent() => this.ParticipantRole == ParticipantRole.Participant;

    public bool IsNullProposedDuration() => this.ProposedDuration == null;
}
