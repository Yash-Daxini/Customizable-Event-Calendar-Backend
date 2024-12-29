namespace WebAPI.Dtos;

public class EventResponseDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Location { get; set; }

    public string Description { get; set; }

    public DurationDto Duration { get; set; }

    public RecurrencePatternDto RecurrencePattern { get; set; }

    public List<DateOnly> Occurrences { get; set; }

    public List<EventCollaboratorResponseDto> EventCollaborators { get; set; }
}
