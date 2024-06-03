using Core.Entities;
using Core.Entities.Enums;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;

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
            User = new User()
            {
                Id = 1,
                Name = "Test",
                Email = "Test@gmail.com",
                Password = "Password",
            }
        };

        _eventCollaboratorRepository.Add(eventCollaborator).Returns(1);

        int result = await _eventCollaboratorService.AddEventCollaborator(eventCollaborator);
        _eventCollaboratorRepository.Received().Add(eventCollaborator);

        Assert.Equivalent(result, 1);
    }
}
