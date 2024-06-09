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

public class UpdateEvent : IClassFixture<AutoMapperFixture>
{
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;
    private readonly EventController _eventController;

    public UpdateEvent(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _eventService = Substitute.For<IEventService>();
        _eventController = new(_eventService, _mapper);
    }

    [Fact]
    public async Task Should_UpdateEvent_When_EventNotOverlaps()
    {
        Event eventObj = Substitute.For<Event>();

        RecurringEventRequestDto recurringEventRequestDto = Substitute.For<RecurringEventRequestDto>();

        IActionResult actionResult = await _eventController.UpdateEvent(1, recurringEventRequestDto);

        var returnedResult = Assert.IsType<CreatedAtActionResult>(actionResult);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_When_EventOverlaps()
    {
        Event eventObj = Substitute.For<Event>();

        RecurringEventRequestDto recurringEventRequestDto = Substitute.For<RecurringEventRequestDto>();

        _eventService.UpdateEvent(eventObj, 1).ThrowsAsyncForAnyArgs<EventOverlapException>();

        IActionResult actionResult = await _eventController.UpdateEvent(1, recurringEventRequestDto);

        Assert.IsType<BadRequestObjectResult>(actionResult);
    }
    
    [Fact]
    public async Task Should_ReturnBadRequest_When_EventNotAvailableWithId()
    {
        Event eventObj = Substitute.For<Event>();

        RecurringEventRequestDto recurringEventRequestDto = Substitute.For<RecurringEventRequestDto>();

        _eventService.UpdateEvent(eventObj, 1).ThrowsAsyncForAnyArgs<NotFoundException>();

        IActionResult actionResult = await _eventController.UpdateEvent(1, recurringEventRequestDto);

        Assert.IsType<NotFoundObjectResult>(actionResult);
    }

    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccured()
    {
        Event eventObj = Substitute.For<Event>();

        RecurringEventRequestDto recurringEventRequestDto = Substitute.For<RecurringEventRequestDto>();

        _eventService.UpdateEvent(eventObj, 1).ThrowsAsyncForAnyArgs<Exception>();

        IActionResult actionResult = await _eventController.UpdateEvent(1, recurringEventRequestDto);

        Assert.IsType<ObjectResult>(actionResult);
    }
}
