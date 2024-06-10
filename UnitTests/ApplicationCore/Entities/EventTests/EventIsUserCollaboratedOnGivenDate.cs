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
                    EventDate = new DateOnly(2024, 5, 30),
                    EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 30),
                            User = new User(48,"a","a@gmail.com","a"),
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 30),
                            User = new User(49,"b","b@gmail.com","b"),
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
                            User = new User(48,"a","a@gmail.com","a"),
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User(49,"b","b@gmail.com","b"),
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
    public void Should_ReturnsFalse_When_DateIsNotPresentInEvent(int year, int month, int day)
    {
        DateOnly date = new(year, month, day);

        bool result = _event.IsUserCollaboratedOnGivenDate(48, date);

        Assert.False(result);
    }

    [Theory]
    [InlineData(2024, 5, 29, 50)]
    [InlineData(2024, 10, 1, 51)]
    public void Should_ReturnsFalse_When_DateAndUserAreNotPresentInEvent(int year, int month, int day, int userId)
    {
        DateOnly date = new (year, month, day);

        bool result = _event.IsUserCollaboratedOnGivenDate(userId, date);

        Assert.False(result);
    }

    [Theory]
    [InlineData(2024, 5, 30, 48)]
    [InlineData(2024, 5, 31, 48)]
    public void Should_ReturnsTrue_When_DateAndUserArePresentInEvent(int year, int month, int day, int userId)
    {
        DateOnly date = new (year, month, day);

        bool result = _event.IsUserCollaboratedOnGivenDate(userId, date);

        Assert.True(result);
    }
    
    [Theory]
    [InlineData(2024, 5, 30, 48)]
    [InlineData(2024, 5, 31, 48)]
    public void Should_ReturnsFalse_When_DateWiseEventCollaboratorsIsNull(int year, int month, int day, int userId)
    {
        DateOnly date = new DateOnly(year, month, day);

        _event.DateWiseEventCollaborators = null;

        bool result = _event.IsUserCollaboratedOnGivenDate(userId, date);

        Assert.False(result);
    }

    [Theory]
    [InlineData(2024, 5, 30, 48)]
    [InlineData(2024, 5, 31, 48)]
    public void Should_ReturnsFalse_When_DateWiseEventCollaboratorsIsEmpty(int year, int month, int day, int userId)
    {
        DateOnly date = new DateOnly(year, month, day);

        _event.DateWiseEventCollaborators = [];

        bool result = _event.IsUserCollaboratedOnGivenDate(userId, date);

        Assert.False(result);
    }
}
