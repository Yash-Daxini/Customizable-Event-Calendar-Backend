using AutoMapper;
using Core.Entities;
using Core.Interfaces.IServices;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;

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
    public async Task Should_Return_IActionResult_When_SharedCalendarsAvailable()
    {
        List<SharedCalendar> sharedCalendars = Substitute.For<List<SharedCalendar>>();

        _sharedCalendarService.GetAllSharedCalendars().Returns(sharedCalendars);

        IActionResult actionResult = await _sharedCalendarController.GetAllSharedCalendars();

        actionResult.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task Should_Return_ServerError_When_SomeErrorOccurred()
    {
        _sharedCalendarService.GetAllSharedCalendars().Throws<Exception>();

        IActionResult actionResult = await _sharedCalendarController.GetAllSharedCalendars();

        actionResult.Should().BeOfType<ObjectResult>();
    }
}
