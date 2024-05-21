using Core.Domain.Enums;

namespace Core.Domain;

public class EventModel
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Location { get; set; }

    public DurationModel Duration { get; set; }

    public RecurrencePatternModel RecurrencePattern { get; set; }

    public List<ParticipantsByDate> DateWiseParticipants { get; set; } = [];

    public UserModel GetEventOrganizer()
    {
        return this.DateWiseParticipants
                   .SelectMany(participantsByDate => participantsByDate.Participants)
                   .First(participant => participant.ParticipantRole == ParticipantRole.Organizer).User;
    }
}
