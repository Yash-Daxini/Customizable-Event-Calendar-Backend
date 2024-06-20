using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Core.Entities.RecurrecePattern;
using FluentAssertions;

namespace UnitTests.Infrastructure.Repositories.EventRepositoryTests;

public class GetEventById : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContextEvent;
    private readonly IMapper _mapper;

    public GetEventById(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_ReturnEvent_When_EventAvailableWithGivenId()
    {
        _dbContextEvent = await new EventRepositoryDBContext().GetDatabaseContext();

        EventRepository eventRepository = new(_dbContextEvent, _mapper);

        Event expectedResult = new()
        {
            Id = 1,
            Title = "Test",
            Description = "Test",
            Location = "Test",
            Duration = new Duration(1, 2),
            RecurrencePattern = new SingleInstanceRecurrencePattern()
            {
                StartDate = new DateOnly(2024, 6, 7),
                EndDate = new DateOnly(2024, 6, 7),
                Frequency = Core.Entities.Enums.Frequency.None,
                Interval = 1,
                ByWeekDay = []
            },
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
                                    Email = "a"
                                }
                            }
                            ]
        };

        Event? actualResult = await eventRepository.GetEventById(1);

        actualResult.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Should_ReturnNull_When_EventNotAvailableWithGivenId()
    {
        _dbContextEvent = await new EventRepositoryDBContext().GetDatabaseContext();

        EventRepository eventRepository = new(_dbContextEvent, _mapper);

        Event? actualResult = await eventRepository.GetEventById(10);

        Assert.Null(actualResult);
    }
}
