using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.Profiles;
using Infrastructure.Repositories;

namespace UnitTests.Infrastructure.Repositories.EventRepositoryTests;

public class AddEvent
{
    private DbContextEventCalendar _dbContextEvent;
    private readonly IMapper _mapper;

    public AddEvent()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new EventProfile());
            mc.AddProfile(new UserProfile());
            mc.AddProfile(new EventCollaboratorProfile());
            mc.AddProfile(new UserProfile());
        });
        IMapper mapper = mappingConfig.CreateMapper();
        _mapper = mapper;
    }

    [Fact]
    public async Task Should_AddEventAndReturnEventId_When_CallsTheRepositoryMethod()
    {
        _dbContextEvent = await new EventRepositoryDBContext().GetDatabaseContext();

        EventRepository eventRepository = new(_dbContextEvent, _mapper);

        Event eventToAdd = new Event()
        {
            Title = "Test2",
            Description = "Test2",
            Location = "Test2",
            Duration = new Duration(3, 4),
            RecurrencePattern = new()
            {
                StartDate = new DateOnly(2024, 6, 8),
                EndDate = new DateOnly(2024, 6, 8),
                Frequency = Core.Entities.Enums.Frequency.None,
                Interval = 1,
                ByMonth = null,
                ByMonthDay = null,
                WeekOrder = null,
            },
            DateWiseEventCollaborators = 
            [
                    new (){
                        EventDate = new DateOnly(2024, 6, 8),
                        EventCollaborators = [
                            new (){
                                EventDate = new DateOnly(2024, 6, 8),
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                                ProposedDuration = null,
                                User = new(){
                                    Id = 3,
                                    Name = "c",
                                    Email = "c",
                                    Password = "c",
                                }
                            },new (){
                                EventDate = new DateOnly(2024, 6, 8),
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
                                ProposedDuration = null,
                                User = new(){
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
