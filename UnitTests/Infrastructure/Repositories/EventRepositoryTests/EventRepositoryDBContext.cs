using System.Data.Common;
using Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infrastructure.Repositories.EventRepositoryTests;

public class EventRepositoryDBContext : IDisposable
{
    private readonly DbConnection _connection;


    public EventRepositoryDBContext()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
    }

    public void Dispose() => _connection.Dispose();

    public async Task<DbContextEventCalendar> GetDatabaseContext()
    {

        var options = new DbContextOptionsBuilder<DbContextEventCalendar>()
            .UseSqlite(_connection)
            .Options;

        var dbContextEvent = new DbContextEventCalendar(options);
        dbContextEvent.Database.EnsureCreated();
        if (await dbContextEvent.Events.CountAsync() <= 0)
        {
            dbContextEvent.Events.Add(new ()
            {
                Id = 1,
                Title = "Test",
                Description = "Test",
                Location = "Test",
                UserId = 1,
                EventStartHour = 1,
                EventEndHour = 2,
                EventStartDate = new DateOnly(2024, 6, 7),
                EventEndDate = new DateOnly(2024, 6, 7),
                Frequency = "None",
                Interval = 1,
                ByMonth = null,
                ByMonthDay = null,
                ByWeekDay = null,
            });
            dbContextEvent.Events.Add(new ()
            {
                Id = 2,
                Title = "Test1",
                Description = "Test1",
                Location = "Test1",
                UserId = 2,
                EventStartHour = 2,
                EventEndHour = 3,
                EventStartDate = new DateOnly(2024, 6, 7),
                EventEndDate = new DateOnly(2024, 6, 7),
                Frequency = "Daily",
                Interval = 1,
                ByMonth = null,
                ByMonthDay = null,
                ByWeekDay = null,
            });
            dbContextEvent.EventCollaborators.Add(new ()
            {
                Id = 1,
                EventId = 1,
                UserId = 1,
                ParticipantRole = "Organizer",
                ConfirmationStatus = "Accept",
                ProposedStartHour = null,
                ProposedEndHour = null,
                EventDate = new DateOnly(2024, 6, 7)
            });
            dbContextEvent.EventCollaborators.Add(new ()
            {
                Id = 2,
                EventId = 2,
                UserId = 2,
                ParticipantRole = "Organizer",
                ConfirmationStatus = "Accept",
                ProposedStartHour = null,
                ProposedEndHour = null,
                EventDate = new DateOnly(2024, 6, 7)
            });
            dbContextEvent.EventCollaborators.Add(new ()
            {
                Id = 3,
                EventId = 2,
                UserId = 3,
                ParticipantRole = "Participant",
                ConfirmationStatus = "Pending",
                ProposedStartHour = null,
                ProposedEndHour = null,
                EventDate = new DateOnly(2024, 6, 7)
            });
            dbContextEvent.Users.Add(new ()
            {
                Id = 1,
                Name = "a",
                Email = "a",
                Password = "a",
            });
            dbContextEvent.Users.Add(new ()
            {
                Id = 2,
                Name = "b",
                Email = "b",
                Password = "b",
            });
            dbContextEvent.Users.Add(new ()
            {
                Id = 3,
                Name = "c",
                Email = "c",
                Password = "c",
            });
            dbContextEvent.SharedCalendars.Add( new()
            {
                Id = 1,
                SenderId = 1,
                ReceiverId = 2,
                FromDate = new DateOnly(2024,6,7),
                ToDate = new DateOnly(2024,6,7)
            });
            dbContextEvent.SaveChanges();
        }

        return dbContextEvent;
    }
}
