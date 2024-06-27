using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IServices;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;

namespace UnitTests.WebAPI.Controllers.UserControllerTests;

public class GetUserById : IClassFixture<AutoMapperFixture>
{
    private readonly IUserService _userService;
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly IMapper _mapper;
    private readonly UserController _userController;

    public GetUserById(AutoMapperFixture autoMapperFixture)
    {
        _userService = Substitute.For<IUserService>();
        _userAuthenticationService = Substitute.For<IUserAuthenticationService>();
        _mapper = autoMapperFixture.Mapper;
        _userController = new UserController(_userService, _userAuthenticationService, _mapper);
    }

    [Fact]
    public async Task Should_Return_ActionResultNotFound_When_UserWithIdNotAvailable()
    {
        _userService.GetUserById(1).ThrowsForAnyArgs(new NotFoundException(""));

        IActionResult actionResult = await _userController.GetUserById(1);

        actionResult.Should().BeOfType<NotFoundObjectResult>();
    }
    
    [Fact]
    public async Task Should_Return_ActionResultOk_When_UserWithIdAvailable()
    {
        User user = Substitute.For<User>();

        _userService.GetUserById(1).ReturnsForAnyArgs(user);

        IActionResult actionResult = await _userController.GetUserById(1);

        actionResult.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task Should_Return_ServerError_When_SomeErrorOccurred()
    {
        _userService.GetUserById(1).ThrowsForAnyArgs<Exception>();

        IActionResult actionResult = await _userController.GetUserById(1);

        actionResult.Should().BeOfType<ObjectResult>();
    }
}
