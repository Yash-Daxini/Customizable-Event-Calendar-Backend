using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;
using WebAPI.Dtos;

namespace UnitTests.WebAPI.Controllers.EventControllerTests;

public class AddRecurringEvent : IClassFixture<AutoMapperFixture>
{

    private readonly IEventService _eventService;
    private readonly IMapper _mapper;
    private readonly EventController _eventController;

    public AddRecurringEvent(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _eventService = Substitute.For<IEventService>();
        _eventController = new(_eventService, _mapper);
    }

    [Fact]
    public async Task Should_AddRecurringEvent_When_EventNotOverlaps()
    {
        Event eventObj = Substitute.For<Event>();

        RecurringEventRequestDto recurringEventRequestDto = Substitute.For<RecurringEventRequestDto>();

        _eventService.AddEvent(eventObj, 1).ReturnsForAnyArgs(1);

        IActionResult actionResult = await _eventController.AddRecurringEvent(1, recurringEventRequestDto);

        var returnedResult = Assert.IsType<CreatedAtActionResult>(actionResult);

        Assert.Equivalent(new { addedEventId = 1 },returnedResult.Value);
    }
    
    [Fact]
    public async Task Should_ReturnBadRequest_When_EventOverlaps()
    {
        Event eventObj = Substitute.For<Event>();

        RecurringEventRequestDto recurringEventRequestDto = Substitute.For<RecurringEventRequestDto>();

        _eventService.AddEvent(eventObj, 1).ThrowsAsyncForAnyArgs<EventOverlapException>();

        IActionResult actionResult = await _eventController.AddRecurringEvent(1, recurringEventRequestDto);

        Assert.IsType<BadRequestObjectResult>(actionResult);
    }
    
    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccured()
    {
        Event eventObj = Substitute.For<Event>();

        RecurringEventRequestDto recurringEventRequestDto = Substitute.For<RecurringEventRequestDto>();

        _eventService.AddEvent(eventObj, 1).ThrowsAsyncForAnyArgs<Exception>();

        IActionResult actionResult = await _eventController.AddRecurringEvent(1, recurringEventRequestDto);

        Assert.IsType<ObjectResult>(actionResult);
    }
}
