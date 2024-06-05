using Core.Entities;
using Core.Entities.Enums;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventIsProposedEvent
{

    private readonly Event _event;

    public EventIsProposedEvent()
    {
        _event = new()
        {
            Id = 2205,
            Title = "event",
            Location = "event",
            Description = "event",
            Duration = new Duration(1,2),
            RecurrencePattern = new RecurrencePattern()
            {
                StartDate = new DateOnly(),
                EndDate = new DateOnly(),
                Frequency = Frequency.Weekly,
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
                            EventCollaboratorRole = EventCollaboratorRole.Organizer,
                            ConfirmationStatus = ConfirmationStatus.Accept,
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
                            EventCollaboratorRole = EventCollaboratorRole.Participant,
                            ConfirmationStatus = ConfirmationStatus.Accept,
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
    public void Should_ReturnsFalse_When_NotAnyEventCollaboratorWithProposedOrPendingStatus()
    {
        bool result = _event.IsProposedEvent();
        Assert.False(result);
    }
    
    [Fact]
    public void Should_ReturnsTrue_When_AnyEventCollaboratorWithProposedStatus()
    {
        _event.DateWiseEventCollaborators[0].EventCollaborators.Add(
            new EventCollaborator
        {
            EventCollaboratorRole = EventCollaboratorRole.Participant,
            ConfirmationStatus = ConfirmationStatus.Proposed,
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
        }
        );
        bool result = _event.IsProposedEvent();
        Assert.True(result);
    }
    
    [Fact]
    public void Should_ReturnsTrue_When_AnyEventCollaboratorWithPendingStatus()
    {
        _event.DateWiseEventCollaborators[0].EventCollaborators.Add(
            new EventCollaborator
        {
            EventCollaboratorRole = EventCollaboratorRole.Participant,
            ConfirmationStatus = ConfirmationStatus.Pending,
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
        }
        );
        bool result = _event.IsProposedEvent();
        Assert.True(result);
    }
    
    [Fact]
    public void Should_ReturnsTrue_When_EventCollaboratorsWithPendingAndProposedStatus()
    {
        _event.DateWiseEventCollaborators[0].EventCollaborators.Add(
            new EventCollaborator
        {
            EventCollaboratorRole = EventCollaboratorRole.Participant,
            ConfirmationStatus = ConfirmationStatus.Pending,
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
        }
        );
        _event.DateWiseEventCollaborators[0].EventCollaborators.Add(
            new EventCollaborator
            {
                EventCollaboratorRole = EventCollaboratorRole.Participant,
                ConfirmationStatus = ConfirmationStatus.Proposed,
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
            }
        );
        bool result = _event.IsProposedEvent();
        Assert.True(result);
    }
}
