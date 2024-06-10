using Core.Interfaces;

namespace Core.Entities;

public class SharedCalendar : IEntity
{
    public SharedCalendar(int Id, User Sender, User Receiver, DateOnly FromDate, DateOnly ToDate)
    {
        if (!ValidateProperties(Id, Sender, Receiver, FromDate, ToDate))
            throw new ArgumentException("Invalid property !");

        this.Id = Id;
        this.Sender = Sender;
        this.Receiver = Receiver;
        this.FromDate = FromDate;
        this.ToDate = ToDate;
    }

    public SharedCalendar() { }

    private bool ValidateProperties(int id, User sender, User receiver, DateOnly fromDate, DateOnly toDate)
    {
        return id >= 0
            && sender is not null
            && receiver is not null
            && fromDate <= toDate;
    }

    public int Id { get; set; }

    public User Sender { get; set; }

    public User Receiver { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }
}
