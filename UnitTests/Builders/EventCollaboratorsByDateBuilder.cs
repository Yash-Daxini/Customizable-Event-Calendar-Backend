using Core.Entities;

namespace UnitTests.Builders;

public class EventCollaboratorsByDateBuilder
{
    private readonly EventCollaboratorsByDate _eventCollaboratorByDate = new();

    public EventCollaboratorsByDateBuilder WithEventDate(DateOnly eventDate)
    {
        _eventCollaboratorByDate.EventDate = eventDate;
        return this;
    }

    public EventCollaboratorsByDateBuilder WithEventCollaborators(List<EventCollaborator> eventCollaborators)
    {
        _eventCollaboratorByDate.EventCollaborators = eventCollaborators;
        return this;
    }

    public EventCollaboratorsByDate Build() => _eventCollaboratorByDate;
}
