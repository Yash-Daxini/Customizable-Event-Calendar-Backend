using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.DataModels;
using Infrastructure.Repositories;
using NSubstitute;

namespace UnitTests.Infrastructure.Repositories.EventCollaboratorRepositoryTests;

public class AddEventCollaborator
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;

    public AddEventCollaborator()
    {
        _mapper = Substitute.For<IMapper>();
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

        EventCollaboratorRepository eventCollaboratorRepository = new(_dbContext, _mapper);

        //Act
        int id = await eventCollaboratorRepository.Add(eventCollaborator);

        bool isContains = _dbContext.EventCollaborators.Any(e => e.Id == id);

        //Assert
        Assert.True(id > 0 && isContains);
    }
}
