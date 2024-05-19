namespace Infrastructure.DataModels
{
    public class SharedCalendar
    {
        public int Id { get; set; }

        public int SenderUserId { get; set; }

        public int ReceiverUserId { get; set; }

        public DateOnly FromDate { get; set; }

        public DateOnly ToDate { get; set; }
    }
}
