using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.DataModels
{
    [Table("Event")]
    public class Event
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public int EventStartHour { get; set; }

        public int EventEndHour { get; set; }

        public DateOnly EventStartDate { get; set; }

        public DateOnly EventEndDate { get; set; }

        public string? Frequency { get; set; }

        public int Interval { get; set; }

        public string? ByWeekDay { get; set; }

        public int? WeekOrder { get; set; }

        public int? ByMonthDay { get; set; }

        public int? ByMonth { get; set; }

        public virtual List<EventCollaborator> EventCollaborators { get; set; } = new List<EventCollaborator>();
    }
}
