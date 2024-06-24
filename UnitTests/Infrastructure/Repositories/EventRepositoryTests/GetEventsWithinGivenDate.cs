using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Core.Entities.RecurrecePattern;
using FluentAssertions;
using UnitTests.Builders;

namespace UnitTests.Infrastructure.Repositories.EventRepositoryTests;

public class GetEventsWithinGivenDate : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContextEvent;
    private readonly IMapper _mapper;

    public GetEventsWithinGivenDate(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Theory]
    [InlineData("6-6-24", "10-6-24", 1)]
    public async Task Should_ReturnListOfEvents_When_EventOccurWithInGivenDate(string startDate, string endDate, int userId)
    {
        _dbContextEvent = await new EventRepositoryDBContext().GetDatabaseContext();

        DateOnly start = DateOnly.Parse(startDate);
        DateOnly end = DateOnly.Parse(endDate);

        SingleInstanceRecurrencePattern singleInstanceRecurrencePattern = new SingleInstanceRecurrencePatternBuilder()
                                                                          .WithStartDate(new DateOnly(2024, 6, 7))
                                                                          .WithEndDate(new DateOnly(2024, 6, 7))
                                                                          .WithInterval(1)
                                                                          .Build();

        User user = new UserBuilder()
                    .WithId(1)
                    .WithName("a")
                    .WithEmail("a")
                    .Build();

        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(1)
                                                     .WithOrganizer(user, new DateOnly(2024, 6, 7))
                                                     .Build();

        List<Event> expectedResult = [new EventBuilder()
                                      .WithId(1)
                                      .WithTitle("Test")
                                      .WithDescription("Test")
                                      .WithLocation("Test")
                                      .WithDuration(new Duration(1,2))
                                      .WithRecurrencePattern(singleInstanceRecurrencePattern)
                                      .WithEventCollaborators(eventCollaborators)
                                      .Build()];


        EventRepository eventRepository = new(_dbContextEvent, _mapper);

        List<Event> actualResult = await eventRepository.GetEventsWithinGivenDateByUserId(userId, start, end);

        actualResult.Should().BeEquivalentTo(expectedResult, option => option.For(e => e.EventCollaborators).Exclude(e => e.Id));
    }
}
