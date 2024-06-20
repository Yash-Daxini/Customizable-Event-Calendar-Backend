using AutoMapper;
using Core.Entities;
using FluentAssertions;
using Infrastructure;
using Infrastructure.Repositories;

namespace UnitTests.Infrastructure.Repositories.EventCollaboratorRepositoryTests;

public class GetEventCollaboratorById : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;

    public GetEventCollaboratorById(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_ReturnEventCollaborators_When_EventCollaboratorAvailableWithGivenId()
    {
        _dbContext = await new EventCollaboratorRepositoryDBContext().GetDatabaseContext();

        EventCollaboratorRepository eventCollaboratorRepository = new(_dbContext, _mapper);

        EventCollaborator? eventCollaboratorById =  await eventCollaboratorRepository.GetEventCollaboratorById(3);

        eventCollaboratorById.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Should_ReturnNull_When_EventCollaboratorNotAvailableWithGivenId()
    {
        _dbContext = await new EventCollaboratorRepositoryDBContext().GetDatabaseContext();

        EventCollaboratorRepository eventCollaboratorRepository = new(_dbContext, _mapper);

        EventCollaborator? eventCollaborator =  await eventCollaboratorRepository.GetEventCollaboratorById(5);

        eventCollaborator.Should().BeNull();
    }
}
