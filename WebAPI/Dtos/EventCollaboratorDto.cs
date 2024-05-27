namespace WebAPI.Dtos;

public class EventCollaboratorDto
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public UserDto User { get; set; }

    public string ParticipantRole { get; set; }

    public string ConfirmationStatus { get; set; }

    public DurationDto? ProposedDuration { get; set; }

    public DateOnly EventDate { get; set; }
}
