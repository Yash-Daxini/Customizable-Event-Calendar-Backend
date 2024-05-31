namespace WebAPI.Dtos;

public class EventCollaboratorRequestDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string EventCollaboratorRole { get; set; }

    public string ConfirmationStatus { get; set; }
}
