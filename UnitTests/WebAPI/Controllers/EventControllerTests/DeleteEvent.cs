using AutoMapper;
using Core.Exceptions;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;

namespace UnitTests.WebAPI.Controllers.EventControllerTests;

public class DeleteEvent : IClassFixture<AutoMapperFixture>
{

    private readonly IEventService _eventService;
    private readonly IMapper _mapper;
    private readonly EventController _eventController;

    public DeleteEvent(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _eventService = Substitute.For<IEventService>();
        _eventController = new(_eventService, _mapper);
    }

    [Fact]
    public async Task Should_DeleteEvent_When_EventAvailableWithId()
    {
        IActionResult actionResult = await _eventController.DeleteEvent(1, 1);

        Assert.IsType<OkResult>(actionResult);
    }
    
    [Fact]
    public async Task Should_ReturnNotFound_When_EventNotAvailableWithId()
    {
        _eventService.DeleteEvent(1, 1).ThrowsAsyncForAnyArgs<NotFoundException>();

        IActionResult actionResult = await _eventController.DeleteEvent(1, 1);

        Assert.IsType<NotFoundObjectResult>(actionResult);
    }
    
    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccurred()
    {
        _eventService.DeleteEvent(1, 1).ThrowsAsyncForAnyArgs<Exception>();

        IActionResult actionResult = await _eventController.DeleteEvent(1, 1);

        Assert.IsType<ObjectResult>(actionResult);
    }

}
