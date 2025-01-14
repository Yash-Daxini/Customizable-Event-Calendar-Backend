namespace WebAPI.Dtos;

public class SharedCalendarDto
{
    public int Id { get; set; }

    public int SenderUserId { get; set; }

    public int ReceiverUserId { get; set; }

    public UserResponseDto Sender{ get; set; }

    public UserResponseDto Receiver { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }
}
