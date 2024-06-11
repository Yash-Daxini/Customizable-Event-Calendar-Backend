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
        Event eventObj = new()
        {
            Id = 1,
            Title = "Title",
            Description = "Description",
            Location = "Location",
            Duration = new(1, 2),
            DateWiseEventCollaborators = [],
            RecurrencePattern = new()
            {
                Frequency = Core.Entities.Enums.Frequency.None,
                StartDate = new(2024, 5, 1),
                EndDate = new(2024, 5, 1)
            }
        };

        //_eventService.GetEventById(2, 2).Returns(eventObj);

        await _eventCollaboratorService.DeleteEventCollaboratorsByEventId(2, 2);

        await _eventCollaboratorRepository.Received().DeleteEventCollaboratorsByEventId(2);
    }

    //[Fact]
    //public async Task Should_ThrowException_When_EventIdNotPresent()
    //{
    //    //_eventService.GetEventById(2, 2).ReturnsNull();

    //    await Assert.ThrowsAsync<NotFoundException>(async () => await _eventCollaboratorService.DeleteEventCollaboratorsByEventId(1, 1));

    //    await _eventCollaboratorRepository.DidNotReceive().DeleteEventCollaboratorsByEventId(1);
    //}

    [Fact]
    public async Task Should_ThrowException_When_EventIdNotValid()
    {
        //_eventService.GetEventById(-1, 2).ReturnsNull();

        await Assert.ThrowsAsync<ArgumentException>(async () => await _eventCollaboratorService.DeleteEventCollaboratorsByEventId(-1, 1));

        await _eventCollaboratorRepository.DidNotReceive().DeleteEventCollaboratorsByEventId(1);
    }
}
