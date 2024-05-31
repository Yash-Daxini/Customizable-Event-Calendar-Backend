namespace WebAPI.Dtos;

public class EventCollaborationRequestDto
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public int UserId { get; set; }

    public string EventCollaboratorRole { get; set; }

    public string ConfirmationStatus { get; set; }

    public DateOnly EventDate { get; set; }
}
