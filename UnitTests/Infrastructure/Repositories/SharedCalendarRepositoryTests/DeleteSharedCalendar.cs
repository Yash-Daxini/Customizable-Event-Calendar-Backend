using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using FluentAssertions;
using Infrastructure.DataModels;
using UnitTests.Builders.DataModelBuilder;

namespace UnitTests.Infrastructure.Repositories.SharedCalendarRepositoryTests;

public class DeleteSharedCalendar : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;
    public DeleteSharedCalendar(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_DeleteSharedCalendar_When_SharedCalendarAvailableWithId()
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

        SharedCalendar sharedCalendar = new(
            1,
            new User { Id = 1, Name = "a", Email = "a@gmail.com", Password = "a" },
            new User { Id = 2, Name = "b", Email = "b@gmail.com", Password = "b" },
            new DateOnly(2024, 6, 7),
            new DateOnly(2024, 6, 7));

        SharedCalendarRepository sharedCalendarRepository = new(_dbContext, _mapper);

        await sharedCalendarRepository.Delete(sharedCalendar);

        SharedCalendar? sharedCalendarFromGetById = await sharedCalendarRepository.GetSharedCalendarById(1);

        sharedCalendarFromGetById.Should().BeNull();
    }
}
