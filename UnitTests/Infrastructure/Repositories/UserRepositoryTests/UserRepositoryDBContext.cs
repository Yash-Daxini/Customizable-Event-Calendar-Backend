using System.Data.Common;
using Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class UserRepositoryDBContext
{
    private readonly DbConnection _connection;

    public UserRepositoryDBContext()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
    }

    public DbContextEventCalendar GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<DbContextEventCalendar>()
            .UseSqlite(_connection)
            .Options;

        var dbContextEvent = new DbContextEventCalendar(options);

        dbContextEvent.Database.EnsureCreated();

        return dbContextEvent;
    }
}
