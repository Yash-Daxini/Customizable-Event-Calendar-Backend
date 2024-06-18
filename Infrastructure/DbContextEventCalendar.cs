using Infrastructure.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DbContextEventCalendar : IdentityDbContext<UserDataModel, IdentityRole<int>, int>
    {
        public DbContextEventCalendar(DbContextOptions<DbContextEventCalendar> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<EventDataModel> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dateOnlyConverter = new DateOnlyConverter();

            modelBuilder.Entity<EventDataModel>()
                .Property(e => e.EndDate)
                .HasConversion(dateOnlyConverter);

            modelBuilder.Entity<EventDataModel>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<EventDataModel>()
                .Property(e => e.StartDate)
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
                .HasOne(e => e.Sender)
                .WithMany()
                .HasForeignKey(e => e.SenderId)
                .IsRequired()   
                .OnDelete(DeleteBehavior.ClientSetNull);

            //Set Primary Key
            modelBuilder.Entity<SharedCalendarDataModel>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<SharedCalendarDataModel>()
                .HasOne(e => e.Receiver)
                .WithMany()
                .HasForeignKey(e => e.ReceiverId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);

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
