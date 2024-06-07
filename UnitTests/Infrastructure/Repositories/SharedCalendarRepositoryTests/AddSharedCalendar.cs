using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using NSubstitute;
using Infrastructure.DataModels;

namespace UnitTests.Infrastructure.Repositories.SharedCalendarRepositoryTests;

public class AddSharedCalendar
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;
    private readonly List<SharedCalendar> _sharedCalendars;

    public AddSharedCalendar()
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
    public async Task Should_AddSharedCalendarAndReturnSharedCalendarId_When_CallsTheRepositoryMethod()
    {
        _dbContext = await new SharedCalendarRepositoryDBContext().GetDatabaseContext();

        SharedCalendar sharedCalendar = new()
        {
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

        SharedCalendarDataModel sharedCalendarDataModel = new()
        {
            SenderId = 1,
            ReceiverId = 2,
            FromDate = new DateOnly(2024, 6, 7),
            ToDate = new DateOnly(2024, 6, 7)
        };

        _mapper.Map<SharedCalendarDataModel>(sharedCalendar).ReturnsForAnyArgs(sharedCalendarDataModel);

        SharedCalendarRepository sharedCalendarRepository = new(_dbContext, _mapper);

        int id = await sharedCalendarRepository.Add(sharedCalendar);

        sharedCalendar.Id = id;

        _mapper.Map<SharedCalendar>(sharedCalendarDataModel).ReturnsForAnyArgs(sharedCalendar);

        SharedCalendar? addedSharedCalendar = await sharedCalendarRepository.GetSharedCalendarById(id);

        Assert.Equivalent(sharedCalendar,addedSharedCalendar);
    }
}
