using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DbContextEvent : DbContext
    {
        public DbContextEvent(DbContextOptions<DbContextEvent> options) : base(options)
        {
            
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<EventCollaborator> EventCollaborators { get; set; }

        public DbSet<SharedCalendar> SharedCalendars { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
