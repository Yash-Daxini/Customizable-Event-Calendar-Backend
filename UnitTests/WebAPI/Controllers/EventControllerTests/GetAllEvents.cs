using AutoMapper;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;
using WebAPI.Dtos;

namespace UnitTests.WebAPI.Controllers.EventControllerTests;

public class GetAllEvents : IClassFixture<AutoMapperFixture>
{
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;
    private readonly EventController _eventController;
    private readonly List<EventResponseDto> _events;

    public GetAllEvents(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _eventService = Substitute.For<IEventService>();
        _eventController = new(_eventService, _mapper);
        _events = [new()
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
            Occurrences = [
                new (2024,2,1),
                new (2024,2,2),
                new (2024,2,3),
            ]
        }];
    }

    [Fact]
    public async Task Should_ReturnEvents_When_EventOfCurrentMonth()
    {
        List<EventResponseDto> events = [];

        IActionResult actionResult = await _eventController.GetAllEvents(1);

        var returnedResult = Assert.IsType<OkObjectResult>(actionResult);

        Assert.Equivalent(events, returnedResult.Value);
    }

    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccurred()
    {
        _eventService.GetAllEventsByUserId(1).Throws<Exception>();

        IActionResult actionResult = await _eventController.GetAllEvents(1);

        Assert.IsType<ObjectResult>(actionResult);
    }
}
