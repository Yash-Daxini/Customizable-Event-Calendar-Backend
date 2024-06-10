using Core.Entities;
using Core.Entities.Enums;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NullArgumentException = Core.Exceptions.NullArgumentException;

namespace UnitTests.ApplicationCore.Services.EventCollaboratorServiceTests;

public class UpdateEventCollaborator
{
    private readonly IEventService _eventService;
    private readonly IEventCollaboratorRepository _eventCollaboratorRepository;
    private readonly IEventCollaboratorService _eventCollaboratorService;

    public UpdateEventCollaborator()
    {
        _eventCollaboratorRepository = Substitute.For<IEventCollaboratorRepository>();
        _eventService = Substitute.For<IEventService>();
        _eventCollaboratorService = new EventCollaboratorService(_eventCollaboratorRepository, _eventService);
    }

    [Fact]
    public async Task Should_UpdateEventCollaborator_When_CallsRepositoryMethod()
    {
        EventCollaborator eventCollaborator = new()
        {
            Id = 1,
            EventCollaboratorRole = EventCollaboratorRole.Organizer,
            ConfirmationStatus = ConfirmationStatus.Accept,
            EventDate = new DateOnly(2024, 6, 2),
            EventId = 1,
            ProposedDuration = null,
            User = new User() { Id = 1, Name = "Test", Email = "Test@gmail.com", Password = "Password" }

        };

        _eventCollaboratorRepository.GetEventCollaboratorById(1).Returns(eventCollaborator);

        await _eventCollaboratorService.UpdateEventCollaborator(eventCollaborator);
        _eventCollaboratorRepository.Received().Update(eventCollaborator);
    }

    [Fact]
    public async Task Should_ThrowException_When_EventCollaboratorIdIsNotPresent()
    {
        EventCollaborator eventCollaborator = new()
        {
            Id = 1,
            EventCollaboratorRole = EventCollaboratorRole.Organizer,
            ConfirmationStatus = ConfirmationStatus.Accept,
            EventDate = new DateOnly(2024, 6, 2),
            EventId = 1,
            ProposedDuration = null,
            User = new User() { Id = 1, Name = "Test", Email = "Test@gmail.com", Password = "Password" }
        };

        _eventCollaboratorRepository.GetEventCollaboratorById(1).ReturnsNull();

        await Assert.ThrowsAsync<NotFoundException>(async () => await _eventCollaboratorService.UpdateEventCollaborator(eventCollaborator));

        _eventCollaboratorRepository.DidNotReceive().Update(eventCollaborator);
    }

    [Fact]
    public async Task Should_ThrowException_When_EventCollaboratorIsNull()
    {
        EventCollaborator eventCollaborator = null;

        _eventCollaboratorRepository.GetEventCollaboratorById(1).ReturnsNull();

        await Assert.ThrowsAsync<NullArgumentException>(async () => await _eventCollaboratorService.UpdateEventCollaborator(eventCollaborator));

        _eventCollaboratorRepository.DidNotReceive().Update(eventCollaborator);
    }
}
