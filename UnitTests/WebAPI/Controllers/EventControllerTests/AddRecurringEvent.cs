using AutoMapper;
using Core.Entities;
using Core.Entities.Enums;
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
    private readonly Event _eventObj;
    private readonly RecurringEventRequestDto _recurringEventRequestDto;

    public AddRecurringEvent(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _eventService = Substitute.For<IEventService>();
        _eventController = new(_eventService, _mapper);
        _eventObj = Substitute.For<Event>();
        _recurringEventRequestDto = new()
        {
            Id = 1,
            Title = "Test",
            Description = "Test",
            Location = "Test",
            Duration = new()
            {
                StartHour = 1,
                EndHour = 2
            },
            RecurrencePattern = new()
            {
                StartDate = new DateOnly(),
                EndDate = new DateOnly(),
                Interval = 1,
                Frequency = "None",
                ByMonth = null,
                ByWeekDay = null,
                ByMonthDay = null,
                WeekOrder = null,
            },
            EventCollaborators = [
            new() {
                Id = 1,
                UserId = 1,
                EventCollaboratorRole = "Organizer",
                ConfirmationStatus = "Accept"
            }]
        };
    }

    [Fact]
    public async Task Should_AddRecurringEvent_When_EventNotOverlapsAndOneTime()
    {
        _eventService.AddEvent(_eventObj, 1).ReturnsForAnyArgs(1);

        IActionResult actionResult = await _eventController.AddRecurringEvent(1, _recurringEventRequestDto);

        var returnedResult = Assert.IsType<CreatedAtActionResult>(actionResult);

        Assert.Equivalent(new { addedEventId = 1 }, returnedResult.Value);
    }

    [Fact]
    public async Task Should_AddRecurringEvent_When_EventNotOverlapsAndEventIsDailyRecurring()
    {
        _recurringEventRequestDto.RecurrencePattern.Frequency = "Daily";
        _recurringEventRequestDto.RecurrencePattern.Interval = 1;
        _recurringEventRequestDto.RecurrencePattern.ByWeekDay = null;

        _eventService.AddEvent(_eventObj, 1).ReturnsForAnyArgs(1);

        IActionResult actionResult = await _eventController.AddRecurringEvent(1, _recurringEventRequestDto);

        var returnedResult = Assert.IsType<CreatedAtActionResult>(actionResult);

        Assert.Equivalent(new { addedEventId = 1 }, returnedResult.Value);
    }

    [Fact]
    public async Task Should_AddRecurringEvent_When_EventNotOverlapsAndEventIsWeeklyRecurring()
    {
        _recurringEventRequestDto.RecurrencePattern.Frequency = "Weekly";
        _recurringEventRequestDto.RecurrencePattern.Interval = 1;
        _recurringEventRequestDto.RecurrencePattern.ByWeekDay = [1];

        _eventService.AddEvent(_eventObj, 1).ReturnsForAnyArgs(1);

        IActionResult actionResult = await _eventController.AddRecurringEvent(1, _recurringEventRequestDto);

        var returnedResult = Assert.IsType<CreatedAtActionResult>(actionResult);

        Assert.Equivalent(new { addedEventId = 1 }, returnedResult.Value);
    }

    [Fact]
    public async Task Should_AddRecurringEvent_When_EventNotOverlapsAndEventIsMonthlyRecurring()
    {
        _recurringEventRequestDto.RecurrencePattern.Frequency = "Monthly";
        _recurringEventRequestDto.RecurrencePattern.Interval = 1;
        _recurringEventRequestDto.RecurrencePattern.ByWeekDay = [1];
        _recurringEventRequestDto.RecurrencePattern.WeekOrder = 1;

        _eventService.AddEvent(_eventObj, 1).ReturnsForAnyArgs(1);

        IActionResult actionResult = await _eventController.AddRecurringEvent(1, _recurringEventRequestDto);

        var returnedResult = Assert.IsType<CreatedAtActionResult>(actionResult);

        Assert.Equivalent(new { addedEventId = 1 }, returnedResult.Value);
    }

    [Fact]
    public async Task Should_AddRecurringEvent_When_EventNotOverlapsAndEventIsYearlyRecurring()
    {
        _recurringEventRequestDto.RecurrencePattern.Frequency = "Yearly";
        _recurringEventRequestDto.RecurrencePattern.Interval = 1;
        _recurringEventRequestDto.RecurrencePattern.ByWeekDay = [1];
        _recurringEventRequestDto.RecurrencePattern.WeekOrder = 1;
        _recurringEventRequestDto.RecurrencePattern.ByMonth = 12;

        _eventService.AddEvent(_eventObj, 1).ReturnsForAnyArgs(1);

        IActionResult actionResult = await _eventController.AddRecurringEvent(1, _recurringEventRequestDto);

        var returnedResult = Assert.IsType<CreatedAtActionResult>(actionResult);

        Assert.Equivalent(new { addedEventId = 1 }, returnedResult.Value);
    }

    [Fact]
    public async Task Should_AddRecurringEvent_When_EventNotOverlapsAndEventIsMonthlyRecurringUsingMonthDay()
    {
        _recurringEventRequestDto.RecurrencePattern.Frequency = "Monthly";
        _recurringEventRequestDto.RecurrencePattern.Interval = 1;
        _recurringEventRequestDto.RecurrencePattern.ByWeekDay = null;
        _recurringEventRequestDto.RecurrencePattern.WeekOrder = null;
        _recurringEventRequestDto.RecurrencePattern.ByMonthDay = 31;

        _eventService.AddEvent(_eventObj, 1).ReturnsForAnyArgs(1);

        IActionResult actionResult = await _eventController.AddRecurringEvent(1, _recurringEventRequestDto);

        var returnedResult = Assert.IsType<CreatedAtActionResult>(actionResult);

        Assert.Equivalent(new { addedEventId = 1 }, returnedResult.Value);
    }

    [Fact]
    public async Task Should_AddRecurringEvent_When_EventNotOverlapsAndEventIsYearlyRecurringUsingMonthDay()
    {
        _recurringEventRequestDto.RecurrencePattern.Frequency = "Yearly";
        _recurringEventRequestDto.RecurrencePattern.Interval = 1;
        _recurringEventRequestDto.RecurrencePattern.ByWeekDay = null;
        _recurringEventRequestDto.RecurrencePattern.WeekOrder = null;
        _recurringEventRequestDto.RecurrencePattern.ByMonthDay = null;
        _recurringEventRequestDto.RecurrencePattern.ByMonth= 12;

        _eventService.AddEvent(_eventObj, 1).ReturnsForAnyArgs(1);

        IActionResult actionResult = await _eventController.AddRecurringEvent(1, _recurringEventRequestDto);

        var returnedResult = Assert.IsType<CreatedAtActionResult>(actionResult);

        Assert.Equivalent(new { addedEventId = 1 }, returnedResult.Value);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_When_EventOverlaps()
    {
        _eventService.AddEvent(_eventObj, 1).ThrowsAsyncForAnyArgs<EventOverlapException>();

        IActionResult actionResult = await _eventController.AddRecurringEvent(1, _recurringEventRequestDto);

        Assert.IsType<BadRequestObjectResult>(actionResult);
    }

    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccurred()
    {
        _eventService.AddEvent(_eventObj, 1).ThrowsAsyncForAnyArgs<Exception>();

        IActionResult actionResult = await _eventController.AddRecurringEvent(1, _recurringEventRequestDto);

        Assert.IsType<ObjectResult>(actionResult);
    }
}
