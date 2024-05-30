using Core.Domain.Enums;
using Core.Interfaces;

namespace Core.Domain.Models;

public class Event : IEntity
{ 
    public int Id { get; set; }

    public string Title { get; set; }

    public string Location { get; set; }

    public string Description { get; set; }

    public Duration Duration { get; set; }

    public RecurrencePattern RecurrencePattern { get; set; }

    public List<EventCollaboratorsByDate> DateWiseEventCollaborators { get; set; }

    public User GetEventOrganizer()
    {
        return DateWiseEventCollaborators
                   .SelectMany(eventCollaboratorsByDate => eventCollaboratorsByDate.EventCollaborators)
                   .First(eventCollaborator => eventCollaborator.EventCollaboratorRole == EventCollaboratorRole.Organizer).User;
    }

    public bool IsProposedEventToGiveResponse()
    {
        return DateWiseEventCollaborators
                   .SelectMany(eventCollaboratorsByDate => eventCollaboratorsByDate.EventCollaborators)
                   .ToList()
                   .Exists(eventCollaborator => eventCollaborator.EventCollaboratorRole == EventCollaboratorRole.Participant
                                        && (
                                                eventCollaborator.ConfirmationStatus == ConfirmationStatus.Pending
                                                || eventCollaborator.ConfirmationStatus == ConfirmationStatus.Proposed
                                            )
                                       );
    }

    public List<EventCollaborator> GetInviteesOfEvent()
    {
        return [.. DateWiseEventCollaborators[0].EventCollaborators
                                      .Where(eventCollaborator => eventCollaborator.IsOrganizerOfEvent()
                                                            || eventCollaborator.IsEventCollaboratorOfEvent())];
    }
}
