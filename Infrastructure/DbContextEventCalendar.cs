using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DbContextEventCalendar : DbContext
    {
        public DbContextEventCalendar(DbContextOptions<DbContextEventCalendar> options) : base(options)
        {

        }

        public DbSet<EventDataModel> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var dateOnlyConverter = new DateOnlyConverter();

            modelBuilder.Entity<EventDataModel>()
                .Property(e => e.EventEndDate)
                .HasConversion(dateOnlyConverter);

            modelBuilder.Entity<EventDataModel>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<EventDataModel>()
                .Property(e => e.EventStartDate)
                .HasConversion(dateOnlyConverter);

            modelBuilder.Entity<EventCollaboratorDataModel>()
            .HasOne(ec => ec.Event)
            .WithMany(e => e.EventCollaborators)
            .HasForeignKey(ec => ec.EventId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_EventCollaborator_Event");

            // Configure column names if needed
            modelBuilder.Entity<EventCollaboratorDataModel>()
                .Property(ec => ec.EventId)
                .HasColumnName("EventId");

            modelBuilder.Entity<EventCollaboratorDataModel>()
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

        public DbSet<EventCollaboratorDataModel> EventCollaborators { get; set; }

        public DbSet<SharedCalendarDataModel> SharedCalendars { get; set; }

        public DbSet<UserDataModel> Users { get; set; }
    }
}
