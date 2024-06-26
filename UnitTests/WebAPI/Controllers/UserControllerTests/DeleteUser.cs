using AutoMapper;
using Core.Exceptions;
using Core.Interfaces.IServices;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;

namespace UnitTests.WebAPI.Controllers.UserControllerTests;

public class DeleteUser : IClassFixture<AutoMapperFixture>
{
    private readonly IUserService _userService;
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly IMapper _mapper;
    private readonly UserController _userController;

    public DeleteUser(AutoMapperFixture autoMapperFixture)
    {
        _userService = Substitute.For<IUserService>();
        _userAuthenticationService = Substitute.For<IUserAuthenticationService>();
        _mapper = autoMapperFixture.Mapper;
        _userController = new UserController(_userService, _userAuthenticationService, _mapper);
    }

    [Fact]
    public async Task Should_DeleteUserAndReturnActionResult_When_UserAvailableWithId()
    {
        _userService.DeleteUser(1).Returns(IdentityResult.Success);

        IActionResult actionResult = await _userController.DeleteUser(1);

        actionResult.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task Should_DeleteUserAndReturnActionResult_When_UserNotAvailableWithId()
    {
        _userService.DeleteUser(1).Throws(new NotFoundException(""));

        IActionResult actionResult = await _userController.DeleteUser(1);

        actionResult.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Should_ReturnBadRequest_When_DeleteOperationFailed()
    {
        _userService.DeleteUser(1).Returns(IdentityResult.Failed());

        IActionResult actionResult = await _userController.DeleteUser(1);

        actionResult.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccurred()
    {
        _userService.DeleteUser(1).Throws<Exception>();

        IActionResult actionResult = await _userController.DeleteUser(1);

        actionResult.Should().BeOfType<ObjectResult>();
    }
}
