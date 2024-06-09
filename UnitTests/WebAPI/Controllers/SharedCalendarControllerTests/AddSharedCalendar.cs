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
        SharedCalendar sharedCalendar = Substitute.For<SharedCalendar>();

        _sharedCalendarService.AddSharedCalendar(sharedCalendar).ReturnsForAnyArgs(1);

        SharedCalendarDto sharedCalendarDto = Substitute.For<SharedCalendarDto>();

        IActionResult actionResult = await _sharedCalendarController.AddSharedCalendar(sharedCalendarDto);

        var returnedValue = Assert.IsType<CreatedAtActionResult>(actionResult);

        Assert.Equivalent(new { addedSharedCalendarId = 1 }, returnedValue.Value);
    }
    
    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccured()
    {
        SharedCalendar sharedCalendar = Substitute.For<SharedCalendar>();

        _sharedCalendarService.AddSharedCalendar(sharedCalendar).ThrowsAsyncForAnyArgs<Exception>();

        SharedCalendarDto sharedCalendarDto = Substitute.For<SharedCalendarDto>();

        IActionResult actionResult = await _sharedCalendarController.AddSharedCalendar(sharedCalendarDto);

        Assert.IsType<ObjectResult>(actionResult);
    }
}
