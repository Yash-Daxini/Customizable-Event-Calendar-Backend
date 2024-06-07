using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using NSubstitute;
using Infrastructure.DataModels;

namespace UnitTests.Infrastructure.Repositories.EventCollaboratorRepositoryTests;

public class UpdateEventCollaborator
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;

    public UpdateEventCollaborator()
    {
        _mapper = Substitute.For<IMapper>();
    }

    [Fact]
    public async Task Should_UpdateEventCollaborator_When_EventCollaboratorAvailableWithGivenId()
    {
        _dbContext = await new EventCollaboratorRepositoryDBContext().GetDatabaseContext();

        EventCollaboratorRepository eventCollaboratorRepository = new(_dbContext, _mapper);

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

        _dbContext.ChangeTracker.Clear();

        await eventCollaboratorRepository.Update(eventCollaborator);

        EventCollaborator? updatedEventCollaborator = await eventCollaboratorRepository.GetEventCollaboratorById(1);

        Assert.Equivalent(eventCollaborator,updatedEventCollaborator);
    }
}
