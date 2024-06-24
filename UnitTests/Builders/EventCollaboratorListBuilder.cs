using Core.Entities;
using Core.Entities.Enums;

namespace UnitTests.Builders;

public class EventCollaboratorListBuilder
{
    private int EventId {  get; set; }

    public EventCollaboratorListBuilder(int eventId)
    {
        EventId = eventId;
    }

    private readonly List<EventCollaborator> _eventCollaborators = [];

    public EventCollaboratorListBuilder WithOrganizer(User user, DateOnly eventDate)
    {
        _eventCollaborators.Add(new EventCollaboratorBuilder()
                                .WithEventCollaboratorRole(EventCollaboratorRole.Organizer)
                                .WithConfirmationStatus(ConfirmationStatus.Accept)
                                .WithUser(user)
                                .WithEventDate(eventDate)
                                .WithEventId(EventId)
                                .Build());

        return this;
    }

    public EventCollaboratorListBuilder WithParticipant(User user, ConfirmationStatus confirmationStatus, DateOnly eventDate, Duration proposedDuration)
    {
        _eventCollaborators.Add(new EventCollaboratorBuilder()
                                .WithEventCollaboratorRole(EventCollaboratorRole.Participant)
                                .WithConfirmationStatus(confirmationStatus)
                                .WithUser(user)
                                .WithEventDate(eventDate)
                                .WithEventId(EventId)
                                .WithProposedDuration(proposedDuration)
                                .Build());

        return this;
    }

    public EventCollaboratorListBuilder WithCollaborator(User user, DateOnly eventDate)
    {
        _eventCollaborators.Add(new EventCollaboratorBuilder()
                                .WithEventCollaboratorRole(EventCollaboratorRole.Collaborator)
                                .WithConfirmationStatus(ConfirmationStatus.Accept)
                                .WithUser(user)
                                .WithEventDate(eventDate)
                                .WithEventId(EventId)
                                .Build());

        return this;
    }

    public List<EventCollaborator> Build() => _eventCollaborators;
}
