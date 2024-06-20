using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using FluentAssertions;

namespace UnitTests.Infrastructure.Repositories.EventCollaboratorRepositoryTests;

public class DeleteEventCollaborator : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;

    public DeleteEventCollaborator(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_UpdateEventCollaborator_When_EventCollaboratorAvailableWithGivenId()
    {
        _dbContext = await new EventCollaboratorRepositoryDBContext().GetDatabaseContext();

        EventCollaborator eventCollaborator = new()
        {
            Id = 1,
            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
            EventDate = new DateOnly(),
            EventId = 1,
            User = new()
            {
                Id = 1,
                Name = "a",
                Email = "a",
                Password = "a",
            },
            ProposedDuration = null
        };

        EventCollaboratorRepository eventCollaboratorRepository = new(_dbContext, _mapper);

        await eventCollaboratorRepository.Delete(eventCollaborator);

        EventCollaborator? deletedEventCollaborator = await eventCollaboratorRepository.GetEventCollaboratorById(1);

        deletedEventCollaborator.Should().BeNull(); 
    }
}
