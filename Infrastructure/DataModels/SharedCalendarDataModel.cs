using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.DataModels
{
    [Table("SharedCalendar")]
    public class SharedCalendarDataModel
    {
        public int Id { get; set; }

        public int SenderUserId { get; set; }
        public virtual UserDataModel ReceiverUser { get; set; } = null!;

        public int ReceiverUserId { get; set; }
        public virtual UserDataModel SenderUser { get; set; } = null!;

        public DateOnly FromDate { get; set; }

        public DateOnly ToDate { get; set; }


    }
}
