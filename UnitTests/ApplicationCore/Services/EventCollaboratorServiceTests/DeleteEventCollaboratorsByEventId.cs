using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace UnitTests.ApplicationCore.Services.EventCollaboratorServiceTests;

public class DeleteEventCollaboratorsByEventId
{
    private readonly IEventCollaboratorRepository _eventCollaboratorRepository;
    private readonly IEventCollaboratorService _eventCollaboratorService;

    public DeleteEventCollaboratorsByEventId()
    {
        _eventCollaboratorRepository = Substitute.For<IEventCollaboratorRepository>();
        _eventCollaboratorService = new EventCollaboratorService(_eventCollaboratorRepository);
    }

    [Fact]
    public async Task Should_DeleteEventCollaboratorsByEventId_When_CallsRepositoryMethod()
    {
        await _eventCollaboratorService.DeleteEventCollaboratorsByEventId(2, 2);

        await _eventCollaboratorRepository.Received().DeleteEventCollaboratorsByEventId(2);
    }

    [Fact]
    public async Task Should_ThrowException_When_EventIdNotValid()
    {
        await Assert.ThrowsAsync<ArgumentException>(async () => await _eventCollaboratorService.DeleteEventCollaboratorsByEventId(-1, 1));

        await _eventCollaboratorRepository.DidNotReceive().DeleteEventCollaboratorsByEventId(1);
    }
}
