using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Core.Entities.RecurrecePattern;
using FluentAssertions;
using UnitTests.Builders;

namespace UnitTests.Infrastructure.Repositories.EventRepositoryTests;

public class GetAllEventsByUserId : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContextEvent;
    private readonly IMapper _mapper;

    public GetAllEventsByUserId(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Theory]
    [InlineData(1)]
    public async Task Should_ReturnListEvents_When_EventAvailableWithGivenUserId(int userId)
    {
        //Arrange

        _dbContextEvent = await new EventRepositoryDBContext().GetDatabaseContext();

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

        //Act
        List<Event> actualResult = await eventRepository.GetAllEventsByUserId(userId);

        //Assert
        actualResult.Should().BeEquivalentTo(expectedResult, options => options.For(e => e.EventCollaborators).Exclude(e => e.Id));
    }
}
