namespace WebAPI.Dtos;

public class EventCollaboratorConfirmationDto
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public int UserId { get; set; }

    public string ConfirmationStatus { get; set; }

    public DurationDto? ProposedDuration { get; set; }
}
