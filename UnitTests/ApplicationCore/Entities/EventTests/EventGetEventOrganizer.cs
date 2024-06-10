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
                            User = new User(48,"a","a@gmail.com","a"),
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
                            ProposedDuration = null,
                            EventDate = new DateOnly(),
                            User = new User(49,"b","b@gmail.com","b"),
                            EventId = 47
                        },
                    ]
                }
            ]
        };
    }

    [Fact]
    public void Should_ReturnOrganizerOfEvent_When_MultipleEventCollaboratorsPresent()
    {
        User actualUser = new User(48, "a", "a@gmail.com", "a");

        User? expectedUser = _event.GetEventOrganizer();

        Assert.Equivalent(expectedUser, actualUser);
    }

    [Fact]
    public void Should_ReturnOrganizerOfEvent_When_OnlyOrganizerPresent()
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
                            User = new User(48,"a","a@gmail.com","a"),
                            EventId = 47
                        }
                    ]
                }
            ];

        User actualUser = new User(48, "a", "a@gmail.com", "a");

        User? expectedUser = _event.GetEventOrganizer();

        Assert.Equivalent(expectedUser, actualUser);
    }

    [Fact]
    public void Should_ReturnOrganizerOfEventAsNull_When_DateWiseEventCollaboratorsIsNull()
    {
        _event.DateWiseEventCollaborators = null;

        User? expectedUser = _event.GetEventOrganizer();

        Assert.Equivalent(expectedUser, null);
    }

    [Fact]
    public void Should_ReturnOrganizerOfEventAsNull_When_EventCollaboratorsIsNull()
    {
        _event.DateWiseEventCollaborators[0].EventCollaborators = null;

        User? expectedUser = _event.GetEventOrganizer();

        Assert.Equivalent(expectedUser, null);
    }

    [Fact]
    public void Should_ReturnOrganizerOfEventAsNull_When_DateWiseEventCollaboratorsIsEmpty()
    {
        _event.DateWiseEventCollaborators = [];

        User? expectedUser = _event.GetEventOrganizer();

        Assert.Equivalent(expectedUser, null);
    }

    [Fact]
    public void Should_ReturnOrganizerOfEventAsNull_When_EventCollaboratorsIsEmpty()
    {
        _event.DateWiseEventCollaborators[0].EventCollaborators = [];

        User? expectedUser = _event.GetEventOrganizer();

        Assert.Equivalent(expectedUser, null);
    }
}