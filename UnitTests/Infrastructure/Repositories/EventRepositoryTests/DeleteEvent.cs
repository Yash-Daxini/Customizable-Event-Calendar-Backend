using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.Profiles;
using Infrastructure.Repositories;

namespace UnitTests.Infrastructure.Repositories.EventRepositoryTests;

public class DeleteEvent
{
    private DbContextEventCalendar _dbContextEvent;
    private readonly IMapper _mapper;

    public DeleteEvent()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new EventProfile());
            mc.AddProfile(new UserProfile());
            mc.AddProfile(new EventCollaboratorProfile());
            mc.AddProfile(new UserProfile());
        });
        IMapper mapper = mappingConfig.CreateMapper();
        _mapper = mapper;
    }

    [Fact]
    public async Task Should_DeleteEvent_When_EventWithIdAvailable()
    {
        _dbContextEvent = await new EventRepositoryDBContext().GetDatabaseContext();

        EventRepository eventRepository = new(_dbContextEvent, _mapper);

        Event? eventObj = await eventRepository.GetEventById(1);

        _dbContextEvent.ChangeTracker.Clear();

        await eventRepository.Delete(eventObj);

        Event? deletedEvent = await eventRepository.GetEventById(1);

        Assert.Null(deletedEvent);
    }
        
}
