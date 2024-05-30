namespace WebAPI.Dtos;

public class SharedCalendarDto
{
    public int Id { get; set; }

    public UserDto Sender { get; set; }

    public UserDto Receiver { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }
}
