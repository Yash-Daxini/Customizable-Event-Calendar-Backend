using AutoMapper;
using Core.Entities;
using FluentAssertions;
using Infrastructure;
using Infrastructure.DataModels;
using Infrastructure.Repositories;
using UnitTests.Builders.DataModelBuilder;
using UnitTests.Builders.EntityBuilder;

namespace UnitTests.Infrastructure.Repositories.EventRepositoryTests;

public class DeleteEvent : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;

    public DeleteEvent(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_DeleteEvent_When_EventAvailableWithId()
    {
        UserDataModel userDataModel1 = new UserDataModelBuilder()
                                      .WithId(1)
                                      .WithUserName("a")
                                      .WithEmail("a@gmail.com")
                                      .Build();

        EventDataModel eventDataModel = new EventDataModelBuilder()
                                        .WithTitle("Test")
                                        .WithDescription("Test")
                                        .WithLocation("Test")
                                        .WithUserId(1)
                                        .WithStartHour(1)
                                        .WithEndHour(2)
                                        .WithStartDate(new DateOnly(2024, 6, 7))
                                        .WithEndDate(new DateOnly(2024, 6, 7))
                                        .WithFrequency("None")
                                        .WithInterval(1)
                                        .WithByMonth(null)
                                        .WithByMonthDay(null)
                                        .WithWeekOrder(null)
                                        .Build();

        _dbContext = new DatabaseBuilder()
                 .WithUser(userDataModel1)
                 .WithEvent(eventDataModel)
                 .Build();

        Event eventObj = new EventBuilder()
                         .WithId(1)
                         .WithTitle("Test")
                         .WithDescription("Test")
                         .WithLocation("Test")
                         .WithDuration(new Duration(1, 2))
                         .WithRecurrencePattern(new SingleInstanceRecurrencePatternBuilder()
                                                .WithStartDate(new DateOnly(2024, 6, 7))
                                                .WithEndDate(new DateOnly(2024, 6, 7))
                                                .WithInterval(1)
                                                .Build())
                         .Build();

        EventRepository eventRepository = new(_dbContext, _mapper);

        await eventRepository.Delete(eventObj);

        Event? deletedEvent = await eventRepository.GetEventById(1);

        deletedEvent.Should().BeNull();
    }

}
