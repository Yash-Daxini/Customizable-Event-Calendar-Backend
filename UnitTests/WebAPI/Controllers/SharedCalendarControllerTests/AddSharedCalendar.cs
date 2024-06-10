using AutoMapper;
using Core.Entities;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;
using WebAPI.Dtos;

namespace UnitTests.WebAPI.Controllers.SharedCalendarControllerTests;

public class AddSharedCalendar : IClassFixture<AutoMapperFixture>
{
    private readonly ISharedCalendarService _sharedCalendarService;
    private readonly IMapper _mapper;
    private readonly SharedCalendarController _sharedCalendarController;

    public AddSharedCalendar(AutoMapperFixture autoMapperFixture)
    {
        _sharedCalendarService = Substitute.For<ISharedCalendarService>();
        _mapper = autoMapperFixture.Mapper;
        _sharedCalendarController = new SharedCalendarController(_sharedCalendarService, _mapper);
    }

    [Fact]
    public async Task Should_ReturnIActionResult_When_SharedCalendarAdded()
    {
        SharedCalendar sharedCalendar = new(1, new User
        {
            Id = 48,
            Name = "a",
            Email = "a@gmail.com",
            Password = "a"
        }, new User
        {
            Id = 49,
            Name = "b",
            Email = "b@gmail.com",
            Password = "b"
        }, new DateOnly(), new DateOnly());

        _sharedCalendarService.AddSharedCalendar(sharedCalendar).ReturnsForAnyArgs(1);

        SharedCalendarDto sharedCalendarDto = Substitute.For<SharedCalendarDto>();

        IActionResult actionResult = await _sharedCalendarController.AddSharedCalendar(sharedCalendarDto);

        var returnedValue = Assert.IsType<CreatedAtActionResult>(actionResult);

        Assert.Equivalent(new { addedSharedCalendarId = 1 }, returnedValue.Value);
    }

    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccurred()
    {
        SharedCalendar sharedCalendar = new(1, new User
        {
            Id = 48,
            Name = "a",
            Email = "a@gmail.com",
            Password = "a"
        }, new User
        {
            Id = 49,
            Name = "b",
            Email = "b@gmail.com",
            Password = "b"
        }, new DateOnly(), new DateOnly());

        _sharedCalendarService.AddSharedCalendar(sharedCalendar).ThrowsAsyncForAnyArgs<Exception>();

        SharedCalendarDto sharedCalendarDto = Substitute.For<SharedCalendarDto>();

        IActionResult actionResult = await _sharedCalendarController.AddSharedCalendar(sharedCalendarDto);

        Assert.IsType<ObjectResult>(actionResult);
    }
}
