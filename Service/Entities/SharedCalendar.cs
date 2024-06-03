using System.Xml.Linq;
using Core.Interfaces;

namespace Core.Entities;

public class SharedCalendar : IEntity
{
    public int Id { get; set; }

    public User Sender { get; set; }

    public User Receiver { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is SharedCalendar sharedCalendar)
        {
            return this.Id == sharedCalendar.Id
                && this.Sender.Equals(sharedCalendar.Sender)
                && this.Receiver.Equals(sharedCalendar.Receiver)
                && this.FromDate.Equals(sharedCalendar.FromDate)
                && this.ToDate.Equals(sharedCalendar.ToDate);    
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Id,
                                this.Sender,
                                this.Receiver,
                                this.FromDate,
                                this.ToDate);
    }
}
