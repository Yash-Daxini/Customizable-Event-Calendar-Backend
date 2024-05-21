using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DbContextEventCalendar : DbContext
    {
        public DbContextEventCalendar(DbContextOptions<DbContextEventCalendar> options) : base(options)
        {
            
        }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dateOnlyConverter = new DateOnlyConverter();

            modelBuilder.Entity<Event>()
                .Property(e => e.EventEndDate)
                .HasConversion(dateOnlyConverter);
            
            modelBuilder.Entity<Event>()
                .Property(e => e.EventStartDate)
                .HasConversion(dateOnlyConverter);
            
            modelBuilder.Entity<EventCollaborator>()
                .Property(e => e.EventDate)
                .HasConversion(dateOnlyConverter);
            
            modelBuilder.Entity<SharedCalendarDataModel>()
                .Property(e => e.FromDate)
                .HasConversion(dateOnlyConverter);
            
            modelBuilder.Entity<SharedCalendarDataModel>()
                .Property(e => e.ToDate)
                .HasConversion(dateOnlyConverter);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<EventCollaborator> EventCollaborators { get; set; }

        public DbSet<SharedCalendarDataModel> SharedCalendars { get; set; }

        public DbSet<UserDataModel> Users { get; set; }
    }
}
