using Core.Entities;
using Core.Entities.Enums;
using Core.Entities.RecurrecePattern;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventGetOverlapDate
{
    private readonly Event _event;

    public EventGetOverlapDate()
    {
        _event = new()
        {
            Id = 2205,
            Title = "event",
            Location = "event",
            Description = "event",
            Duration = new Duration(1, 2),
            RecurrencePattern = new WeeklyRecurrencePattern()
            {
                StartDate = new DateOnly(2024, 5, 31),
                EndDate = new DateOnly(2024, 8, 25),
                Frequency = Frequency.Weekly,
                Interval = 2,
                ByWeekDay = [2, 6]
            },
            DateWiseEventCollaborators = [
                new EventCollaboratorsByDate
                {
                    EventDate = new DateOnly(2024, 5, 31),
                    EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Organizer,
                            ConfirmationStatus = ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 48,
                                Name = "a",
                                Email = "a@gmail.com",
                                Password = "a"
                            },
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 49,
                                Name = "b",
                                Email = "b@gmail.com",
                                Password = "b"
                            },
                            EventId = 47
                        },
                    ]
                }
            ]
        };
    }

    [Fact]
    public void Should_ReturnsNull_When_OverlapNotOccur()
    {
        Event eventToCheckOverlap = new()
        {
            Id = 2205,
            Title = "event",
            Location = "event",
            Description = "event",
            Duration = new Duration(1, 2),
            RecurrencePattern = new WeeklyRecurrencePattern()
            {
                StartDate = new DateOnly(2024, 5, 31),
                EndDate = new DateOnly(2024, 8, 25),
                Frequency = Frequency.Weekly,
                Interval = 2,
                ByWeekDay = [2, 6]
            },
            DateWiseEventCollaborators = [
                new EventCollaboratorsByDate
                {
                    EventDate = new DateOnly(2024, 6, 1),
                    EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 6, 1),
                            User = new User
                            {
                                Id = 48,
                                Name = "a",
                                Email = "a@gmail.com",
                                Password = "a"
                            },
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 6, 1),
                            User = new User
                            {
                                Id = 49,
                                Name = "b",
                                Email = "b@gmail.com",
                                Password = "b"
                            },
                            EventId = 47
                        },
                    ]
                }
            ]
        };

        DateOnly? overlapDate = _event.GetOverlapDate(eventToCheckOverlap);

        Assert.Equivalent(overlapDate, null);
    }

    [Fact]
    public void Should_ReturnsOverlapDate_When_OverlapOccur()
    {
        Event eventToCheckOverlap = new()
        {
            Id = 2205,
            Title = "event",
            Location = "event",
            Description = "event",
            Duration = new Duration(1, 2),
            RecurrencePattern = new WeeklyRecurrencePattern()
            {
                StartDate = new DateOnly(2024, 5, 31),
                EndDate = new DateOnly(2024, 8, 25),
                Frequency = Frequency.Weekly,
                Interval = 2,
                ByWeekDay = [2, 6]
            },
            DateWiseEventCollaborators = [
                new EventCollaboratorsByDate
                {
                    EventDate = new DateOnly(2024, 5, 31),
                    EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Organizer,
                            ConfirmationStatus = ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 48,
                                Name = "a",
                                Email = "a@gmail.com",
                                Password = "a"
                            },
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Participant,
                            ConfirmationStatus = ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 49,
                                Name = "b",
                                Email = "b@gmail.com",
                                Password = "b"
                            },
                            EventId = 47
                        },
                    ]
                },
                new EventCollaboratorsByDate
                {
                    EventDate = new DateOnly(2024, 6, 1),
                    EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Organizer,
                            ConfirmationStatus = ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 6, 1),
                            User = new User
                            {
                                Id = 48,
                                Name = "a",
                                Email = "a@gmail.com",
                                Password = "a"
                            },
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = EventCollaboratorRole.Participant,
                            ConfirmationStatus = ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 6, 1),
                            User = new User
                            {
                                Id = 49,
                                Name = "b",
                                Email = "b@gmail.com",
                                Password = "b"
                            },
                            EventId = 47
                        },
                    ]
                }
            ]
        };

        DateOnly? overlapDate = _event.GetOverlapDate(eventToCheckOverlap);

        Assert.Equivalent(overlapDate, new DateOnly(2024, 5, 31));
    }

    [Fact]
    public void Should_ReturnsNull_When_PassedEventIsNull()
    {
        DateOnly? overlapDate = _event.GetOverlapDate(null);

        Assert.Equivalent(overlapDate, null);
    }
}