using Core.Entities;
using Core.Entities.Enums;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using Core.Exceptions;
using FluentAssertions;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Services.EventCollaboratorServiceTests;

public class AddEventCollaborator
{
    private readonly IEventCollaboratorRepository _eventCollaboratorRepository;
    private readonly IEventCollaboratorService _eventCollaboratorService;

    public AddEventCollaborator()
    {
        _eventCollaboratorRepository = Substitute.For<IEventCollaboratorRepository>();
        _eventCollaboratorService = new EventCollaboratorService(_eventCollaboratorRepository);
    }

    [Fact]
    public async Task Should_ReturnsAddedEventCollaboratorId_When_CallsRepositoryMethod()
    {
        User user = new UserBuilder()
                    .WithId(1)
                    .WithName("Test")
                    .WithEmail("Test@gmail.com")
                    .WithPassword("Password")
                    .Build();

        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithId(1)
                                              .WithEventCollaboratorRole(EventCollaboratorRole.Organizer)
                                              .WithConfirmationStatus(ConfirmationStatus.Accept)
                                              .WithEventDate(new DateOnly(2024, 6, 2))
                                              .WithEventId(1)
                                              .WithUser(user)
                                              .Build();

        _eventCollaboratorRepository.Add(eventCollaborator).Returns(1);

        int result = await _eventCollaboratorService.AddEventCollaborator(eventCollaborator);

        await _eventCollaboratorRepository.Received().Add(eventCollaborator);

        result.Should().Be(1);
    }

    [Fact]
    public async Task Should_ThrowException_When_EventCollaboratorIsNull()
    {
        EventCollaborator eventCollaborator = null;

        Action action = () =>
        {
            _eventCollaboratorRepository.Add(eventCollaborator).Returns(1);

            _eventCollaboratorService.AddEventCollaborator(eventCollaborator);
        };

        action.Should().Throw<NullArgumentException>();

        await _eventCollaboratorRepository.DidNotReceive().Add(eventCollaborator);
    }
}
