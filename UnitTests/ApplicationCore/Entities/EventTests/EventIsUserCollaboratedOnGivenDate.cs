using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventIsUserCollaboratedOnGivenDate
{
    private readonly Event _event;
    public EventIsUserCollaboratedOnGivenDate()
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
                    EventDate = new DateOnly(2024, 5, 30),
                    EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 30),
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
                            EventDate = new DateOnly(2024, 5, 30),
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
                }
            ]
        };
    }

    [Theory]
    [InlineData(2024, 5, 29)]
    [InlineData(2024, 10, 1)]
    public void IsUserCollaboratedOnGivenDateReturnsFalseIfDateIsNotPresentInEvent(int year, int month, int day)
    {
        DateOnly date = new DateOnly(year, month, day);

        bool result = _event.IsUserCollaboratedOnGivenDate(48, date);

        Assert.False(result);
    }

    [Theory]
    [InlineData(2024, 5, 29, 50)]
    [InlineData(2024, 10, 1, 51)]
    public void IsUserCollaboratedOnGivenDateReturnsFalseIfDateAndUserAreNotPresentInEvent(int year, int month, int day, int userId)
    {
        DateOnly date = new DateOnly(year, month, day);

        bool result = _event.IsUserCollaboratedOnGivenDate(userId, date);

        Assert.False(result);
    }

    [Theory]
    [InlineData(2024, 5, 30, 48)]
    [InlineData(2024, 5, 31, 48)]
    public void IsUserCollaboratedOnGivenDateReturnsTrueIfDateAndUserArePresentInEvent(int year, int month, int day, int userId)
    {
        DateOnly date = new DateOnly(year, month, day);

        bool result = _event.IsUserCollaboratedOnGivenDate(userId, date);

        Assert.True(result);
    }
}
