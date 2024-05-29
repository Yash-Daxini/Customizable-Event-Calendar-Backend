namespace WebAPI.Dtos;

public class EventRequestDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Location { get; set; }

    public string Description { get; set; }

    public DurationDto Duration { get; set; }

    public RecurrencePatternDto RecurrencePattern { get; set; }

    public List<EventCollaboratorRequestDto> EventCollaborators { get; set; }
}
