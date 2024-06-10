using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;
using WebAPI.Dtos;

namespace UnitTests.WebAPI.Controllers.SharedCalendarControllerTests;

public class GetSharedCalendarById : IClassFixture<AutoMapperFixture>
{
    private readonly ISharedCalendarService _sharedCalendarService;
    private readonly IMapper _mapper;
    private readonly SharedCalendarController _sharedCalendarController;

    public GetSharedCalendarById(AutoMapperFixture autoMapperFixture)
    {
        _sharedCalendarService = Substitute.For<ISharedCalendarService>();
        _mapper = autoMapperFixture.Mapper;
        _sharedCalendarController = new SharedCalendarController(_sharedCalendarService, _mapper);
    }

    [Fact]
    public async Task Should_ReturnSharedCalendar_When_SharedCalendarAvailableWithId()
    {
        SharedCalendarDto sharedCalendarDto = Substitute.For<SharedCalendarDto>();

        SharedCalendar sharedCalendar = Substitute.For<SharedCalendar>();

        _sharedCalendarService.GetSharedCalendarById(1).Returns(sharedCalendar);

        IActionResult actionResult = await _sharedCalendarController.GetSharedCalendarById(1);

        var returedResult = Assert.IsType<OkObjectResult>(actionResult);

        Assert.Equivalent(sharedCalendarDto,returedResult.Value);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_When_SharedCalendarNotAvailableWithId()
    {
        SharedCalendarDto sharedCalendarDto = Substitute.For<SharedCalendarDto>();

        SharedCalendar sharedCalendar = Substitute.For<SharedCalendar>();

        _sharedCalendarService.GetSharedCalendarById(1).Throws<NotFoundException>();

        IActionResult actionResult = await _sharedCalendarController.GetSharedCalendarById(1);

        var returedResult = Assert.IsType<NotFoundObjectResult>(actionResult);
    }
    
    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccurred()
    {
        SharedCalendarDto sharedCalendarDto = Substitute.For<SharedCalendarDto>();

        SharedCalendar sharedCalendar = Substitute.For<SharedCalendar>();

        _sharedCalendarService.GetSharedCalendarById(1).Throws<Exception>();

        IActionResult actionResult = await _sharedCalendarController.GetSharedCalendarById(1);

        Assert.IsType<ObjectResult>(actionResult);
    }

}
