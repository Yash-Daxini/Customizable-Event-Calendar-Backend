using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class UserRepositoryDBContext
{
    public async Task<DbContextEventCalendar> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<DbContextEventCalendar>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
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
