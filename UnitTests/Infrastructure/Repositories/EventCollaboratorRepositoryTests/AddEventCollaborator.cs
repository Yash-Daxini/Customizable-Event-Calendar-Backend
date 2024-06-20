using AutoMapper;
using Core.Entities;
using FluentAssertions;
using Infrastructure;
using Infrastructure.Repositories;

namespace UnitTests.Infrastructure.Repositories.EventCollaboratorRepositoryTests;

public class AddEventCollaborator : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;

    public AddEventCollaborator(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_AddEventCollaboratorAndReturnId_When_CallsRepositoryMethod()
    {
        //Arrange
        _dbContext = await new EventCollaboratorRepositoryDBContext().GetDatabaseContext();

        EventCollaborator eventCollaborator = new()
        {
            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
            EventDate = new DateOnly(),
            EventId = 1,
            User = new()
            {
                Id = 1,
                Name = "a",
                Email = "a"
            },
            ProposedDuration = null
        };

        EventCollaboratorRepository eventCollaboratorRepository = new(_dbContext, _mapper);

        //Act
        int id = await eventCollaboratorRepository.Add(eventCollaborator);

        bool isContains = _dbContext.EventCollaborators.Any(e => e.Id == id);

        //Assert
        id.Should().BeGreaterThan(0);
        isContains.Should().BeTrue();
    }
}
