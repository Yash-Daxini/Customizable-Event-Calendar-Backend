using Core.Domain.Enums;

namespace Core.Domain;

public class ParticipantModel
{
    public int Id { get; set; }

    public ParticipantRole ParticipantRole { get; set; }

    public ConfirmationStatus ConfirmationStatus { get; set; }

    public DurationModel? ProposedDuration { get; set; }

    public DateOnly EventDate { get; set; }

    public UserModel User { get; set; }
}
