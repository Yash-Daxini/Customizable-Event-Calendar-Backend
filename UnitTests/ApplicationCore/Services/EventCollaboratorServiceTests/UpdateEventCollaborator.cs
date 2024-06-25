using Core.Entities;
using Core.Entities.Enums;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Services.EventCollaboratorServiceTests;

public class UpdateEventCollaborator
{
    private readonly IEventCollaboratorRepository _eventCollaboratorRepository;
    private readonly IEventCollaboratorService _eventCollaboratorService;

    public UpdateEventCollaborator()
    {
        _eventCollaboratorRepository = Substitute.For<IEventCollaboratorRepository>();
        _eventCollaboratorService = new EventCollaboratorService(_eventCollaboratorRepository);
    }

    [Fact]
    public async Task Should_UpdateEventCollaborator_When_CallsRepositoryMethod()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithId(1)
                                              .WithEventCollaboratorRole(EventCollaboratorRole.Organizer)
                                              .WithConfirmationStatus(ConfirmationStatus.Accept)
                                              .WithEventDate(new DateOnly(2024,6,2))
                                              .WithEventId(1)
                                              .WithProposedDuration(null)
                                              .WithUser(new UserBuilder(1).Build())
                                              .Build();

        _eventCollaboratorRepository.GetEventCollaboratorById(1).Returns(eventCollaborator);

        await _eventCollaboratorService.UpdateEventCollaborator(eventCollaborator);

        await _eventCollaboratorRepository.Received().Update(eventCollaborator);
    }

    [Fact]
    public async Task Should_ThrowException_When_EventCollaboratorIdIsNotPresent()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithId(1)
                                              .WithEventCollaboratorRole(EventCollaboratorRole.Organizer)
                                              .WithConfirmationStatus(ConfirmationStatus.Accept)
                                              .WithEventDate(new DateOnly(2024, 6, 2))
                                              .WithEventId(1)
                                              .WithProposedDuration(null)
                                              .WithUser(new UserBuilder(1).Build())
                                              .Build();

        _eventCollaboratorRepository.GetEventCollaboratorById(1).ReturnsNull();

        var action = async () => await _eventCollaboratorService.UpdateEventCollaborator(eventCollaborator);

        await action.Should().ThrowAsync<NotFoundException>();

        await _eventCollaboratorRepository.DidNotReceive().Update(eventCollaborator);
    }

    [Fact]
    public async Task Should_ThrowException_When_EventCollaboratorIsNull()
    {
        EventCollaborator eventCollaborator = null;

        _eventCollaboratorRepository.GetEventCollaboratorById(1).ReturnsNull();

        var action = async () => await _eventCollaboratorService.UpdateEventCollaborator(eventCollaborator);

        await action.Should().ThrowAsync<NullArgumentException>();

        await _eventCollaboratorRepository.DidNotReceive().Update(eventCollaborator);
    }
}
