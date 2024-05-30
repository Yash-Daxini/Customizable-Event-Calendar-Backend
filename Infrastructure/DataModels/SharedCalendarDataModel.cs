using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces;

namespace Infrastructure.DataModels;

[Table("SharedCalendar")]
public class SharedCalendarDataModel : IModel
{
    public int Id { get; set; }

    public int SenderId { get; set; }
    public virtual UserDataModel Sender { get; set; } = null!;

    public int ReceiverId { get; set; }
    public virtual UserDataModel Receiver { get; set; } = null!;

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }


}
