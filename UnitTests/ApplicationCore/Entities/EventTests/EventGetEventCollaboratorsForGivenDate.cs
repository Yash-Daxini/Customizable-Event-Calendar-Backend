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
                    EventDate = new DateOnly(2024, 6, 2),
                    EventCollaborators = [
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
    [InlineData(2024, 6, 3)]
    [InlineData(2024, 6, 1)]
    
    public void Should_ReturnsEmptyList_When_EventCollaboratorNotOccurOnGivenDate(int year, int month, int day)
    {
        DateOnly date = new(year, month, day);
        List<EventCollaborator> eventCollaborators = _event.GetEventCollaboratorsForGivenDate(date);

        Assert.Equivalent(new List<EventCollaborator>(), eventCollaborators);
    }

    [Theory]
    [InlineData(2024, 5, 31)]
    [InlineData(2024, 6, 2)]
    public void Should_ReturnsEventCollaboratorList_When_EventCollaboratorOccurOnGivenDate(int year, int month, int day)
    {
        DateOnly date = new(year, month, day);
        List<EventCollaborator> eventCollaborators = _event.GetEventCollaboratorsForGivenDate(date);

        List<EventCollaborator> expectedResult = [
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

        if (day == 2)
            Assert.Equivalent(expectedResult, eventCollaborators);
        else
            Assert.Equivalent(_eventCollaborators, eventCollaborators);
    }
}
