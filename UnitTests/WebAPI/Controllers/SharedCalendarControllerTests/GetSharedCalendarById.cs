using AutoMapper;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using WebAPI.Controllers;
using WebAPI.Dtos;

namespace UnitTests.WebAPI.Controllers.SharedCalendarControllerTests;

public class GetSharedCalendarById
{
    private readonly ISharedCalendarService _sharedCalendarService;
    private readonly IMapper _mapper;
    private readonly SharedCalendarController _sharedCalendarController;

    public GetSharedCalendarById()
    {
        _sharedCalendarService = Substitute.For<ISharedCalendarService>();
        _mapper = Substitute.For<IMapper>();
        _sharedCalendarController = new SharedCalendarController(_sharedCalendarService, _mapper);
    }

    [Fact]
    public async Task Should_AddSharedCalendar_When_CallsTheMethod()
    {
        SharedCalendarDto sharedCalendarDto = Substitute.For<SharedCalendarDto>();

        IActionResult actionResult = await _sharedCalendarController.AddSharedCalendar(sharedCalendarDto);

        Assert.IsType<CreatedAtActionResult>(actionResult);
    }

}
