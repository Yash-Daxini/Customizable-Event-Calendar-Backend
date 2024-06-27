using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using FluentAssertions;

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
        _dbContext = await new SharedCalendarRepositoryDBContext().GetDatabaseContext();

        SharedCalendar sharedCalendar = new(
            1,
            new User { Id = 1, Name = "a", Email = "a" },
            new User { Id = 2, Name = "b", Email = "b" },
            new DateOnly(2024, 6, 7),
            new DateOnly(2024, 6, 7));

        SharedCalendarRepository sharedCalendarRepository = new(_dbContext, _mapper);

        SharedCalendar? sharedCalendarById = await sharedCalendarRepository.GetSharedCalendarById(1);

        sharedCalendarById.Should().BeEquivalentTo(sharedCalendar);
    }

    [Fact]
    public async Task Should_Return_Null_When_SharedCalendarNotAvailableWithGivenId()
    {
        _dbContext = await new SharedCalendarRepositoryDBContext().GetDatabaseContext();

        SharedCalendarRepository sharedCalendarRepository = new(_dbContext, _mapper);

        SharedCalendar? sharedCalendarById = await sharedCalendarRepository.GetSharedCalendarById(2);

        sharedCalendarById.Should().BeNull();
    }
}
