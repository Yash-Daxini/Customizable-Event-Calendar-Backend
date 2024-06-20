using Core.Entities;
using Core.Entities.Enums;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventIsOverlappingWith
{
    private readonly Event _event;

    public EventIsOverlappingWith()
    {
        _event = new()
        {
            Duration = new Duration(1, 2),
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Organizer,
                            ConfirmationStatus = ConfirmationStatus.Accept,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 48,
                            },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Participant,
                            ConfirmationStatus = ConfirmationStatus.Accept,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 49,
                            },
                        }
            ]
        };
    }

    [Fact]
    public void Should_ReturnsFalse_When_EventOccurOnDifferentDates()
    {
        Event eventToCheckOverlap = new()
        {
            Duration = new Duration(1, 4),
            EventCollaborators = [
                   new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Organizer,
                            ConfirmationStatus = ConfirmationStatus.Accept,
                            EventDate = new DateOnly(),
                            User = new User
                            {
                                Id = 48,
                            },
                        }
            ]
        };

        bool result = _event.IsEventOverlappingWith(eventToCheckOverlap, null);

        result.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnsFalse_When_EventOccurOnDifferentDuration()
    {
        Event eventToCheckOverlap = new()
        {
            Duration = new Duration(2, 3),
            EventCollaborators = [
                     new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Organizer,
                            ConfirmationStatus = ConfirmationStatus.Accept,
                            EventDate = new DateOnly(),
                            User = new User
                            {
                                Id = 48,
                            },
                        }
            ]
        };

        bool result = _event.IsEventOverlappingWith(eventToCheckOverlap, new DateOnly());

        result.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnsTrue_When_EventOccurOnSameDuration()
    {
        Event eventToCheckOverlap = new()
        {
            Duration = new Duration(1, 2),
            EventCollaborators = [
                     new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Organizer,
                            ConfirmationStatus = ConfirmationStatus.Accept,
                            EventDate = new DateOnly(),
                            User = new User
                            {
                                Id = 48,
                            },
                }
            ]
        };

        bool result = _event.IsEventOverlappingWith(eventToCheckOverlap, new DateOnly(2024, 5, 31));

        result.Should().BeTrue();
    }

    [Fact]
    public void Should_ReturnsTrue_When_EventOccurWithOverlapDuration()
    {
        Event eventToCheckOverlap = new()
        {
            Duration = new Duration(1, 4),
            EventCollaborators = [
                     new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Organizer,
                            ConfirmationStatus = ConfirmationStatus.Accept,
                            EventDate = new DateOnly(),
                            User = new User
                            {
                                Id = 48,
                            },
                }
            ]
        };

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
