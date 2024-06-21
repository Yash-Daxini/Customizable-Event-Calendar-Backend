using AutoMapper;
using Core.Interfaces.IServices;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;
using WebAPI.Dtos;

namespace UnitTests.WebAPI.Controllers.EventControllerTests;

public class GetEventsForDailyView : IClassFixture<AutoMapperFixture>
{
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;
    private readonly EventController _eventController;

    public GetEventsForDailyView(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _eventService = Substitute.For<IEventService>();
        _eventController = new(_eventService, _mapper);
    }

    [Fact]
    public async Task Should_ReturnEvents_When_EventOfCurrentDay()
    {
        IActionResult actionResult = await _eventController.GetEventsForDailyView(1);

        actionResult.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccurred()
    {
        _eventService.GetEventsForDailyViewByUserId(1).Throws<Exception>();

        IActionResult actionResult = await _eventController.GetEventsForDailyView(1);

        actionResult.Should().BeOfType<ObjectResult>();
    }
}
