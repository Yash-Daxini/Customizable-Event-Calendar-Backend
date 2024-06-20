using AutoMapper;
using Core.Entities;
using FluentAssertions;
using Infrastructure;
using Infrastructure.Repositories;

namespace UnitTests.Infrastructure.Repositories.EventCollaboratorRepositoryTests;

public class DeleteEventCollaboratorsByEventId : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;
    public DeleteEventCollaboratorsByEventId(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_DeleteEventCollaborators_When_EventCollaboratorsHasGivenEventId()
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

        await eventCollaboratorRepository.DeleteEventCollaboratorsByEventId(1);

        bool isContains = _dbContext.EventCollaborators.Any(e => e.EventId == 1);

        isContains.Should().BeFalse();
    }
}
