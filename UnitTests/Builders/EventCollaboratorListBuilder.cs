using Core.Entities;
using Core.Entities.Enums;

namespace UnitTests.Builders;

public class EventCollaboratorListBuilder
{
    private readonly List<EventCollaborator> _eventCollaborators = [];

    public EventCollaboratorListBuilder WithOrganizer(User user, DateOnly eventDate)
    {
        _eventCollaborators.Add(new EventCollaborator()
        {
            EventCollaboratorRole = EventCollaboratorRole.Organizer,
            ConfirmationStatus = ConfirmationStatus.Accept,
            User = user,
            EventDate = eventDate,
        });

        return this;
    }

    public EventCollaboratorListBuilder WithParticipant(User user, ConfirmationStatus confirmationStatus, DateOnly eventDate)
    {
        _eventCollaborators.Add(new EventCollaborator()
        {
            EventCollaboratorRole = EventCollaboratorRole.Participant,
            ConfirmationStatus = confirmationStatus,
            User = user,
            EventDate = eventDate,
        });

        return this;
    }

    public List<EventCollaborator> Build() => _eventCollaborators;
}
