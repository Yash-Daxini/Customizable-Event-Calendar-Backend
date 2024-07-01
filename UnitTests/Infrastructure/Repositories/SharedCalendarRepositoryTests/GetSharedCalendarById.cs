using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using FluentAssertions;
using Infrastructure.DataModels;
using UnitTests.Builders.DataModelBuilder;

namespace UnitTests.Infrastructure.Repositories.SharedCalendarRepositoryTests;

public class GetSharedCalendarById : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;

    public GetSharedCalendarById(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_Return_SharedCalendar_When_SharedCalendarAvailableWithGivenId()
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

        SharedCalendar expectedResult = new(
            1,
            new User { Id = 1, Name = "a", Email = "a@gmail.com" },
            new User { Id = 2, Name = "b", Email = "b@gmail.com" },
            new DateOnly(),
            new DateOnly());

        SharedCalendarRepository sharedCalendarRepository = new(_dbContext, _mapper);

        SharedCalendar? sharedCalendarById = await sharedCalendarRepository.GetSharedCalendarById(1);

        sharedCalendarById.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Should_Return_Null_When_SharedCalendarNotAvailableWithGivenId()
    {
        _dbContext = new DatabaseBuilder().Build();

        SharedCalendarRepository sharedCalendarRepository = new(_dbContext, _mapper);

        SharedCalendar? sharedCalendarById = await sharedCalendarRepository.GetSharedCalendarById(2);

        sharedCalendarById.Should().BeNull();
    }
}
