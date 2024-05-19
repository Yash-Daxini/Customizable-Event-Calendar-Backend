namespace Infrastructure.DomainEntities
{
    public class ParticipantsByDate
    {
        public DateOnly EventDate { get; set; }

        public List<ParticipantModel> Participants { get; set; }
    }
}
