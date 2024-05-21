namespace Core.Domain;

public class ParticipantsByDate
{
    public DateOnly EventDate { get; set; }

    public List<ParticipantModel> Participants { get; set; }
}
