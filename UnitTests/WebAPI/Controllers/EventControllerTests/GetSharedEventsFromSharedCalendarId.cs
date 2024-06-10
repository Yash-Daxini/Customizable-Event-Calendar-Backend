using AutoMapper;
using Core.Exceptions;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;
using WebAPI.Dtos;

namespace UnitTests.WebAPI.Controllers.EventControllerTests;

public class GetSharedEventsFromSharedCalendarId : IClassFixture<AutoMapperFixture>
{
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;
    private readonly EventController _eventController;

    public GetSharedEventsFromSharedCalendarId(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _eventService = Substitute.For<IEventService>();
        _eventController = new(_eventService, _mapper);
    }

    [Fact]
    public async Task Should_ReturnEvents_When_SharedCalendarAvailableWithId()
    {
        List<EventResponseDto> events = [];

        IActionResult actionResult = await _eventController.GetSharedEventsFromSharedCalendarId(1);

        var returnedResult = Assert.IsType<OkObjectResult>(actionResult);

        Assert.Equivalent(events, returnedResult.Value);
    }
    
    [Fact]
    public async Task Should_ReturnNotFound_When_SharedCalendarNotAvailableWithId()
    {
        _eventService.GetSharedEvents(1).Throws<NotFoundException>();

        IActionResult actionResult = await _eventController.GetSharedEventsFromSharedCalendarId(1);

        Assert.IsType<NotFoundObjectResult>(actionResult);
    }
    
    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccurred()
    {
        _eventService.GetSharedEvents(1).Throws<Exception>();

        IActionResult actionResult = await _eventController.GetSharedEventsFromSharedCalendarId(1);

        Assert.IsType<ObjectResult>(actionResult);
    }
}
