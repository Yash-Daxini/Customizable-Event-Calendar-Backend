using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.DataModels;
using Infrastructure.Repositories;
using NSubstitute;

namespace UnitTests.Infrastructure.Repositories.SharedCalendarRepositoryTests;

public class GetAllSharedCalendars
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;
    private readonly List<SharedCalendar> _sharedCalendars;

    public GetAllSharedCalendars()
    {
        _mapper = Substitute.For<IMapper>();
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

        List<SharedCalendarDataModel> sharedCalendarDataModels = [new()
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
        }];

        _mapper.Map<List<SharedCalendar>>(sharedCalendarDataModels).ReturnsForAnyArgs(_sharedCalendars);

        SharedCalendarRepository sharedCalendarRepository = new(_dbContext, _mapper);

        List<SharedCalendar> sharedCalendars = await sharedCalendarRepository.GetAllSharedCalendars();

        Assert.Equivalent(_sharedCalendars, sharedCalendars);
    }
}
