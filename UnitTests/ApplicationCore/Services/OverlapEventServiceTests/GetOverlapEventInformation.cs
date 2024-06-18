using System.Text;
using Core.Entities;
using Core.Entities.Enums;
using Core.Interfaces.IServices;
using Core.Services;
using Core.Entities.RecurrecePattern;

namespace UnitTests.ApplicationCore.Services.OverlapEventServiceTests;

public class GetOverlapEventInformation
{

    private readonly List<Event> _events;

    private readonly IOverlappingEventService _overlappingEventService;

    public GetOverlapEventInformation()
    {
        _events =
            [
                new(){
                    Id = 1,
                    Title = "1",
                    Description = "1",
                    Location = "1",
                    Duration = new (5,6),
                    RecurrencePattern = new DailyRecurrencePattern(){
                        StartDate = new DateOnly(2024,5,30),
                        EndDate = new DateOnly(2024,6,2),
                        Frequency = Frequency.Daily,
                        ByWeekDay = [1,2,3,4,5,6,7],
                        Interval = 1
                    },
                    DateWiseEventCollaborators =
                    [
                        new (){
                            EventDate = new DateOnly(2024,5,30),
                            EventCollaborators =
                            [
                                new()
                                {
                                    Id = 1,
                                    EventCollaboratorRole = EventCollaboratorRole.Organizer,
                                    ConfirmationStatus = ConfirmationStatus.Accept,
                                    EventDate = new DateOnly(2024, 6, 2),
                                    EventId = 1,
                                    ProposedDuration = null,
                                    User = new User()
                                    {
                                        Id = 1,
                                        Name = "Test",
                                        Email = "Test@gmail.com",
                                        Password = "Password",
                                    }
                                }
                            ]
                        },
                        new (){
                            EventDate = new DateOnly(2024,5,31),
                            EventCollaborators =
                            [
                                new()
                                {
                                    Id = 1,
                                    EventCollaboratorRole = EventCollaboratorRole.Organizer,
                                    ConfirmationStatus = ConfirmationStatus.Accept,
                                    EventDate = new DateOnly(2024, 6, 2),
                                    EventId = 1,
                                    ProposedDuration = null,
                                    User = new User()
                                    {
                                        Id = 1,
                                        Name = "Test",
                                        Email = "Test@gmail.com",
                                        Password = "Password",
                                    }
                                }
                            ]
                        },
                        new (){
                            EventDate = new DateOnly(2024,6,1),
                            EventCollaborators =
                            [
                                new()
                                {
                                    Id = 1,
                                    EventCollaboratorRole = EventCollaboratorRole.Organizer,
                                    ConfirmationStatus = ConfirmationStatus.Accept,
                                    EventDate = new DateOnly(2024, 6, 2),
                                    EventId = 1,
                                    ProposedDuration = null,
                                    User = new User()
                                    {
                                        Id = 1,
                                        Name = "Test",
                                        Email = "Test@gmail.com",
                                        Password = "Password",
                                    }
                                }
                            ]
                        },
                        new (){
                            EventDate = new DateOnly(2024,6,2),
                            EventCollaborators =
                            [
                                new()
                                {
                                    Id = 1,
                                    EventCollaboratorRole = EventCollaboratorRole.Organizer,
                                    ConfirmationStatus = ConfirmationStatus.Accept,
                                    EventDate = new DateOnly(2024, 6, 2),
                                    EventId = 1,
                                    ProposedDuration = null,
                                    User = new User()
                                    {
                                        Id = 1,
                                        Name = "Test",
                                        Email = "Test@gmail.com",
                                        Password = "Password",
                                    }
                                }
                            ]
                        }
                    ]
                },
                new()
        {
            Id = 2,
            Title = "2",
            Description = "2",
            Location = "2",
            Duration = new(6,7),
            RecurrencePattern = new DailyRecurrencePattern()
            {
                StartDate = new DateOnly(2024, 5, 30),
                EndDate = new DateOnly(2024, 6, 2),
                Frequency = Frequency.Daily,
                ByWeekDay = [1, 2, 3, 4, 5, 6, 7],
                Interval = 2
            },
            DateWiseEventCollaborators =
                    [
                        new (){
                            EventDate = new DateOnly(2024,5,2),
                            EventCollaborators =
                            [
                                new()
                                {
                                    Id = 1,
                                    EventCollaboratorRole = EventCollaboratorRole.Organizer,
                                    ConfirmationStatus = ConfirmationStatus.Accept,
                                    EventDate = new DateOnly(2024, 6, 2),
                                    EventId = 1,
                                    ProposedDuration = null,
                                    User = new User()
                                    {
                                        Id = 1,
                                        Name = "Test",
                                        Email = "Test@gmail.com",
                                        Password = "Password",
                                    }
                                }
                            ]
                        },
                        new (){
                            EventDate = new DateOnly(2024,6,2),
                            EventCollaborators =
                            [
                                new()
                                {
                                    Id = 1,
                                    EventCollaboratorRole = EventCollaboratorRole.Organizer,
                                    ConfirmationStatus = ConfirmationStatus.Accept,
                                    EventDate = new DateOnly(2024, 6, 2),
                                    EventId = 1,
                                    ProposedDuration = null,
                                    User = new User()
                                    {
                                        Id = 1,
                                        Name = "Test",
                                        Email = "Test@gmail.com",
                                        Password = "Password",
                                    }
                                }
                            ]
                        }
                    ]
        },
                new()
        {
            Id = 3,
            Title = "3",
            Description = "3",
            Location = "3",
            Duration = new(6,7),
            RecurrencePattern = new DailyRecurrencePattern()
            {
                StartDate = new DateOnly(2024, 6, 2),
                EndDate = new DateOnly(2024, 6, 2),
                Frequency = Frequency.None,
                ByWeekDay = null,
                Interval = 2
            },
            DateWiseEventCollaborators =
                    [
                        new (){
                            EventDate = new DateOnly(2024,6,2),
                            EventCollaborators =
                            [
                                new()
                                {
                                    Id = 1,
                                    EventCollaboratorRole = EventCollaboratorRole.Organizer,
                                    ConfirmationStatus = ConfirmationStatus.Accept,
                                    EventDate = new DateOnly(2024, 6, 2),
                                    EventId = 1,
                                    ProposedDuration = null,
                                    User = new User()
                                    {
                                        Id = 1,
                                        Name = "Test",
                                        Email = "Test@gmail.com",
                                        Password = "Password",
                                    }
                                }
                            ]
                        }
                    ]
        }

            ];
        _overlappingEventService = new OverlapEventService();
    }

