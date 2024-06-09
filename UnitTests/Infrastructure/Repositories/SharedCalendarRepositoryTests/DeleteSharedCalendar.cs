using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;

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
        _dbContext = await new SharedCalendarRepositoryDBContext().GetDatabaseContext();

        _dbContext.ChangeTracker.Clear();

        SharedCalendar sharedCalendar = new()
        {
            Id = 1,
            Sender = new()
            {
                Id = 1,
                Name = "a",
                Email = "a",
                Password = "a",
            },
            Receiver = new()
            {
                Id = 2,
                Name = "b",
                Email = "b",
                Password = "b",
            },
            FromDate = new DateOnly(2024, 6, 7),
            ToDate = new DateOnly(2024, 6, 7)
        };

        SharedCalendarRepository sharedCalendarRepository = new(_dbContext, _mapper);

        await sharedCalendarRepository.Delete(sharedCalendar);

        SharedCalendar? updatedSharedCalendar = await sharedCalendarRepository.GetSharedCalendarById(1);

        Assert.Null(updatedSharedCalendar);
    }
}
