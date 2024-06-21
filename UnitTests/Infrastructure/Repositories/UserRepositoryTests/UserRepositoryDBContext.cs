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

        if (dbContextEvent.Events.Count() <= 0)
        {
            dbContextEvent.Users.Add(new()
            {
                Id = 1,
                UserName = "a",
                NormalizedUserName ="A",
                Email = "abc@gmail.com",
                NormalizedEmail= "ABC@GMAIL.COM",
                PasswordHash = "AQAAAAIAAYagAAAAEFVo/8EEd6wiXBAHoU2ZdzjgEzJRnLm0PaXPO1q41Ns09QyF/L+BMTafbFxAALlKKg==",
                SecurityStamp = Guid.NewGuid().ToString(),
            });
            dbContextEvent.SaveChanges();
        }

        dbContextEvent.ChangeTracker.Clear();

        return dbContextEvent;
    }
}
