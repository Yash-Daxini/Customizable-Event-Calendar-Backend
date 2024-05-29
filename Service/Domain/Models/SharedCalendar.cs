using Core.Interfaces;

namespace Core.Domain.Models;

public class SharedCalendar : IEntity
{
    public int Id { get; set; }

    public User SenderUser { get; set; }

    public User ReceiverUser { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }
}
