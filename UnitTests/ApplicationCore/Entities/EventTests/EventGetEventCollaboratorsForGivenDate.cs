using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventGetEventCollaboratorsForGivenDate
{

    private readonly Event _event;
    private readonly List<EventCollaborator> _eventCollaborators;

    public EventGetEventCollaboratorsForGivenDate()
    {
        _event = new()
        {
            Id = 2205,
            Title = "event",
            Location = "event",
            Description = "event",
            Duration = new Duration()
            {
                StartHour = 1,
                EndHour = 2
            },
            RecurrencePattern = new RecurrencePattern()
            {
                StartDate = new DateOnly(2024, 5, 31),
                EndDate = new DateOnly(2024, 8, 25),
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
                },
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
                },
                new EventCollaboratorsByDate
                {
                    EventDate = new DateOnly(2024, 5, 2),
                    EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 2),
                            User = new User
                            {
                                Id = 48,
                                Name = "a",
                                Email = "a@gmail.com",
                                Password = "a"
                            },
                            EventId = 47
                        }
                    ]
                }
            ]
        };
        _eventCollaborators = [
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
                        }
                    ];
    }

    [Theory]
    [InlineData(2024, 5, 30)]
    [InlineData(2024, 5, 29)]
    [InlineData(2024, 6, 2)]
    [InlineData(2024, 6, 3)]
    public void GetEventCollaboratorsForGivenDateReturnsEmptyListIfNotOccurOnGivenDate(int year, int month, int day)
    {
        DateOnly date = new(year, month, day);
        List<EventCollaborator> eventCollaborators = _event.GetEventCollaboratorsForGivenDate(date);

        Assert.Equivalent(eventCollaborators, new List<EventCollaborator>());
    }

    [Theory]
    [InlineData(2024, 5, 31)]
    [InlineData(2024, 6, 1)]
    public void GetEventCollaboratorsForGivenDateReturnsEventCollaboratorListIfOccurOnGivenDate(int year, int month, int day)
    {
        DateOnly date = new(year, month, day);
        List<EventCollaborator> eventCollaborators = _event.GetEventCollaboratorsForGivenDate(date);

        Assert.Equivalent(eventCollaborators, _eventCollaborators);
    }

    [Theory]
    [InlineData(2024, 6, 2)]
    public void GetEventCollaboratorsForGivenDateReturnsDifferentEventCollaboratorListIfOccurOnGivenDate(int year, int month, int day)
    {
        DateOnly date = new(year, month, day);
        List<EventCollaborator> eventCollaborators = _event.GetEventCollaboratorsForGivenDate(date);

        List<EventCollaborator> actualResult = [
            new EventCollaborator
            {
                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                ProposedDuration = null,
                EventDate = new DateOnly(2024, 6, 2),
                User = new User
                {
                    Id = 48,
                    Name = "a",
                    Email = "a@gmail.com",
                    Password = "a"
                },
                EventId = 47
            }
        ];

        Assert.Equivalent(eventCollaborators, actualResult);
    }
}
