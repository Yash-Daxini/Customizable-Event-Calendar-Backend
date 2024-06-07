using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.DataModels;
using Infrastructure.Profiles;
using Infrastructure.Repositories;
using NSubstitute;

namespace UnitTests.Infrastructure.Repositories.EventRepositoryTests;

public class GetSharedEvents
{
    private DbContextEventCalendar _dbContextEvent;
    private readonly IMapper _mapper;
    private readonly List<Event> _events;

    public GetSharedEvents()
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
    public async Task Should_ReturnListOfEvents_When_SharedCalendarAvailableWithGivenId()
    {
        //Arrange
        _dbContextEvent = await new EventRepositoryDBContext().GetDatabaseContext();

        EventDataModel eventDataModel = _dbContextEvent.Events.First(eventObj => eventObj.Id == 1);

        eventDataModel.EventCollaborators = [_dbContextEvent.EventCollaborators.First(eventCollaborator => eventCollaborator.Id == 1)];

        List<EventDataModel> eventDataModels = [eventDataModel];

        _events.RemoveAt(1);

        _mapper.Map<List<Event>>(eventDataModels).ReturnsForAnyArgs(_events);

        SharedCalendar sharedCalendar = new()
        {
            Id = 1,
            Sender = new()
            {
                Id = 1,
                Name = "a",
                Email = "a",
                Password = "a",
            },
            Receiver = new()
            {
                Id = 2,
                Name = "b",
                Email = "b",
                Password = "b",
            },
            FromDate = new DateOnly(2024, 6, 7),
            ToDate = new DateOnly(2024, 6, 7)
        };

        _mapper.Map<SharedCalendarDataModel>(sharedCalendar).ReturnsForAnyArgs(_dbContextEvent.SharedCalendars.First());

        EventRepository eventRepository = new(_dbContextEvent, _mapper);

        //Act
        List<Event> actualResult = await eventRepository.GetSharedEvents(sharedCalendar);
        
        //Assert
        Assert.Equivalent(_events, actualResult);
    }
}
