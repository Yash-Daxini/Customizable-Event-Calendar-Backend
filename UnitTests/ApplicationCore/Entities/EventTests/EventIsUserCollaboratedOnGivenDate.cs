using Core.Entities;
using Core.Entities.Enums;
using FluentAssertions;
using UnitTests.Builders.EntityBuilder;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventIsUserCollaboratedOnGivenDate
{
    private readonly Event _event;
    public EventIsUserCollaboratedOnGivenDate()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                             .WithOrganizer(new UserBuilder(48).Build(), new DateOnly(2024, 5, 30))
                                             .WithParticipant(new UserBuilder(49).Build(),
                                                              ConfirmationStatus.Accept,
                                                              new DateOnly(2024, 5, 30),
                                                              null)
                                             .WithOrganizer(new UserBuilder(48).Build(), new DateOnly(2024, 5, 31))
                                             .WithParticipant(new UserBuilder(49).Build(),
                                                              ConfirmationStatus.Accept,
                                                              new DateOnly(2024, 5, 31),
                                                              null)
                                             .Build();

        _event = new EventBuilder()
                 .WithEventCollaborators(eventCollaborators)
                 .Build();
    }

    [Theory]
    [InlineData(2024, 5, 29)]
    [InlineData(2024, 10, 1)]
    public void Should_Return_False_When_DateIsNotPresentInEvent(int year, int month, int day)
    {
        DateOnly date = new(year, month, day);

        bool result = _event.IsUserCollaboratedOnGivenDate(48, date);

        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(2024, 5, 29, 50)]
    [InlineData(2024, 10, 1, 51)]
    public void Should_Return_False_When_DateAndUserIsNotPresentInEvent(int year, int month, int day, int userId)
    {
        DateOnly date = new(year, month, day);

        bool result = _event.IsUserCollaboratedOnGivenDate(userId, date);

        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(2024, 5, 30, 48)]
    [InlineData(2024, 5, 31, 48)]
    public void Should_Return_True_When_DateAndUserIsPresentInEvent(int year, int month, int day, int userId)
    {
        DateOnly date = new(year, month, day);

        bool result = _event.IsUserCollaboratedOnGivenDate(userId, date);

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(2024, 5, 30, 48)]
    [InlineData(2024, 5, 31, 48)]
    public void Should_Return_False_When_EventCollaboratorsIsNull(int year, int month, int day, int userId)
    {
        DateOnly date = new(year, month, day);

        _event.EventCollaborators = null;

        bool result = _event.IsUserCollaboratedOnGivenDate(userId, date);

        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(2024, 5, 30, 48)]
    [InlineData(2024, 5, 31, 48)]
    public void Should_Return_False_When_EventCollaboratorsIsEmpty(int year, int month, int day, int userId)
    {
        DateOnly date = new(year, month, day);

        _event.EventCollaborators = [];

        bool result = _event.IsUserCollaboratedOnGivenDate(userId, date);

        result.Should().BeFalse();
    }
}
