using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories;

namespace UnitTests.Infrastructure.Repositories.EventRepositoryTests;

public class GetSharedEvents : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContextEvent;
    private readonly IMapper _mapper;
    private readonly List<Event> _events;

    public GetSharedEvents(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _events = [
            new() {
                Id = 1,
                Title = "Test",
                Description = "Test",
                Location = "Test",
                Duration = new Duration(1,2),
                RecurrencePattern = new SingleInstanceRecurrencePattern(){
                    StartDate = new DateOnly(2024, 6, 7),
                    EndDate = new DateOnly(2024, 6, 7),
                    Frequency = Core.Entities.Enums.Frequency.None,
                    Interval = 1
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
                RecurrencePattern = new DailyRecurrencePattern(){
                    StartDate = new DateOnly(2024, 6, 7),
                    EndDate = new DateOnly(2024, 6, 7),
                    Frequency = Core.Entities.Enums.Frequency.Daily,
                    Interval = 1
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

        _events.RemoveAt(1);

        SharedCalendar sharedCalendar = new(
            1,
            new User
            {
                Id = 1,
                Name = "a",
                Email = "a@gmail.com",
                Password = "a"
            },
            new User
            {
                Id = 2,
                Name = "b",
                Email = "b@gmail.com",
                Password = "b"
            },
            new DateOnly(2024, 6, 7),
            new DateOnly(2024, 6, 7));

        EventRepository eventRepository = new(_dbContextEvent, _mapper);

        //Act
        List<Event> actualResult = await eventRepository.GetSharedEvents(sharedCalendar);

        //Assert
        Assert.Equivalent(_events, actualResult);
    }
}
