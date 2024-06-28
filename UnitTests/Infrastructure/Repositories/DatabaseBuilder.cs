using Infrastructure;
using Infrastructure.DataModels;

namespace UnitTests.Infrastructure.Repositories;

public class DatabaseBuilder
{
    private readonly DbContextEventCalendar _dbContext;

    public DatabaseBuilder(DbContextEventCalendar dbContext)
    {
        _dbContext = dbContext;
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

    public DatabaseBuilder WithEventCollaborator(EventCollaboratorDataModel eventCollaboratorDataModel)
    {
        _dbContext.EventCollaborators.Add(eventCollaboratorDataModel);
        return this;
    }

    public DatabaseBuilder WithSharedCalendar(SharedCalendarDataModel sharedCalendarDataModel)
    {
        _dbContext.SharedCalendars.Add(sharedCalendarDataModel);
        return this;
    }

    public async Task Build()
    {
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();
    }
}

