namespace WebAPI.Dtos;

public class NonRecurringEventRequestDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Location { get; set; }

    public string Description { get; set; }

    public DurationDto Duration { get; set; }

    public DateOnly EventDate { get; set; }

    public List<EventCollaboratorRequestDto> EventCollaborators { get; set; }
}
