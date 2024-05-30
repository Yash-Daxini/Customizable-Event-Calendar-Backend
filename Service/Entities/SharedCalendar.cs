using Core.Interfaces;

namespace Core.Entities;

public class SharedCalendar : IEntity
{
    public int Id { get; set; }

    public User Sender { get; set; }

    public User Receiver { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }
}
