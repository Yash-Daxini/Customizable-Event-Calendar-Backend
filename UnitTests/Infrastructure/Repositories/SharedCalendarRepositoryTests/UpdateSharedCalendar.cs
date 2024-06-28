using AutoMapper;
using Core.Entities;
using FluentAssertions;
using Infrastructure;
using Infrastructure.DataModels;
using Infrastructure.Repositories;
using UnitTests.Builders.DataModelBuilder;

namespace UnitTests.Infrastructure.Repositories.SharedCalendarRepositoryTests;

public class UpdateSharedCalendar : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;
    public UpdateSharedCalendar(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_UpdateSharedCalendar_When_SharedCalendarAvailableWithId()
    {
        _dbContext = await new SharedCalendarRepositoryDBContext().GetDatabaseContext();

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

        await new DatabaseBuilder(_dbContext)
            .WithUser(user1)
            .WithUser(user2)
            .WithSharedCalendar(sharedCalendarDataModel)
            .Build();

        SharedCalendar sharedCalendar = new(
            1,
            new User { Id = 1, Name = "a", Email = "a@gmail.com" },
            new User { Id = 2, Name = "b", Email = "b@gmail.com" },
            new DateOnly(),
            new DateOnly());

        SharedCalendarRepository sharedCalendarRepository = new(_dbContext, _mapper);

        await sharedCalendarRepository.Update(sharedCalendar);

        SharedCalendar? updatedSharedCalendar = await sharedCalendarRepository.GetSharedCalendarById(1);

        updatedSharedCalendar.Should().BeEquivalentTo(sharedCalendar);
    }
}
