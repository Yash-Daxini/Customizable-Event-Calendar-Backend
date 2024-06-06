using AutoMapper;
using Infrastructure;
using Infrastructure.Profiles;
using Infrastructure.Repositories;

namespace UnitTests.Infrastructure.Repositories.EventCollaboratorRepositoryTests;

public class DeleteEventCollaboratorsByEventId
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;
    public DeleteEventCollaboratorsByEventId()
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
    public async Task Should_DeleteEventCollaborators_When_EventCollaboratorsHasGivenEventId()
    {
        _dbContext = await new EventCollaboratorRepositoryDBContext().GetDatabaseContext();

        EventCollaboratorRepository eventCollaboratorRepository = new(_dbContext, _mapper);

        await eventCollaboratorRepository.DeleteEventCollaboratorsByEventId(1);

        bool isContains = _dbContext.EventCollaborators.Any(e => e.EventId == 1);

        Assert.False(isContains);
    }
}