    [Fact]
    public void Should_ReturnStringMessage_When_EventOverlap()
    {
        Event eventToCheckOverlap = new()
        {
            Id = 2,
            Title = "2",
            Description = "2",
            Location = "2",
            Duration = new(5, 6),
            RecurrencePattern = new DailyRecurrencePattern()
            {
                StartDate = new DateOnly(2024, 5, 30),
                EndDate = new DateOnly(2024, 6, 2),
                Frequency = Frequency.Daily,
                ByWeekDay = [1, 2, 3, 4, 5, 6, 7],
                Interval = 2
            },
            DateWiseEventCollaborators =
                    [
                        new (){
                            EventDate = new DateOnly(2024,5,30),
                            EventCollaborators =
                            [
                                new()
                                {
                                    Id = 1,
                                    EventCollaboratorRole = EventCollaboratorRole.Organizer,
                                    ConfirmationStatus = ConfirmationStatus.Accept,
                                    EventDate = new DateOnly(2024, 6, 2),
                                    EventId = 1,
                                    ProposedDuration = null,
                                    User = new User()
                                    {
                                        Id = 1,
                                        Name = "Test",
                                        Email = "Test@gmail.com",
                                        Password = "Password",
                                    }
                                }
                            ]
                        },
                        new (){
                            EventDate = new DateOnly(2024,6,2),
                            EventCollaborators =
                            [
                                new()
                                {
                                    Id = 1,
                                    EventCollaboratorRole = EventCollaboratorRole.Organizer,
                                    ConfirmationStatus = ConfirmationStatus.Accept,
                                    EventDate = new DateOnly(2024, 6, 2),
                                    EventId = 1,
                                    ProposedDuration = null,
                                    User = new User()
                                    {
                                        Id = 1,
                                        Name = "Test",
                                        Email = "Test@gmail.com",
                                        Password = "Password",
                                    }
                                }
                            ]
                        }
                    ]
        };

        StringBuilder expectedMessage = new($"2 overlaps with following events at {eventToCheckOverlap.Duration.GetDurationInFormat()} :-  ");

        expectedMessage.AppendLine($"Event Name : 1 , " +
                                   $"Date : {new DateOnly(2024, 5, 30)} , " +
                                   $"Duration : 5 AM - 6 AM");

        string? message = _overlappingEventService.GetOverlappedEventInformation(eventToCheckOverlap, _events);

        Assert.NotNull(message);

        Assert.Equal(expectedMessage.ToString(), message);
    }

