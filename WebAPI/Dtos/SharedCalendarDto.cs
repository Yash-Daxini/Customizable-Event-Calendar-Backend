namespace WebAPI.Dtos;

public class SharedCalendarDto
{
    public int Id { get; set; }

    public UserDto SenderUser { get; set; }

    public UserDto ReceiverUser { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }
}
