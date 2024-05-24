using Core.Domain.Enums;

namespace Core.Domain;

public class Event
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Location { get; set; }

    public Duration Duration { get; set; }

    public RecurrencePattern RecurrencePattern { get; set; }

    public List<ParticipantsByDate> DateWiseParticipants { get; set; }

    public User GetEventOrganizer()
    {
        return this.DateWiseParticipants
                   .SelectMany(participantsByDate => participantsByDate.Participants)
                   .First(participant => participant.ParticipantRole == ParticipantRole.Organizer).User;
    }

    public bool IsProposedEventToGiveResponse()
    {
        return this.DateWiseParticipants
                   .SelectMany(participantsByDate => participantsByDate.Participants)
                   .ToList()
                   .Exists(participant => participant.ParticipantRole == ParticipantRole.Participant
                                        && (
                                                participant.ConfirmationStatus == ConfirmationStatus.Pending
                                                || participant.ConfirmationStatus == ConfirmationStatus.Proposed
                                            )
                                       );
    }
}
