using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.DataModels;
using Infrastructure.Repositories;
using NSubstitute;

namespace UnitTests.Infrastructure.Repositories.EventRepositoryTests;

public class GetEventById
{
    private DbContextEventCalendar _dbContextEvent;
    private readonly IMapper _mapper;
    private readonly List<Event> _events;

    public GetEventById()
    {
        _mapper = Substitute.For<IMapper>();
        _events = [
            new() {
                Id = 1,
                Title = "Test",
                Description = "Test",
                Location = "Test",
                Duration = new Duration(1,2),
                RecurrencePattern = new (){
                    StartDate = new DateOnly(2024, 6, 7),
                    EndDate = new DateOnly(2024, 6, 7),
                    Frequency = Core.Entities.Enums.Frequency.None,
                    Interval = 1,
                    ByMonth = null,
                    ByMonthDay = null,
                    WeekOrder = null,
                },
                DateWiseEventCollaborators = [
                    new (){
                        EventDate = new DateOnly(2024, 6, 7),
                        EventCollaborators = [
                            new (){
                                Id = 1,
                                EventDate = new DateOnly(2024, 6, 7),
                                EventId = 1,
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                                ProposedDuration = null,
                                User = new(){
                                    Id = 1,
                                    Name = "a",
                                    Email = "a",
                                    Password = "a",
                                }
                            }
                            ]
                    }
                    ]
        },new() {
                Id = 2,
                Title = "Test1",
                Description = "Test1",
                Location = "Test1",
                Duration = new Duration(2,3),
                RecurrencePattern = new (){
                    StartDate = new DateOnly(2024, 6, 7),
                    EndDate = new DateOnly(2024, 6, 7),
                    Frequency = Core.Entities.Enums.Frequency.Daily,
                    Interval = 1,
                    ByMonth = null,
                    ByMonthDay = null,
                    WeekOrder = null,
                },
                DateWiseEventCollaborators = [
                    new (){
                        EventDate = new DateOnly(2024, 6, 7),
                        EventCollaborators = [
                            new (){
                                Id = 2,
                                EventDate = new DateOnly(2024, 6, 7),
                                EventId = 2,
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                                ProposedDuration = null,
                                User = new(){
                                    Id = 2,
                                    Name = "b",
                                    Email = "b",
                                    Password = "b",
                                }
                            },
                            new (){
                                Id = 3,
                                EventDate = new DateOnly(2024, 6, 7),
                                EventId = 2,
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
                                ProposedDuration = null,
                                User = new(){
                                    Id = 3,
                                    Name = "c",
                                    Email = "c",
                                    Password = "c",
                                }
                            }
                            ]
                    }
                    ]
        }
            ];
    }

    [Fact]
    public async Task Should_ReturnEvent_When_EventAvailableWithGivenId()
    {
        _dbContextEvent = await new EventRepositoryDBContext().GetDatabaseContext();

        EventRepository eventRepository = new(_dbContextEvent, _mapper);

        EventDataModel eventDataModel = new()
        {
            Id = 1,
            Title = "Test",
            Description = "Test",
            Location = "Test",
            UserId = 1,
            EventStartHour = 1,
            EventEndHour = 2,
            EventStartDate = new DateOnly(2024, 6, 7),
            EventEndDate = new DateOnly(2024, 6, 7),
            Frequency = "None",
            Interval = 1,
            ByMonth = null,
            ByMonthDay = null,
            ByWeekDay = null,
            EventCollaborators = [new (){
                                EventDate = new DateOnly(2024, 6, 8),
                                ParticipantRole = "Organizer",
                                ConfirmationStatus = "Accept",
                                ProposedStartHour = null,
                                ProposedEndHour = null,
                                UserId = 1
                            }
                            ]
        };

        _mapper.Map<Event>(eventDataModel).ReturnsForAnyArgs(_events[0]);

        Event? actualResult = await eventRepository.GetEventById(1);

        Assert.Equivalent(_events[0], actualResult);
    }

    [Fact]
    public async Task Should_ReturnNull_When_EventNotAvailableWithGivenId()
    {
        _dbContextEvent = await new EventRepositoryDBContext().GetDatabaseContext();

        EventRepository eventRepository = new(_dbContextEvent, _mapper);

        Event? actualResult = await eventRepository.GetEventById(3);

        Assert.Null(actualResult);
    }
}
