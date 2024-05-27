using Core.Domain.Enums;

namespace Core.Domain;

public class Event
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Location { get; set; }

    public string Description { get; set; }

    public Duration Duration { get; set; }

    public RecurrencePattern RecurrencePattern { get; set; }

    public List<EventCollaboratorsByDate> DateWiseParticipants { get; set; }

    public User GetEventOrganizer()
    {
        return this.DateWiseParticipants
                   .SelectMany(participantsByDate => participantsByDate.EventCollaborators)
                   .First(eventCollaborator => eventCollaborator.ParticipantRole == ParticipantRole.Organizer).User;
    }

    public bool IsProposedEventToGiveResponse()
    {
        return this.DateWiseParticipants
                   .SelectMany(participantsByDate => participantsByDate.EventCollaborators)
                   .ToList()
                   .Exists(eventCollaborator => eventCollaborator.ParticipantRole == ParticipantRole.Participant
                                        && (
                                                eventCollaborator.ConfirmationStatus == ConfirmationStatus.Pending
                                                || eventCollaborator.ConfirmationStatus == ConfirmationStatus.Proposed
                                            )
                                       );
    }

    public List<EventCollaborator> GetInviteesOfEvent()
    {
        return [.. this.DateWiseParticipants[0].EventCollaborators
                                      .Where(eventCollaborator => eventCollaborator.IsOrganizerOfEvent()
                                                            || eventCollaborator.IsParticipantOfEvent())];
    }
}
