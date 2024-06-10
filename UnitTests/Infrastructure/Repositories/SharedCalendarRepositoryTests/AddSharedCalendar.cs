using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;

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
    public async Task Should_AddSharedCalendarAndReturnSharedCalendarId_When_CallsTheRepositoryMethod()
    {
        _dbContext = await new SharedCalendarRepositoryDBContext().GetDatabaseContext();

        SharedCalendar sharedCalendar = new(
            0,
            new(1, "a", "a", "a"),
            new(2, "b", "b", "b"),
            new DateOnly(2024, 6, 7),
            new DateOnly(2024, 6, 7));

        SharedCalendarRepository sharedCalendarRepository = new(_dbContext, _mapper);

        int id = await sharedCalendarRepository.Add(sharedCalendar);

        sharedCalendar.Id = id;

        SharedCalendar? addedSharedCalendar = await sharedCalendarRepository.GetSharedCalendarById(id);

        Assert.Equivalent(sharedCalendar, addedSharedCalendar);
    }
}
