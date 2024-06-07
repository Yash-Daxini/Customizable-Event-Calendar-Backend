using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.DataModels;
using Infrastructure.Repositories;
using NSubstitute;

namespace UnitTests.Infrastructure.Repositories.EventRepositoryTests;

public class DeleteEvent
{
    private DbContextEventCalendar _dbContextEvent;
    private readonly IMapper _mapper;

    public DeleteEvent()
    {
        _mapper = Substitute.For<IMapper>();
    }

    [Fact]
    public async Task Should_DeleteEvent_When_EventWithIdAvailable()
    {
        _dbContextEvent = await new EventRepositoryDBContext().GetDatabaseContext();

        EventRepository eventRepository = new(_dbContextEvent, _mapper);

        Event? eventObj = await eventRepository.GetEventById(1);

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

        _dbContextEvent.ChangeTracker.Clear();

        _mapper.Map<EventDataModel>(eventObj).ReturnsForAnyArgs(eventDataModel);

        await eventRepository.Delete(eventObj);

        Event? deletedEvent = await eventRepository.GetEventById(1);

        Assert.Null(deletedEvent);
    }

}
