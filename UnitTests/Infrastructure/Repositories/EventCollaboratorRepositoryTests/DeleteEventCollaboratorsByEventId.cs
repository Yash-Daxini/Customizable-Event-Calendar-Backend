using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.DataModels;
using Infrastructure.Repositories;
using NSubstitute;

namespace UnitTests.Infrastructure.Repositories.EventCollaboratorRepositoryTests;

public class DeleteEventCollaboratorsByEventId
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;
    public DeleteEventCollaboratorsByEventId()
    {
        _mapper = Substitute.For<IMapper>();
    }

    [Fact]
    public async Task Should_DeleteEventCollaborators_When_EventCollaboratorsHasGivenEventId()
    {
        _dbContext = await new EventCollaboratorRepositoryDBContext().GetDatabaseContext();

        EventCollaboratorDataModel eventCollaboratorDataModel = new()
        {
            EventId = 1,
            UserId = 1,
            ParticipantRole = "Organizer",
            ConfirmationStatus = "Accept",
            ProposedStartHour = null,
            ProposedEndHour = null,
            EventDate = new DateOnly(2024, 6, 7)
        };

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

        _mapper.Map<EventCollaboratorDataModel>(eventCollaborator).ReturnsForAnyArgs(eventCollaboratorDataModel);

        EventCollaboratorRepository eventCollaboratorRepository = new(_dbContext, _mapper);

        await eventCollaboratorRepository.DeleteEventCollaboratorsByEventId(1);

        bool isContains = _dbContext.EventCollaborators.Any(e => e.EventId == 1);

        Assert.False(isContains);
    }
}
