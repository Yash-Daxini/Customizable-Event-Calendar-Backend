using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infrastructure.Repositories.SharedCalendarRepositoryTests;

public class SharedCalendarRepositoryDBContext
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

        return dbContextEvent;
    }
}
