using Core.Domain.Enums;

namespace Core.Domain;

public class Participant
{
    public int Id { get; set; }

    public ParticipantRole ParticipantRole { get; set; }

    public ConfirmationStatus ConfirmationStatus { get; set; }

    public Duration? ProposedDuration { get; set; }

    public DateOnly EventDate { get; set; }

    public User User { get; set; }
}
