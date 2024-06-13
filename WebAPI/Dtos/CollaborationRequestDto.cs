namespace WebAPI.Dtos;

public class CollaborationRequestDto
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public int UserId { get; set; }

    public DateOnly EventDate { get; set; }
}
