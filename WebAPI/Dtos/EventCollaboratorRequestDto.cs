namespace WebAPI.Dtos;

public class EventCollaboratorRequestDto
{
    public int Id { get; set; }

    public UserDto User { get; set; }

    public string ParticipantRole { get; set; }

    public string ConfirmationStatus { get; set; }
}
