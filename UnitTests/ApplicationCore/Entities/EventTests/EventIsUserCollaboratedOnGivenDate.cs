using Core.Entities;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventIsUserCollaboratedOnGivenDate
{
    private readonly Event _event;
    public EventIsUserCollaboratedOnGivenDate()
    {
        _event = new()
        {
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            EventDate = new DateOnly(2024, 5, 30),
                            User = new User
                            {
                                Id = 48,
                            },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
                            EventDate = new DateOnly(2024, 5, 30),
                            User = new User
                            {
                                Id = 49,
                            },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 48,
                            },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 49,
                            },
                        },
            ]
        };
    }

    [Theory]
    [InlineData(2024, 5, 29)]
    [InlineData(2024, 10, 1)]
    public void Should_ReturnsFalse_When_DateIsNotPresentInEvent(int year, int month, int day)
    {
        DateOnly date = new(year, month, day);

        bool result = _event.IsUserCollaboratedOnGivenDate(48, date);

        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(2024, 5, 29, 50)]
    [InlineData(2024, 10, 1, 51)]
    public void Should_ReturnsFalse_When_DateAndUserIsNotPresentInEvent(int year, int month, int day, int userId)
    {
        DateOnly date = new(year, month, day);

        bool result = _event.IsUserCollaboratedOnGivenDate(userId, date);

        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(2024, 5, 30, 48)]
    [InlineData(2024, 5, 31, 48)]
    public void Should_ReturnsTrue_When_DateAndUserIsPresentInEvent(int year, int month, int day, int userId)
    {
        DateOnly date = new(year, month, day);

        bool result = _event.IsUserCollaboratedOnGivenDate(userId, date);

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(2024, 5, 30, 48)]
    [InlineData(2024, 5, 31, 48)]
    public void Should_ReturnsFalse_When_EventCollaboratorsIsNull(int year, int month, int day, int userId)
    {
        DateOnly date = new(year, month, day);

        _event.EventCollaborators = null;

        bool result = _event.IsUserCollaboratedOnGivenDate(userId, date);

        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(2024, 5, 30, 48)]
    [InlineData(2024, 5, 31, 48)]
    public void Should_ReturnsFalse_When_EventCollaboratorsIsEmpty(int year, int month, int day, int userId)
    {
        DateOnly date = new(year, month, day);

        _event.EventCollaborators = [];

        bool result = _event.IsUserCollaboratedOnGivenDate(userId, date);

        result.Should().BeFalse();
    }
}
