using System.Data.Common;
using Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infrastructure.Repositories.EventCollaboratorRepositoryTests;

public class EventCollaboratorRepositoryDBContext
{
    private readonly DbConnection _connection;
    public EventCollaboratorRepositoryDBContext()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
    }

    public async Task<DbContextEventCalendar> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<DbContextEventCalendar>()
            .UseSqlite(_connection)
            .Options;

        var dbContextEvent = new DbContextEventCalendar(options);
        dbContextEvent.Database.EnsureCreated();
        if (await dbContextEvent.Events.CountAsync() <= 0)
        {
            dbContextEvent.Events.Add(new()
            {
                Id = 1,
                Title = "Test",
                Description = "Test",
                Location = "Test",
                UserId = 1,
                StartHour = 1,
                EndHour = 2,
                StartDate = new DateOnly(2024, 6, 7),
                EndDate = new DateOnly(2024, 6, 7),
                Frequency = "None",
                Interval = 1,
                ByMonth = null,
                ByMonthDay = null,
                ByWeekDay = null,
            });
            dbContextEvent.Events.Add(new()
            {
                Id = 2,
                Title = "Test1",
                Description = "Test1",
                Location = "Test1",
                UserId = 2,
                StartHour = 2,
                EndHour = 3,
                StartDate = new DateOnly(2024, 6, 7),
                EndDate = new DateOnly(2024, 6, 7),
                Frequency = "Daily",
                Interval = 1,
                ByMonth = null,
                ByMonthDay = null,
                ByWeekDay = null,
            });
            dbContextEvent.EventCollaborators.Add(new()
            {
                Id = 1,
                EventId = 1,
                UserId = 1,
                EventCollaboratorRole = "Organizer",
                ConfirmationStatus = "Accept",
                ProposedStartHour = null,
                ProposedEndHour = null,
                EventDate = new DateOnly(2024, 6, 7)
            });
            dbContextEvent.EventCollaborators.Add(new()
            {
                Id = 2,
                EventId = 2,
                UserId = 2,
                EventCollaboratorRole = "Organizer",
                ConfirmationStatus = "Accept",
                ProposedStartHour = null,
                ProposedEndHour = null,
                EventDate = new DateOnly(2024, 6, 7)
            });
            dbContextEvent.EventCollaborators.Add(new()
            {
                Id = 3,
                EventId = 2,
                UserId = 3,
                EventCollaboratorRole = "Participant",
                ConfirmationStatus = "Proposed",
                ProposedStartHour = 1,
                ProposedEndHour = 2,
                EventDate = new DateOnly(2024, 6, 7)
            });
            dbContextEvent.Users.Add(new()
            {
                Id = 1,
                UserName = "a",
                Email = "a",
                //Password = "a",
            });
            dbContextEvent.Users.Add(new()
            {
                Id = 2,
                UserName = "b",
                Email = "b",
                //Password = "b",
            });
            dbContextEvent.Users.Add(new()
            {
                Id = 3,
                UserName = "c",
                Email = "c",
                //Password = "c",
            });
            dbContextEvent.SharedCalendars.Add(new()
            {
                Id = 1,
                SenderId = 1,
                ReceiverId = 2,
                FromDate = new DateOnly(2024, 6, 7),
                ToDate = new DateOnly(2024, 6, 7)
            });


            dbContextEvent.SaveChanges();
        }

        dbContextEvent.ChangeTracker.Clear();

        return dbContextEvent;
    }
}
