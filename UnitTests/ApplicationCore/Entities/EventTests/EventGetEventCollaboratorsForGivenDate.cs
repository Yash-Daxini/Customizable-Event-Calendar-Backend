using Core.Entities;
using Core.Entities.Enums;
using FluentAssertions;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventGetEventCollaboratorsForGivenDate
{

    private readonly Event _event;

    public EventGetEventCollaboratorsForGivenDate()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                     .WithOrganizer(new UserBuilder(48).Build(),
                                                                    new DateOnly(2024, 5, 31))
                                                     .WithParticipant(new UserBuilder(49).Build(),
                                                                      ConfirmationStatus.Accept,
                                                                      new DateOnly(2024, 5, 31),
                                                                      null)
                                                     .WithOrganizer(new UserBuilder(48).Build(),
                                                                    new DateOnly(2024, 6, 2))
                                                    .Build();

        _event = new EventBuilder()
                 .WithEventCollaborators(eventCollaborators)
                 .Build();
    }

    [Theory]
    [InlineData(2024, 5, 30)]
    [InlineData(2024, 5, 29)]
    [InlineData(2024, 6, 3)]
    [InlineData(2024, 6, 1)]

    public void Should_Returns_EmptyList_When_EventCollaboratorNotOccurOnGivenDate(int year, int month, int day)
    {
        DateOnly date = new(year, month, day);

        List<EventCollaborator> eventCollaborators = _event.GetEventCollaboratorsForGivenDate(date);

        eventCollaborators.Should().BeEmpty();
    }

    [Theory]
    [InlineData(2024, 5, 31)]
    public void Should_Returns_EventCollaboratorList_When_EventCollaboratorOccurOnGivenDate(int year, int month, int day)
    {
        List<EventCollaborator> expectedResult = new EventCollaboratorListBuilder(47)
                                                     .WithOrganizer(new UserBuilder(48).Build(),
                                                                    new DateOnly(2024, 5, 31))
                                                     .WithParticipant(new UserBuilder(49).Build(),
                                                                      ConfirmationStatus.Accept,
                                                                      new DateOnly(2024, 5, 31),
                                                                      null)
                                                     .Build();

        DateOnly date = new(year, month, day);
        List<EventCollaborator> eventCollaborators = _event.GetEventCollaboratorsForGivenDate(date);

        eventCollaborators.Should().BeEquivalentTo(expectedResult);
    }

    [Theory]
    [InlineData(2024, 5, 31)]
    [InlineData(2024, 6, 2)]
    public void Should_Returns_EmptyList_When_EventCollaboratorsIsNull(int year, int month, int day)
    {
        _event.EventCollaborators = null;

        DateOnly date = new(year, month, day);
        List<EventCollaborator> eventCollaborators = _event.GetEventCollaboratorsForGivenDate(date);

        eventCollaborators.Should().BeEmpty();
    }

    [Theory]
    [InlineData(2024, 5, 31)]
    [InlineData(2024, 6, 2)]
    public void Should_Returns_EmptyList_When_EventCollaboratorsIsEmpty(int year, int month, int day)
    {
        _event.EventCollaborators = [];

        DateOnly date = new(year, month, day);
        List<EventCollaborator> eventCollaborators = _event.GetEventCollaboratorsForGivenDate(date);

        eventCollaborators.Should().BeEmpty();
    }
}
