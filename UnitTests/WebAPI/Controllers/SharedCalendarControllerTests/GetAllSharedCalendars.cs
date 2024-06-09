using AutoMapper;
using Core.Entities;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;
using WebAPI.Dtos;

namespace UnitTests.WebAPI.Controllers.SharedCalendarControllerTests;

public class GetAllSharedCalendars : IClassFixture<AutoMapperFixture>
{

    private readonly ISharedCalendarService _sharedCalendarService;
    private readonly IMapper _mapper;
    private readonly SharedCalendarController _sharedCalendarController;

    public GetAllSharedCalendars(AutoMapperFixture autoMapperFixture)
    {
        _sharedCalendarService = Substitute.For<ISharedCalendarService>();
        _mapper = autoMapperFixture.Mapper;
        _sharedCalendarController = new SharedCalendarController(_sharedCalendarService, _mapper);
    }

    [Fact]
    public async Task Should_ReturnIActionResult_When_SharedCalendarsAvailable()
    {
        List<SharedCalendar> sharedCalendars = Substitute.For<List<SharedCalendar>>();

        _sharedCalendarService.GetAllSharedCalendars().Returns(sharedCalendars);

        List<SharedCalendarDto> sharedCalendarDtos = Substitute.For<List<SharedCalendarDto>>();

        IActionResult actionResult = await _sharedCalendarController.GetAllSharedCalendars();

        var returedResult = Assert.IsType<OkObjectResult>(actionResult);

        Assert.Equivalent(sharedCalendarDtos,returedResult.Value);
    }
    
    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccured()
    {
        List<SharedCalendar> sharedCalendars = Substitute.For<List<SharedCalendar>>();

        _sharedCalendarService.GetAllSharedCalendars().Throws<Exception>();

        List<SharedCalendarDto> sharedCalendarDtos = Substitute.For<List<SharedCalendarDto>>();

        IActionResult actionResult = await _sharedCalendarController.GetAllSharedCalendars();

        Assert.IsType<ObjectResult>(actionResult);
    }
}
