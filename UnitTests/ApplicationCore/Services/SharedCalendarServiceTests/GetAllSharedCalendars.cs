﻿using Core.Entities;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;

namespace UnitTests.ApplicationCore.Services.SharedCalendarServiceTests;

public class GetAllSharedCalendars
{

    private readonly ISharedCalendarService _sharedCalendarService;

    private readonly ISharedCalendarRepository _sharedCalendarRepository;

    private readonly List<SharedCalendar> _sharedCalendars;

    public GetAllSharedCalendars()
    {
        _sharedCalendarRepository = Substitute.For<ISharedCalendarRepository>();
        _sharedCalendarService = new SharedCalendarService(_sharedCalendarRepository);
        _sharedCalendars =
            [
                new(1,
                    new(1,"1","x@gmail.com","1"),
                    new(2,"2","y@gmail.com","2"),
                    new DateOnly(2024,6,2),
                    new DateOnly(2024,6,20)),
                new(2,
                    new(2,"2","y@gmail.com","2"),
                    new(1,"1","x@gmail.com","1"),
                    new DateOnly(2024,6,12),
                    new DateOnly(2024,6,22))
            ];
    }

    [Fact]
    public async Task Should_ReturnListOfSharedCalendars_When_SharedCalendarsAvailable()
    {
        _sharedCalendarRepository.GetAllSharedCalendars().Returns(_sharedCalendars);
        List<SharedCalendar> sharedCalendars = await _sharedCalendarService.GetAllSharedCalendars();

        _sharedCalendarRepository.Received().GetAllSharedCalendars();

        Assert.Equal(sharedCalendars[0], _sharedCalendars[0]);
        Assert.Equal(sharedCalendars[1], _sharedCalendars[1]);
    }
}
