using Core.Entities;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventHasPendingResponseFromUser
{
    private readonly Event _event;

    public EventHasPendingResponseFromUser()
    {
        _event = new()
        {
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            User = new User
                            {
                                Id = 48
                            }
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Proposed,
                            User = new User
                            {
                                Id = 49,
                            },
                        }
            ]
        };
    }

    [Theory]
    [InlineData(50)]
    [InlineData(51)]
    public void Should_ReturnsFalse_When_UserIsNotAvailableAsEventCollaborator(int userId)
    {
        bool result = _event.HasPendingResponseFromUser(userId);

        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(48)]
    [InlineData(49)]
    public void Should_ReturnsFalse_When_UserHasNotPendingResponse(int userId)
    {
        bool result = _event.HasPendingResponseFromUser(userId);

        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(50)]
    public void Should_ReturnsTrue_When_UserHasPendingResponse(int userId)
    {
        _event.EventCollaborators.Add(
        new EventCollaborator
        {
            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
            User = new User
            {
                Id = 50,
            },
        }
        );

        bool result = _event.HasPendingResponseFromUser(userId);

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(50)]
    [InlineData(51)]
    public void Should_ReturnsTrue_When_MultipleUserHasPendingResponse(int userId)
    {
        _event.EventCollaborators.Add(
        new EventCollaborator
        {
            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
            User = new User
            {
                Id = 50,
            },
        }
            );

        _event.EventCollaborators.Add(
        new EventCollaborator
        {
            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
            User = new User
            {
                Id = 51,
            },
        }
            );

        bool result = _event.HasPendingResponseFromUser(userId);

        result.Should().BeTrue();
    }

}
