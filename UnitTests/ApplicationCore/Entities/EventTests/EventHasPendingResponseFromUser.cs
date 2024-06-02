using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventHasPendingResponseFromUser
{
    private readonly Event _event;

    public EventHasPendingResponseFromUser()
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
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Proposed,
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

    [Theory]
    [InlineData(50)]
    [InlineData(51)]
    public void HasPendingResponseFromUserReturnsFalseIfUserIsNotAvailableAsEventCollaborator(int userId)
    {
        bool result = _event.HasPendingResponseFromUser(userId);

        Assert.False(result);
    }

    [Theory]
    [InlineData(48)]
    [InlineData(49)]
    public void HasPendingResponseFromUserReturnsFalseIfUserHasNotPendingResponse(int userId)
    {
        bool result = _event.HasPendingResponseFromUser(userId);

        Assert.False(result);
    }

    [Theory]
    [InlineData(50)]
    public void HasPendingResponseFromUserReturnsTrueIfUserHasPendingResponse(int userId)
    {
        _event.DateWiseEventCollaborators[0].EventCollaborators.Add(
        new EventCollaborator
        {
            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
            ProposedDuration = null,
            EventDate = new DateOnly(),
            User = new User
            {
                Id = 50,
                Name = "a",
                Email = "a@gmail.com",
                Password = "a"
            },
            EventId = 47
        }
            );

        bool result = _event.HasPendingResponseFromUser(userId);

        Assert.False(result);
    }
    
    [Theory]
    [InlineData(50)]
    [InlineData(51)]
    public void HasPendingResponseFromUserReturnsTrueIfMultipleUserHasPendingResponse(int userId)
    {
        _event.DateWiseEventCollaborators[0].EventCollaborators.Add(
        new EventCollaborator
        {
            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
            ProposedDuration = null,
            EventDate = new DateOnly(),
            User = new User
            {
                Id = 50,
                Name = "a",
                Email = "a@gmail.com",
                Password = "a"
            },
            EventId = 47
        }
            );

        _event.DateWiseEventCollaborators[0].EventCollaborators.Add(
        new EventCollaborator
        {
            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
            ProposedDuration = null,
            EventDate = new DateOnly(),
            User = new User
            {
                Id = 51,
                Name = "a",
                Email = "a@gmail.com",
                Password = "a"
            },
            EventId = 47
        }
            );

        bool result = _event.HasPendingResponseFromUser(userId);

        Assert.False(result);
    }

}
