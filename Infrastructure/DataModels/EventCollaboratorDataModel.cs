using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.DataModels
{
    [Table("EventCollaborator")]
    public class EventCollaboratorDataModel
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public int UserId { get; set; }

        public string ParticipantRole { get; set; }

        public string ConfirmationStatus { get; set; }

        public int? ProposedStartHour { get; set; }

        public int? ProposedEndHour { get; set; }

        public DateOnly EventDate { get; set; }

        public virtual UserDataModel? User { get; set; }

        public virtual EventDataModel Event { get; set; }
    }
}
