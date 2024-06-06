using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infrastructure.Repositories.EventCollaboratorRepositoryTests;

public class EventCollaboratorRepositoryDBContext
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
            dbContextEvent.EventCollaborators.Add(new()
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
            dbContextEvent.EventCollaborators.Add(new()
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
            dbContextEvent.EventCollaborators.Add(new()
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
            dbContextEvent.SaveChanges();
        }

        return dbContextEvent;
    }
}
