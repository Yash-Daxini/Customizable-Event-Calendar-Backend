using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventGetEventOrganizer
{

    private readonly Event _event;

    public EventGetEventOrganizer()
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
                }
            ]
        };
    }

    [Fact]
    public void GetOrganizerOfEventIfMultipleEventCollaboratorsPresent()
    {
        User actualUser = new()
        {
            Id = 48,
            Name = "a",
            Email = "a@gmail.com",
            Password = "a"
        };

        User? expectedUser = _event.GetEventOrganizer();

        Assert.Equivalent(expectedUser, actualUser);
    }

    [Fact]
    public void GetOrganizerOfEventIfOnlyOrganizerPresent()
    {
        _event.DateWiseEventCollaborators = [
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
                        }
                    ]
                }
            ];

        User actualUser = new()
        {
            Id = 48,
            Name = "a",
            Email = "a@gmail.com",
            Password = "a"
        };

        User? expectedUser = _event.GetEventOrganizer();

        Assert.Equivalent(expectedUser, actualUser);
    }
    
    [Fact]
    public void GetOrganizerOfEventAsNullIfDateWiseEventCollaboratorsIsNull()
    {
        _event.DateWiseEventCollaborators = null;

        User? expectedUser = _event.GetEventOrganizer();

        Assert.Equivalent(expectedUser, null);
    }
    
    [Fact]
    public void GetOrganizerOfEventAsNullIfDateWiseEventCollaboratorsIsEmpty()
    {
        _event.DateWiseEventCollaborators = [];

        User? expectedUser = _event.GetEventOrganizer();

        Assert.Equivalent(expectedUser, null);
    }
}