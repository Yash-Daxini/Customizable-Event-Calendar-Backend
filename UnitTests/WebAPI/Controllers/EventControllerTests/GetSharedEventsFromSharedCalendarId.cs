using AutoMapper;
using Core.Exceptions;
using Core.Interfaces.IServices;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;

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
        IActionResult actionResult = await _eventController.GetSharedEventsFromSharedCalendarId(1);

        actionResult.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task Should_ReturnNotFound_When_SharedCalendarNotAvailableWithId()
    {
        _eventService.GetSharedEvents(1).Throws<NotFoundException>();

        IActionResult actionResult = await _eventController.GetSharedEventsFromSharedCalendarId(1);

        actionResult.Should().BeOfType<NotFoundObjectResult>();
    }
    
    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccurred()
    {
        _eventService.GetSharedEvents(1).Throws<Exception>();

        IActionResult actionResult = await _eventController.GetSharedEventsFromSharedCalendarId(1);

        actionResult.Should().BeOfType<ObjectResult>();
    }
}
