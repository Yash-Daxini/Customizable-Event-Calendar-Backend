using AutoMapper;
using Core.Entities;
using Core.Interfaces.IServices;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using UnitTests.Builders.EntityBuilder;
using WebAPI.Controllers;
using WebAPI.Dtos;

namespace UnitTests.WebAPI.Controllers.SharedCalendarControllerTests;

public class AddSharedCalendar : IClassFixture<AutoMapperFixture>
{
    private readonly ISharedCalendarService _sharedCalendarService;
    private readonly IMapper _mapper;
    private readonly SharedCalendarController _sharedCalendarController;

    private readonly SharedCalendar _sharedCalendar;

    public AddSharedCalendar(AutoMapperFixture autoMapperFixture)
    {
        _sharedCalendarService = Substitute.For<ISharedCalendarService>();
        _mapper = autoMapperFixture.Mapper;
        _sharedCalendarController = new SharedCalendarController(_sharedCalendarService, _mapper);
        _sharedCalendar = new(1,
                              new UserBuilder(48).Build(),
                              new UserBuilder(49).Build(),
                              new DateOnly(),
                              new DateOnly());
    }

    [Fact]
    public async Task Should_Return_IActionResult_When_SharedCalendarAdded()
    {
        _sharedCalendarService.AddSharedCalendar(_sharedCalendar).ReturnsForAnyArgs(1);

        SharedCalendarDto sharedCalendarDto = Substitute.For<SharedCalendarDto>();

        IActionResult actionResult = await _sharedCalendarController.AddSharedCalendar(sharedCalendarDto);

        actionResult.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public async Task Should_Return_ServerError_When_SomeErrorOccurred()
    {
        _sharedCalendarService.AddSharedCalendar(_sharedCalendar).ThrowsAsyncForAnyArgs<Exception>();

        SharedCalendarDto sharedCalendarDto = Substitute.For<SharedCalendarDto>();

        IActionResult actionResult = await _sharedCalendarController.AddSharedCalendar(sharedCalendarDto);

        actionResult.Should().BeOfType<ObjectResult>();
    }
}
