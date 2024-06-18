using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Core.Entities.RecurrecePattern;

namespace UnitTests.Infrastructure.Repositories.EventRepositoryTests;

public class AddEvent : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContextEvent;
    private readonly IMapper _mapper;

    public AddEvent(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_AddEventAndReturnEventId_When_EventOccurSingleTime()
    {
        _dbContextEvent = await new EventRepositoryDBContext().GetDatabaseContext();

        EventRepository eventRepository = new(_dbContextEvent, _mapper);

        Event eventToAdd = new()
        {
            Title = "Test2",
            Description = "Test2",
            Location = "Test2",
            Duration = new Duration(3, 4),
            RecurrencePattern = new SingleInstanceRecurrencePattern()
            {
                StartDate = new DateOnly(2024, 6, 8),
                EndDate = new DateOnly(2024, 6, 8),
                Frequency = Core.Entities.Enums.Frequency.None,
                Interval = 1,
                ByWeekDay = null
            },
            DateWiseEventCollaborators =
            [
                    new()
                    {
                        EventDate = new DateOnly(2024, 6, 8),
                        EventCollaborators = [
                            new()
                            {
                                EventDate = new DateOnly(2024, 6, 8),
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                                ProposedDuration = null,
                                User = new()
                                {
                                    Id = 3,
                                    Name = "c",
                                    Email = "c",
                                    Password = "c",
                                }
                            }, new()
                            {
                                EventDate = new DateOnly(2024, 6, 8),
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
                                ProposedDuration = null,
                                User = new()
                                {
                                    Id = 2,
                                    Name = "b",
                                    Email = "b",
                                    Password = "b",
                                }
                            }
                            ]
                    }
            ]
        };

        int eventId = await eventRepository.Add(eventToAdd);

        eventToAdd.Id = eventId;

        Assert.True(eventId > 0);
    }
    
    [Fact]
    public async Task Should_AddEventAndReturnEventId_When_EventIsDailyRecurring()
    {
        _dbContextEvent = await new EventRepositoryDBContext().GetDatabaseContext();

        EventRepository eventRepository = new(_dbContextEvent, _mapper);

        Event eventToAdd = new()
        {
            Title = "Test2",
            Description = "Test2",
            Location = "Test2",
            Duration = new Duration(3, 4),
            RecurrencePattern = new DailyRecurrencePattern()
            {
                StartDate = new DateOnly(2024, 6, 8),
                EndDate = new DateOnly(2024, 6, 8),
                Frequency = Core.Entities.Enums.Frequency.Daily,
                Interval = 1,
                ByWeekDay = null,
            },
            DateWiseEventCollaborators =
            [
                    new()
                    {
                        EventDate = new DateOnly(2024, 6, 8),
                        EventCollaborators = [
                            new()
                            {
                                EventDate = new DateOnly(2024, 6, 8),
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                                ProposedDuration = null,
                                User = new()
                                {
                                    Id = 3,
                                    Name = "c",
                                    Email = "c",
                                    Password = "c",
                                }
                            }, new()
                            {
                                EventDate = new DateOnly(2024, 6, 8),
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
                                ProposedDuration = null,
                                User = new()
                                {
                                    Id = 2,
                                    Name = "b",
                                    Email = "b",
                                    Password = "b",
                                }
                            }
                            ]
                    }
            ]
        };

        int eventId = await eventRepository.Add(eventToAdd);

        eventToAdd.Id = eventId;

        Assert.True(eventId > 0);
    }

    [Fact]
    public async Task Should_AddEventAndReturnEventId_When_EventIsWeeklyRecurring()
    {
        _dbContextEvent = await new EventRepositoryDBContext().GetDatabaseContext();

        EventRepository eventRepository = new(_dbContextEvent, _mapper);

        Event eventToAdd = new()
        {
            Title = "Test2",
            Description = "Test2",
            Location = "Test2",
            Duration = new Duration(3, 4),
            RecurrencePattern = new WeeklyRecurrencePattern()
            {
                StartDate = new DateOnly(2024, 6, 8),
                EndDate = new DateOnly(2024, 6, 8),
                Frequency = Core.Entities.Enums.Frequency.Weekly,
                Interval = 1,
                ByWeekDay = [1,2],
            },
            DateWiseEventCollaborators =
            [
                    new()
                    {
                        EventDate = new DateOnly(2024, 6, 8),
                        EventCollaborators = [
                            new()
                            {
                                EventDate = new DateOnly(2024, 6, 8),
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                                ProposedDuration = null,
                                User = new()
                                {
                                    Id = 3,
                                    Name = "c",
                                    Email = "c",
                                    Password = "c",
                                }
                            }, new()
                            {
                                EventDate = new DateOnly(2024, 6, 8),
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
                                ProposedDuration = null,
                                User = new()
                                {
                                    Id = 2,
                                    Name = "b",
                                    Email = "b",
                                    Password = "b",
                                }
                            }
                            ]
                    }
            ]
        };

        int eventId = await eventRepository.Add(eventToAdd);

        eventToAdd.Id = eventId;

        Assert.True(eventId > 0);
    }

    [Fact]
    public async Task Should_AddEventAndReturnEventId_When_EventIsMonthlyRecurring()
    {
        _dbContextEvent = await new EventRepositoryDBContext().GetDatabaseContext();

        EventRepository eventRepository = new(_dbContextEvent, _mapper);

        Event eventToAdd = new()
        {
            Title = "Test2",
            Description = "Test2",
            Location = "Test2",
            Duration = new Duration(3, 4),
            RecurrencePattern = new MonthlyRecurrencePattern()
            {
                StartDate = new DateOnly(2024, 6, 8),
                EndDate = new DateOnly(2024, 6, 8),
                Frequency = Core.Entities.Enums.Frequency.Monthly,
                Interval = 1,
                ByWeekDay = null,
                ByMonthDay = 31,
                WeekOrder = null,
            },
            DateWiseEventCollaborators =
            [
                    new()
                    {
                        EventDate = new DateOnly(2024, 6, 8),
                        EventCollaborators = [
                            new()
                            {
                                EventDate = new DateOnly(2024, 6, 8),
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                                ProposedDuration = null,
                                User = new()
                                {
                                    Id = 3,
                                    Name = "c",
                                    Email = "c",
                                    Password = "c",
                                }
                            }, new()
                            {
                                EventDate = new DateOnly(2024, 6, 8),
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
                                ProposedDuration = null,
                                User = new()
                                {
                                    Id = 2,
                                    Name = "b",
                                    Email = "b",
                                    Password = "b",
                                }
                            }
                            ]
                    }
            ]
        };

        int eventId = await eventRepository.Add(eventToAdd);

        eventToAdd.Id = eventId;

        Assert.True(eventId > 0);
    }

    [Fact]
    public async Task Should_AddEventAndReturnEventId_When_EventIsYearlyRecurring()
    {
        _dbContextEvent = await new EventRepositoryDBContext().GetDatabaseContext();

        EventRepository eventRepository = new(_dbContextEvent, _mapper);

        Event eventToAdd = new()
        {
            Title = "Test2",
            Description = "Test2",
            Location = "Test2",
            Duration = new Duration(3, 4),
            RecurrencePattern = new YearlyRecurrencePattern()
            {
                StartDate = new DateOnly(2024, 6, 8),
                EndDate = new DateOnly(2024, 6, 8),
                Frequency = Core.Entities.Enums.Frequency.Yearly,
                Interval = 1,
                ByWeekDay = null,
                ByMonthDay = 31,
                WeekOrder = null,
                ByMonth = 12
            },
            DateWiseEventCollaborators =
            [
                    new()
                    {
                        EventDate = new DateOnly(2024, 6, 8),
                        EventCollaborators = [
                            new()
                            {
                                EventDate = new DateOnly(2024, 6, 8),
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                                ProposedDuration = null,
                                User = new()
                                {
                                    Id = 3,
                                    Name = "c",
                                    Email = "c",
                                    Password = "c",
                                }
                            }, new()
                            {
                                EventDate = new DateOnly(2024, 6, 8),
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
                                ProposedDuration = null,
                                User = new()
                                {
                                    Id = 2,
                                    Name = "b",
                                    Email = "b",
                                    Password = "b",
                                }
                            }
                            ]
                    }
            ]
        };

        int eventId = await eventRepository.Add(eventToAdd);

        eventToAdd.Id = eventId;

        Assert.True(eventId > 0);
    }

}
