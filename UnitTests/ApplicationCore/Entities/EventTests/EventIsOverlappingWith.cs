using Core.Entities;
using Core.Entities.Enums;
using FluentAssertions;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventIsOverlappingWith
{
    private readonly Event _event;

    public EventIsOverlappingWith()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                     .WithOrganizer(new UserBuilder(48).Build(), new DateOnly())
                                     .WithParticipant(new UserBuilder(49).Build(),
                                                      ConfirmationStatus.Proposed,
                                                      new DateOnly(),
                                                      null)
                                     .Build();

        _event = new EventBuilder()
                 .WithDuration(new Duration(1, 2))
                 .WithEventCollaborators(eventCollaborators)
                 .Build();
    }

    [Fact]
    public void Should_ReturnsFalse_When_EventOccurOnDifferentDates()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                     .WithOrganizer(new UserBuilder(48).Build(), new DateOnly())
                                                     .Build();

        Event eventToCheckOverlap = new EventBuilder()
                                    .WithDuration(new Duration(1, 4))
                                    .WithEventCollaborators(eventCollaborators)
                                    .Build();

        bool result = _event.IsEventOverlappingWith(eventToCheckOverlap, null);

        result.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnsFalse_When_EventOccurOnDifferentDuration()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                     .WithOrganizer(new UserBuilder(48).Build(), new DateOnly())
                                                     .Build();

        Event eventToCheckOverlap = new EventBuilder()
                                    .WithDuration(new Duration(2, 3))
                                    .WithEventCollaborators(eventCollaborators)
                                    .Build();

        bool result = _event.IsEventOverlappingWith(eventToCheckOverlap, new DateOnly());

        result.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnsTrue_When_EventOccurOnSameDuration()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                     .WithOrganizer(new UserBuilder(48).Build(), new DateOnly())
                                                     .Build();

        Event eventToCheckOverlap = new EventBuilder()
                                    .WithDuration(new Duration(1, 2))
                                    .WithEventCollaborators(eventCollaborators)
                                    .Build(); ;

        bool result = _event.IsEventOverlappingWith(eventToCheckOverlap, new DateOnly(2024, 5, 31));

        result.Should().BeTrue();
    }

    [Fact]
    public void Should_ReturnsTrue_When_EventOccurWithOverlapDuration()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                     .WithOrganizer(new UserBuilder(48).Build(), new DateOnly())
                                                     .Build();

        Event eventToCheckOverlap = new EventBuilder()
                                    .WithDuration(new Duration(1, 4))
                                    .WithEventCollaborators(eventCollaborators)
                                    .Build();

        bool result = _event.IsEventOverlappingWith(eventToCheckOverlap, new DateOnly(2024, 5, 31));

        result.Should().BeTrue();
    }

    [Fact]
    public void Should_ReturnFalse_When_MatchedDateAndEventAreNull()
    {
        bool result = _event.IsEventOverlappingWith(null, null);

        result.Should().BeFalse();
    }

}
