using AutoMapper;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using WebAPI.Controllers;

namespace UnitTests.WebAPI.Controllers.SharedCalendarControllerTests;

public class GetAllSharedCalendars
{

    private readonly ISharedCalendarService _sharedCalendarService;
    private readonly IMapper _mapper;
    private readonly SharedCalendarController _sharedCalendarController;

    public GetAllSharedCalendars()
    {
        _sharedCalendarService = Substitute.For<ISharedCalendarService>();
        _mapper = Substitute.For<IMapper>();
        _sharedCalendarController = new SharedCalendarController(_sharedCalendarService, _mapper);
    }

    [Fact]
    public async Task Should_ReturnIActionResult_When_SharedCalendarsAvailable()
    {
        IActionResult actionResult = await _sharedCalendarController.GetAllSharedCalendars();

        Assert.IsType<OkObjectResult>(actionResult);
    }
}
