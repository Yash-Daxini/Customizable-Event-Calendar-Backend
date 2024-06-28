using System.Data.Common;
using Infrastructure;
using Infrastructure.DataModels;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infrastructure.Repositories;

public class DatabaseBuilder
{
    private readonly DbConnection _connection;

    private readonly DbContextEventCalendar _dbContext;

    public DatabaseBuilder()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<DbContextEventCalendar>()
            .UseSqlite(_connection)
            .Options;

        _dbContext = new DbContextEventCalendar(options);
        _dbContext.Database.EnsureCreated();
    }

    public DatabaseBuilder WithUser(UserDataModel userDataModel)
    {
        _dbContext.Users.Add(userDataModel);
        return this;
    }

    public DatabaseBuilder WithEvent(EventDataModel eventDataModel)
    {
        _dbContext.Events.Add(eventDataModel);
        return this;
    }

    public DatabaseBuilder WithSharedCalendar(SharedCalendarDataModel sharedCalendarDataModel)
    {
        _dbContext.SharedCalendars.Add(sharedCalendarDataModel);
        return this;
    }

    public DbContextEventCalendar Build()
    {
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();
        return _dbContext;
    }
}

