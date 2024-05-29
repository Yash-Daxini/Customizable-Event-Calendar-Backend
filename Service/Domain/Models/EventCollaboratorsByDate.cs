namespace Core.Domain.Models;

public class EventCollaboratorsByDate
{
    public DateOnly EventDate { get; set; }

    public List<EventCollaborator> EventCollaborators { get; set; }
}
