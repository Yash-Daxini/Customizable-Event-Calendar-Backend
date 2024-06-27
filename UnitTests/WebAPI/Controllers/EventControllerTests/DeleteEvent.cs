using AutoMapper;
using Core.Exceptions;
using Core.Interfaces.IServices;
using FluentAssertions;
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

        actionResult.Should().BeOfType<OkResult>();
    }
    
    [Fact]
    public async Task Should_Return_NotFound_When_EventNotAvailableWithId()
    {
        _eventService.DeleteEvent(1, 1).ThrowsAsyncForAnyArgs(new NotFoundException(""));

        IActionResult actionResult = await _eventController.DeleteEvent(1, 1);

        actionResult.Should().BeOfType<NotFoundObjectResult>();
    }
    
    [Fact]
    public async Task Should_Return_ServerError_When_SomeErrorOccurred()
    {
        _eventService.DeleteEvent(1, 1).ThrowsAsyncForAnyArgs<Exception>();

        IActionResult actionResult = await _eventController.DeleteEvent(1, 1);

        actionResult.Should().BeOfType<ObjectResult>();
    }

}
