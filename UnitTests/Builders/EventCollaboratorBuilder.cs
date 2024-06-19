using Core.Entities;
using Core.Entities.Enums;

namespace UnitTests.Builders;

public class EventCollaboratorBuilder
{
    private readonly EventCollaborator _eventCollaborator = new();

    public EventCollaboratorBuilder WithId(int id)
    {
        _eventCollaborator.Id = id;
        return this;
    }

    public EventCollaboratorBuilder WithEventId(int eventId)
    {
        _eventCollaborator.EventId = eventId;
        return this;
    }

    public EventCollaboratorBuilder WithUser(User user)
    {
        _eventCollaborator.User = user;
        return this;
    }

    public EventCollaboratorBuilder WithEventCollaboratorRole(EventCollaboratorRole eventCollaboratorRole)
    {
        _eventCollaborator.EventCollaboratorRole = eventCollaboratorRole;
        return this;
    }

    public EventCollaboratorBuilder WithConfirmationStatus(ConfirmationStatus confirmationStatus)
    {
        _eventCollaborator.ConfirmationStatus = confirmationStatus;
        return this;
    }

    public EventCollaboratorBuilder WithEventDate(DateOnly eventDate)
    {
        _eventCollaborator.EventDate = eventDate;
        return this;
    }

    public EventCollaboratorBuilder WithProposedDuration(Duration duration)
    {
        _eventCollaborator.ProposedDuration = duration;
        return this;
    }

    public EventCollaborator Build() => _eventCollaborator;
}
