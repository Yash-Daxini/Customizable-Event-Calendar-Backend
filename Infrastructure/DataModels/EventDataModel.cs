using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces;

namespace Infrastructure.DataModels;

[Table("Event")]
public class EventDataModel : IModel
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Location { get; set; }

    public string Description { get; set; }

    public int StartHour { get; set; }

    public int EndHour { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string? Frequency { get; set; }

    public int Interval { get; set; }

    public string? ByWeekDay { get; set; }

    public int? WeekOrder { get; set; }

    public int? ByMonthDay { get; set; }

    public int? ByMonth { get; set; }

    public int UserId { get; set; }

    public virtual List<EventCollaboratorDataModel> EventCollaborators { get; set; } = [];
}
