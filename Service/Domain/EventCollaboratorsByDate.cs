namespace Core.Domain;

public class EventCollaboratorsByDate
{
    public DateOnly EventDate { get; set; }

    public List<EventCollaborator> EventCollaborators { get; set; }
}