    [Fact]
    public void Should_ReturnStringMessage_When_EventOverlapWithMultipleEvents()
    {
        Event eventToCheckOverlap = new()
        {
            Id = 4,
            Title = "4",
            Description = "4",
            Location = "4",
            Duration = new(6, 7),
            RecurrencePattern = new DailyRecurrencePattern()
            {
                StartDate = new DateOnly(2024, 5, 30),
                EndDate = new DateOnly(2024, 6, 2),
                Frequency = Frequency.Daily,
                ByWeekDay = [1, 2, 3, 4, 5, 6, 7],
                Interval = 2
            },
            DateWiseEventCollaborators =
                    [
                        new (){
                            EventDate = new DateOnly(2024,6,2),
                            EventCollaborators =
                            [
                                new()
                                {
                                    Id = 1,
                                    EventCollaboratorRole = EventCollaboratorRole.Organizer,
                                    ConfirmationStatus = ConfirmationStatus.Accept,
                                    EventDate = new DateOnly(2024, 6, 2),
                                    EventId = 1,
                                    ProposedDuration = null,
                                    User = new User()
                                    {
                                        Id = 1,
                                        Name = "Test",
                                        Email = "Test@gmail.com",
                                        Password = "Password",
                                    }
                                }
                            ]
                        },
                        new (){
                            EventDate = new DateOnly(2024,6,2),
                            EventCollaborators =
                            [
                                new()
                                {
                                    Id = 1,
                                    EventCollaboratorRole = EventCollaboratorRole.Organizer,
                                    ConfirmationStatus = ConfirmationStatus.Accept,
                                    EventDate = new DateOnly(2024, 6, 2),
                                    EventId = 1,
                                    ProposedDuration = null,
                                    User = new User()
                                    {
                                        Id = 1,
                                        Name = "Test",
                                        Email = "Test@gmail.com",
                                        Password = "Password",
                                    }
                                }
                            ]
                        }
                    ]
        };

        StringBuilder expectedMessage = new($"4 overlaps with following events at {eventToCheckOverlap.Duration.GetDurationInFormat()} :-  ");

        expectedMessage.AppendLine($"Event Name : 2 , " +
                                   $"Date : {new DateOnly(2024, 6, 2)} , " +
                                   $"Duration : 6 AM - 7 AM");

        expectedMessage.AppendLine($"Event Name : 3 , " +
                                   $"Date : {new DateOnly(2024, 6, 2)} , " +
                                   $"Duration : 6 AM - 7 AM");


        string? message = _overlappingEventService.GetOverlappedEventInformation(eventToCheckOverlap, _events);

        Assert.NotNull(message);

        Assert.Equal(expectedMessage.ToString(), message);
    }

    [Fact]
    public void Should_ReturnNull_When_NonOverlapEvent()
    {
        Event eventToCheckOverlap = new()
        {
            Id = 2,
            Title = "2",
            Description = "2",
            Location = "2",
            Duration = new(6, 7),
            RecurrencePattern = new DailyRecurrencePattern()
            {
                StartDate = new DateOnly(2024, 5, 30),
                EndDate = new DateOnly(2024, 6, 2),
                Frequency = Frequency.Daily,
                ByWeekDay = [1, 2, 3, 4, 5, 6, 7],
                Interval = 2
            },
            DateWiseEventCollaborators =
                    [
                        new (){
                            EventDate = new DateOnly(2024,5,30),
                            EventCollaborators =
                            [
                                new()
                                {
                                    Id = 1,
                                    EventCollaboratorRole = EventCollaboratorRole.Organizer,
                                    ConfirmationStatus = ConfirmationStatus.Accept,
                                    EventDate = new DateOnly(2024, 6, 2),
                                    EventId = 1,
                                    ProposedDuration = null,
                                    User = new User()
                                    {
                                        Id = 1,
                                        Name = "Test",
                                        Email = "Test@gmail.com",
                                        Password = "Password",
                                    }
                                }
                            ]
                        },
                    ]
        };

        string? message = _overlappingEventService.GetOverlappedEventInformation(eventToCheckOverlap, _events);

        Assert.Null(message);
    }

    [Fact]
    public void Should_ReturnNull_When_EventToCheckOverlapIsNull()
    {
        string? message = _overlappingEventService.GetOverlappedEventInformation(null, _events);

        Assert.Null(message);
    }

    [Fact]
    public void Should_ReturnNull_When_EventToCheckOverlapIsEmpty()
    {
        string? message = _overlappingEventService.GetOverlappedEventInformation(null, []);

        Assert.Null(message);
    }
}
