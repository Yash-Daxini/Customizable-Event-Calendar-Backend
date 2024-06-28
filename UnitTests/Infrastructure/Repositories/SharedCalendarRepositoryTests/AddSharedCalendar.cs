using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using FluentAssertions;
using Infrastructure.DataModels;
using UnitTests.Builders.DataModelBuilder;

namespace UnitTests.Infrastructure.Repositories.SharedCalendarRepositoryTests;

public class AddSharedCalendar : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;

    public AddSharedCalendar(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_Return_AddedSharedCalendarId_When_SharedCalendarIsValid()
    {
        UserDataModel user1 = new UserDataModelBuilder()
                              .WithUserName("a")
                              .WithEmail("a@gmail.com")
                              .Build();

        UserDataModel user2 = new UserDataModelBuilder()
                              .WithUserName("b")
                              .WithEmail("b@gmail.com")
                              .Build();

        _dbContext = new DatabaseBuilder()
            .WithUser(user1)
            .WithUser(user2)
            .Build();

        SharedCalendar sharedCalendar = new(
            0,
            new User { Id = 1, Name = "a", Email = "a@gmail.com" },
            new User { Id = 2, Name = "b", Email = "b@gmail.com" },
            new DateOnly(2024, 6, 7),
            new DateOnly(2024, 6, 7));

        SharedCalendarRepository sharedCalendarRepository = new(_dbContext, _mapper);

        int id = await sharedCalendarRepository.Add(sharedCalendar);

        sharedCalendar.Id = id;

        SharedCalendar? addedSharedCalendar = await sharedCalendarRepository.GetSharedCalendarById(id);

        addedSharedCalendar.Should().BeEquivalentTo(sharedCalendar);
    }
}
