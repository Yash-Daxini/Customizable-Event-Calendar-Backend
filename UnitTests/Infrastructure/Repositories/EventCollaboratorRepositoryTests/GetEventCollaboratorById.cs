using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.DataModels;
using Infrastructure.Repositories;
using NSubstitute;

namespace UnitTests.Infrastructure.Repositories.EventCollaboratorRepositoryTests;

public class GetEventCollaboratorById
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;

    public GetEventCollaboratorById()
    {
        _mapper = Substitute.For<IMapper>();
    }

    [Fact]
    public async Task Should_ReturnEventCollaborators_When_EventCollaboratorAvailableWithGivenId()
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

        _mapper.Map<EventCollaboratorDataModel>(eventCollaborator).ReturnsForAnyArgs(eventCollaboratorDataModel);

        _mapper.Map<EventCollaborator>(eventCollaboratorDataModel).ReturnsForAnyArgs(eventCollaborator);

        EventCollaboratorRepository eventCollaboratorRepository = new(_dbContext, _mapper);

        EventCollaborator? eventCollaboratorById =  await eventCollaboratorRepository.GetEventCollaboratorById(1);

        Assert.NotNull(eventCollaboratorById);
    }
    
    [Fact]
    public async Task Should_ReturnNull_When_EventCollaboratorNotAvailableWithGivenId()
    {
        _dbContext = await new EventCollaboratorRepositoryDBContext().GetDatabaseContext();

        EventCollaboratorRepository eventCollaboratorRepository = new(_dbContext, _mapper);

        EventCollaborator? eventCollaborator =  await eventCollaboratorRepository.GetEventCollaboratorById(5);

        Assert.Null(eventCollaborator);
    }
}
