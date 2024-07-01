using AutoMapper;
using Core.Entities;
using FluentAssertions;
using Infrastructure;
using Infrastructure.DataModels;
using Infrastructure.Repositories;
using UnitTests.Builders.DataModelBuilder;

namespace UnitTests.Infrastructure.Repositories.SharedCalendarRepositoryTests;

public class GetAllSharedCalendars : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;
    private readonly List<SharedCalendar> _expectedResult;

    public GetAllSharedCalendars(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _expectedResult = [new(
            1,
            new User
            {
                Id = 1,
                Name = "a",
                Email = "a@gmail.com"
            },
            new User
            {
                Id = 2,
                Name = "b",
                Email = "b@gmail.com"
            },
            new DateOnly(),
            new DateOnly())
            ];
    }

    [Fact]
    public async Task Should_Return_AllSharedCalendar_When_SharedCalendarIsAvailable()
    {
        UserDataModel user1 = new UserDataModelBuilder()
                              .WithUserName("a")
                              .WithEmail("a@gmail.com")
                              .Build();

        UserDataModel user2 = new UserDataModelBuilder()
                              .WithUserName("b")
                              .WithEmail("b@gmail.com")
                              .Build();

        SharedCalendarDataModel sharedCalendarDataModel = new SharedCalendarDataModelBuilder()
                                                          .WithSenderId(1)
                                                          .WithReceiverId(2)
                                                          .WithFromDate(new DateOnly())
                                                          .WithToDate(new DateOnly())
                                                          .Build();

        _dbContext = new DatabaseBuilder()
            .WithUser(user1)
            .WithUser(user2)
            .WithSharedCalendar(sharedCalendarDataModel)
            .Build();


        SharedCalendarRepository sharedCalendarRepository = new(_dbContext, _mapper);

        List<SharedCalendar> sharedCalendars = await sharedCalendarRepository.GetAllSharedCalendars();

        sharedCalendars.Should().BeEquivalentTo(_expectedResult);
    }
}
