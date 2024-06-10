using Core.Entities;
using Core.Entities.Enums;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using NullArgumentException = Core.Exceptions.NullArgumentException;

namespace UnitTests.ApplicationCore.Services.EventCollaboratorServiceTests;

public class AddEventCollaborator
{
    private readonly IEventService _eventService;
    private readonly IEventCollaboratorRepository _eventCollaboratorRepository;
    private readonly IEventCollaboratorService _eventCollaboratorService;

    public AddEventCollaborator()
    {
        _eventCollaboratorRepository = Substitute.For<IEventCollaboratorRepository>();
        _eventService = Substitute.For<IEventService>();
        _eventCollaboratorService = new EventCollaboratorService(_eventCollaboratorRepository, _eventService);
    }

    [Fact]
    public async Task Should_ReturnsAddedEventCollaboratorId_When_CallsRepositoryMethod()
    {
        EventCollaborator eventCollaborator = new()
        {
            Id = 1,
            EventCollaboratorRole = EventCollaboratorRole.Organizer,
            ConfirmationStatus = ConfirmationStatus.Accept,
            EventDate = new DateOnly(2024, 6, 2),
            EventId = 1,
            ProposedDuration = null,
            User = new User(1, "Test", "Test@gmail.com", "Password")
        };

        _eventCollaboratorRepository.Add(eventCollaborator).Returns(1);

        int result = await _eventCollaboratorService.AddEventCollaborator(eventCollaborator);
        _eventCollaboratorRepository.Received().Add(eventCollaborator);

        Assert.Equivalent(result, 1);
    }

    [Fact]
    public async Task Should_ThrowException_When_EventCollaboratorIsNull()
    {
        EventCollaborator eventCollaborator = null;

        await Assert.ThrowsAsync<NullArgumentException>(async () =>
        {
            _eventCollaboratorRepository.Add(eventCollaborator).Returns(1);

            await _eventCollaboratorService.AddEventCollaborator(eventCollaborator);

            await _eventCollaboratorRepository.DidNotReceive().Add(eventCollaborator);
        });

    }
}
