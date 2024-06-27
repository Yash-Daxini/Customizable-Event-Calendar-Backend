using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IServices;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;

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
    public async Task Should_Return_SharedCalendar_When_SharedCalendarAvailableWithId()
    {
        SharedCalendar sharedCalendar = new(1, 
                                            new User 
                                            { 
                                                Id = 48, 
                                                Name = "a", 
                                                Email = "a@gmail.com",
                                                Password = "a" 
                                            }, 
                                            new User 
                                            { 
                                                Id = 49,
                                                Name = "b",
                                                Email = "b@gmail.com",
                                                Password = "b"
                                            }, 
                                            new DateOnly(),
                                            new DateOnly());

        _sharedCalendarService.GetSharedCalendarById(1).Returns(sharedCalendar);

        IActionResult actionResult = await _sharedCalendarController.GetSharedCalendarById(1);

        actionResult.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Should_Return_BadRequest_When_SharedCalendarNotAvailableWithId()
    {
        _sharedCalendarService.GetSharedCalendarById(1).Throws(new NotFoundException(""));

        IActionResult actionResult = await _sharedCalendarController.GetSharedCalendarById(1);

        actionResult.Should().BeOfType<NotFoundObjectResult>();
    }
    
    [Fact]
    public async Task Should_Return_ServerError_When_SomeErrorOccurred()
    {
        _sharedCalendarService.GetSharedCalendarById(1).Throws<Exception>();

        IActionResult actionResult = await _sharedCalendarController.GetSharedCalendarById(1);

        actionResult.Should().BeOfType<ObjectResult>();
    }

}
