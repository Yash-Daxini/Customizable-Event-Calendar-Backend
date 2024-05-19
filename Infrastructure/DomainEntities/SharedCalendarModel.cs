namespace Infrastructure.DomainEntities
{
    public class SharedCalendarModel
    {
        public int Id { get; set; }

        public UserModel SenderUser { get; set; }

        public UserModel ReceiverUser { get; set; }

        public DateOnly FromDate { get; set; }

        public DateOnly ToDate { get; set; }
    }
}
