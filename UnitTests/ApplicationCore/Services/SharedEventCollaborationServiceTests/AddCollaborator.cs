using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;

namespace UnitTests.ApplicationCore.Services.SharedEventCollaborationServiceTests;

public class AddCollaborator
{

    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly IEventService _eventService;
    private readonly ISharedEventCollaborationService _sharedEventCollaborationService;

    public AddCollaborator()
    {
        _eventCollaboratorService = Substitute.For<IEventCollaboratorService>();
        _eventService = Substitute.For<IEventService>();
        _sharedEventCollaborationService = new SharedEventCollaborationService(_eventCollaboratorService,_eventService);
    }

    [Fact]
    public async Task Should_AddCollaborator_When_NotOverlapAndNotAlreadyCollaborated()
    {

    }

    [Fact]
    public async Task Should_ThrowException_When_CollaborationOverlap()
    {

    }

    [Fact]
    public async Task Should_ThrowException_When_AlreadyCollaborated()
    {

    }
}
