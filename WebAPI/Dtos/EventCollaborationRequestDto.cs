namespace WebAPI.Dtos;

public class EventCollaborationRequestDto
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public UserDto User { get; set; }

    public string ParticipantRole { get; set; }

    public string ConfirmationStatus { get; set; }

    public DateOnly EventDate { get; set; }
}
