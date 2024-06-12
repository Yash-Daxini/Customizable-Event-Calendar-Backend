using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces;

namespace Infrastructure.DataModels;

[Table("EventCollaborator")]
public class EventCollaboratorDataModel : IModel
{
    public int Id { get; set; }

    public int EventId { get; set; }
    public virtual EventDataModel Event { get; set; }

    public int UserId { get; set; }
    public virtual UserDataModel User { get; set; }

    public string EventCollaboratorRole { get; set; }

    public string ConfirmationStatus { get; set; }

    public int? ProposedStartHour { get; set; }

    public int? ProposedEndHour { get; set; }

    public DateOnly EventDate { get; set; }
}
