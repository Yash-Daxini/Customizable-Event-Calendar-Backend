using Core.Entities;
using Core.Entities.RecurrecePattern;

namespace UnitTests.Builders;

public class EventBuilder
{
    private readonly Event _eventObj = new();

    public EventBuilder WithId(int id)
    {
        _eventObj.Id = id;
        return this;
    }

    public EventBuilder WithTitle(string title)
    {
        _eventObj.Title = title;
        return this;
    }

    public EventBuilder WithDescription(string description)
    {
        _eventObj.Description = description;
        return this;
    }

    public EventBuilder WithLocation(string location)
    {
        _eventObj.Location = location;
        return this;
    }

    public EventBuilder WithRecurrencePattern(RecurrencePattern recurrencePattern)
    {
        _eventObj.RecurrencePattern = recurrencePattern;
        return this;
    }

    public EventBuilder WithDuration(Duration duration)
    {
        _eventObj.Duration = duration;
        return this;
    }

    public EventBuilder WithDateWiseEventCollaborators(List<EventCollaboratorsByDate> dateWiseEventCollaborators)
    {
        _eventObj.DateWiseEventCollaborators = dateWiseEventCollaborators;
        return this;
    }

    public Event Build() => _eventObj;
}
