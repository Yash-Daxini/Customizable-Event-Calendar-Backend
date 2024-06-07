using System.Data.Common;
using Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class UserRepositoryDBContext : IDisposable
{
    private readonly DbConnection _connection;
    public UserRepositoryDBContext()
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
            dbContextEvent.Users.Add(new()
            {
                Id = 1,
                Name = "a",
                Email = "a",
                Password = "a",
            });
            dbContextEvent.SaveChanges();
        }

        return dbContextEvent;
    }
}
