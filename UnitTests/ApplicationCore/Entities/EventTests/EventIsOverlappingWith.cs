﻿using Core.Entities;
using Core.Entities.Enums;
using FluentAssertions;
using UnitTests.Builders.EntityBuilder;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventIsOverlappingWith
{
    private readonly Event _event;

    public EventIsOverlappingWith()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                     .WithOrganizer(new UserBuilder(48).Build(), new DateOnly(2024, 4, 1))
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
    public void Should_Return_False_When_EventOccurOnDifferentDates()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                     .WithOrganizer(new UserBuilder(48).Build(), new DateOnly(2024, 4, 2))
                                                     .Build();

        Event eventToCheckOverlap = new EventBuilder()
                                    .WithDuration(new Duration(1, 4))
                                    .WithEventCollaborators(eventCollaborators)
                                    .Build();

        bool result = _event.IsEventOverlappingWith(eventToCheckOverlap, null);

        result.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_EventOccurOnSameDateAndDifferentDuration()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                     .WithOrganizer(new UserBuilder(48).Build(), new DateOnly(2024, 4, 1))
                                                     .Build();

        Event eventToCheckOverlap = new EventBuilder()
                                    .WithDuration(new Duration(2, 3))
                                    .WithEventCollaborators(eventCollaborators)
                                    .Build();

        bool result = _event.IsEventOverlappingWith(eventToCheckOverlap, new DateOnly(2024, 4, 1));

        result.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_True_When_EventOccurOnSameDateAndSameDuration()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                     .WithOrganizer(new UserBuilder(48).Build(), new DateOnly(2024, 4, 1))
                                                     .Build();

        Event eventToCheckOverlap = new EventBuilder()
                                    .WithDuration(new Duration(1, 2))
                                    .WithEventCollaborators(eventCollaborators)
                                    .Build(); ;

        bool result = _event.IsEventOverlappingWith(eventToCheckOverlap, new DateOnly(2024, 4, 1));

        result.Should().BeTrue();
    }

    [Fact]
    public void Should_Return_True_When_EventOccurOnSameDateAndOverlapDuration()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                     .WithOrganizer(new UserBuilder(48).Build(), new DateOnly(2024, 4, 1))
                                                     .Build();

        Event eventToCheckOverlap = new EventBuilder()
                                    .WithDuration(new Duration(1, 4))
                                    .WithEventCollaborators(eventCollaborators)
                                    .Build();

        bool result = _event.IsEventOverlappingWith(eventToCheckOverlap, new DateOnly(2024, 4, 1));

        result.Should().BeTrue();
    }

    [Fact]
    public void Should_Return_False_When_EventIsNull()
    {
        bool result = _event.IsEventOverlappingWith(null, null);

        result.Should().BeFalse();
    }

}
