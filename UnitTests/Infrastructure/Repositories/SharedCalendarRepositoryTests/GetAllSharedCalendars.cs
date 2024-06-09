using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories;

namespace UnitTests.Infrastructure.Repositories.SharedCalendarRepositoryTests;

public class GetAllSharedCalendars : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;
    private readonly List<SharedCalendar> _sharedCalendars;

    public GetAllSharedCalendars(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _sharedCalendars = [new() {
            Id = 1,
            Sender = new(){
                Id = 1,
                Name = "a",
                Email = "a",
                Password = "a",
            },
            Receiver = new(){
                Id = 2,
                Name = "b",
                Email = "b",
                Password = "b",
            },
            FromDate = new DateOnly(2024,6,7),
            ToDate = new DateOnly(2024,6,7)
        }];
    }

    [Fact]
    public async Task Should_ReturnAllSharedCalendar_When_CallsTheRepositoryMethod()
    {
        _dbContext = await new SharedCalendarRepositoryDBContext().GetDatabaseContext();

        SharedCalendarRepository sharedCalendarRepository = new(_dbContext, _mapper);

        List<SharedCalendar> sharedCalendars = await sharedCalendarRepository.GetAllSharedCalendars();

        Assert.Equivalent(_sharedCalendars, sharedCalendars);
    }
}
