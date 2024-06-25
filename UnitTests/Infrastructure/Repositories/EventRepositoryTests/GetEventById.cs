using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Core.Entities.RecurrecePattern;
using FluentAssertions;
using UnitTests.Builders;

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

        SingleInstanceRecurrencePattern singleInstanceRecurrencePattern = new SingleInstanceRecurrencePatternBuilder()
                                                                          .WithStartDate(new DateOnly(2024, 6, 7))
                                                                          .WithEndDate(new DateOnly(2024, 6, 7))
                                                                          .WithInterval(1)
                                                                          .Build();

        User user = new UserBuilder(1)
                    .WithName("a")
                    .WithEmail("a")
                    .Build();

        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(1)
                                                     .WithOrganizer(user, new DateOnly(2024, 6, 7))
                                                     .Build();

        Event expectedResult = new EventBuilder()
                               .WithId(1)
                               .WithTitle("Test")
                               .WithDescription("Test")
                               .WithLocation("Test")
                               .WithDuration(new Duration(1, 2))
                               .WithRecurrencePattern(singleInstanceRecurrencePattern)
                               .WithEventCollaborators(eventCollaborators)
                               .Build();


        Event? actualResult = await eventRepository.GetEventById(1);

        actualResult.Should().BeEquivalentTo(expectedResult,option => option.For(e => e.EventCollaborators).Exclude(e => e.Id));
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
