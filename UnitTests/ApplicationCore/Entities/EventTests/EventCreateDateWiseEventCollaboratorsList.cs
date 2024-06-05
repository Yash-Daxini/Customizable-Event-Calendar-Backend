using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventCreateDateWiseEventCollaboratorsList
{
    private readonly Event _event;
    public EventCreateDateWiseEventCollaboratorsList()
    {
        _event = _event = new()
        {
            Id = 2205,
            Title = "event",
            Location = "event",
            Description = "event",
            Duration = new Duration(1, 2),
            RecurrencePattern = new RecurrencePattern()
            {
                StartDate = new DateOnly(),
                EndDate = new DateOnly(),
                Frequency = Core.Entities.Enums.Frequency.Weekly,
                Interval = 2,
                ByWeekDay = [2, 6],
                WeekOrder = null,
                ByMonthDay = null,
                ByMonth = null
            },
            DateWiseEventCollaborators = [
                new EventCollaboratorsByDate
                {
                    EventDate = new DateOnly(2024, 5, 31),
                    EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
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
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
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
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
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
    }

    [Fact]
    public void Should_CreateDateWiseEventCollaboratorsListFromOccurrences_When_CallsTheMethod()
    {
        List<DateOnly> occurrences = [new DateOnly(2024, 5, 31),
                                      new DateOnly(2024, 6, 1)];

        Event eventObj = new()
        {
            Id = 2205,
            Title = "event",
            Location = "event",
            Description = "event",
            Duration = new Duration(1, 2),
            RecurrencePattern = new RecurrencePattern()
            {
                StartDate = new DateOnly(),
                EndDate = new DateOnly(),
                Frequency = Core.Entities.Enums.Frequency.Weekly,
                Interval = 2,
                ByWeekDay = [2, 6],
                WeekOrder = null,
                ByMonthDay = null,
                ByMonth = null
            },
            DateWiseEventCollaborators = [
            new EventCollaboratorsByDate
            {
                EventDate = new DateOnly(),
                EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(),
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
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
                            ProposedDuration = null,
                            EventDate = new DateOnly(),
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
            }]
        };

        eventObj.CreateDateWiseEventCollaboratorsList(occurrences);

        Assert.Equivalent(eventObj.DateWiseEventCollaborators, _event.DateWiseEventCollaborators);
    }

    [Fact]
    public void Should_BeEmptyList_When_DateWiseEventCollaboratosNotContainsParticipants()
    {
        List<DateOnly> occurrences = [new DateOnly(2024, 5, 31),
                                      new DateOnly(2024, 6, 1)];

        Event eventObj = new()
        {
            Id = 2205,
            Title = "event",
            Location = "event",
            Description = "event",
            Duration = new Duration(1, 2),
            RecurrencePattern = new RecurrencePattern()
            {
                StartDate = new DateOnly(),
                EndDate = new DateOnly(),
                Frequency = Core.Entities.Enums.Frequency.Weekly,
                Interval = 2,
                ByWeekDay = [2, 6],
                WeekOrder = null,
                ByMonthDay = null,
                ByMonth = null
            },
            DateWiseEventCollaborators = []
        };

        eventObj.CreateDateWiseEventCollaboratorsList(occurrences);

        Assert.Equivalent(new List<EventCollaboratorsByDate>(), eventObj.DateWiseEventCollaborators);
    }

    [Fact]
    public void Should_BeEmptyList_When_OccurrencesListIsEmpty()
    {
        List<DateOnly> occurrences = [];

        Event eventObj = new()
        {
            Id = 2205,
            Title = "event",
            Location = "event",
            Description = "event",
            Duration = new Duration(1, 2),
            RecurrencePattern = new RecurrencePattern()
            {
                StartDate = new DateOnly(),
                EndDate = new DateOnly(),
                Frequency = Core.Entities.Enums.Frequency.Weekly,
                Interval = 2,
                ByWeekDay = [2, 6],
                WeekOrder = null,
                ByMonthDay = null,
                ByMonth = null
            },
            DateWiseEventCollaborators = []
        };

        eventObj.CreateDateWiseEventCollaboratorsList(occurrences);

        Assert.Equivalent(new List<EventCollaboratorsByDate>(), eventObj.DateWiseEventCollaborators);
    }
}
