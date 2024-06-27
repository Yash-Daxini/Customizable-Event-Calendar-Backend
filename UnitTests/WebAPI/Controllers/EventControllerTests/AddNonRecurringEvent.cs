using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IServices;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;
using WebAPI.Dtos;

namespace UnitTests.WebAPI.Controllers.EventControllerTests;

public class AddNonRecurringEvent : IClassFixture<AutoMapperFixture>
{
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;
    private readonly EventController _eventController;
    private readonly Event _eventObj;
    private readonly NonRecurringEventRequestDto _nonRecurringEventRequestDto;

    public AddNonRecurringEvent(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _eventService = Substitute.For<IEventService>();
        _eventController = new(_eventService, _mapper);
        _eventObj = Substitute.For<Event>();
        _nonRecurringEventRequestDto = new NonRecurringEventRequestDto()
        {
            Title = "Test",
            Location = "Test",
            Description = "Test",
            Duration = new()
            {
                StartHour = 1,
                EndHour = 2,
            },
            EventDate = new DateOnly(2024,4,1),
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
    public async Task Should_AddNonRecurringEvent_When_EventNotOverlaps()
    {
        _eventService.AddNonRecurringEvent(_eventObj, 1).ReturnsForAnyArgs(1);

        IActionResult actionResult = await _eventController.AddNonRecurringEvent(1, _nonRecurringEventRequestDto);

        actionResult.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public async Task Should_Return_BadRequest_When_EventOverlaps()
    {
        _eventService.AddNonRecurringEvent(_eventObj, 1).ThrowsAsyncForAnyArgs(new EventOverlapException("Overlap"));

        IActionResult actionResult = await _eventController.AddNonRecurringEvent(1, _nonRecurringEventRequestDto);

        actionResult.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Should_Return_ServerError_When_SomeErrorOccurred()
    {
        _eventService.AddNonRecurringEvent(_eventObj, 1).ThrowsAsyncForAnyArgs<Exception>();

        IActionResult actionResult = await _eventController.AddNonRecurringEvent(1, _nonRecurringEventRequestDto);

        actionResult.Should().BeOfType<ObjectResult>();
    }
}
