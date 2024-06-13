using System.Data.Common;
using Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infrastructure.Repositories.SharedCalendarRepositoryTests;

public class SharedCalendarRepositoryDBContext
{
    private readonly DbConnection _connection;

    public SharedCalendarRepositoryDBContext()
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
            dbContextEvent.Users.Add(new()
            {
                Id = 1,
                Name = "a",
                Email = "a",
                Password = "a",
            });
            dbContextEvent.Users.Add(new()
            {
                Id = 2,
                Name = "b",
                Email = "b",
                Password = "b",
            });
            dbContextEvent.Users.Add(new()
            {
                Id = 3,
                Name = "c",
                Email = "c",
                Password = "c",
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
