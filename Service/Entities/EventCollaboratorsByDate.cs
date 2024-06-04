namespace Core.Entities;

public class EventCollaboratorsByDate
{
    public DateOnly EventDate { get; set; }

    public List<EventCollaborator> EventCollaborators { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is EventCollaboratorsByDate eventCollaboratorsByDate)
        {
            bool result = this.EventDate.Equals(eventCollaboratorsByDate.EventDate)
                          && this.EventCollaborators.Count == eventCollaboratorsByDate.EventCollaborators.Count;

            foreach (var eventCollaborator in this.EventCollaborators)
            {
                result &= eventCollaboratorsByDate.EventCollaborators.Contains(eventCollaborator);
            }

            return result;

        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.EventDate,
                                this.EventCollaborators);
    }
}
