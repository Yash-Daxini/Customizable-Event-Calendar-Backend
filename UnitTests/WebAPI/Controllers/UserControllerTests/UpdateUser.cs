using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IServices;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using UnitTests.Builders.EntityBuilder;
using WebAPI.Controllers;
using WebAPI.Dtos;

namespace UnitTests.WebAPI.Controllers.UserControllerTests;

public class UpdateUser : IClassFixture<AutoMapperFixture>
{
    private readonly IUserService _userService;
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly IMapper _mapper;
    private readonly UserController _userController;

    public UpdateUser(AutoMapperFixture autoMapperFixture)
    {
        _userService = Substitute.For<IUserService>();
        _userAuthenticationService = Substitute.For<IUserAuthenticationService>();
        _mapper = autoMapperFixture.Mapper;
        _userController = new UserController(_userService, _userAuthenticationService, _mapper);
    }

    [Fact]
    public async Task Should_Return_ActionResult_When_CallsTheMethod()
    {
        UserRequestDto userDto = Substitute.For<UserRequestDto>();

        User user = new UserBuilder(49)
            .WithName("b")
            .WithEmail("b@gmail.com")
            .WithPassword("b")
            .Build();

        _userService.UpdateUser(user).ReturnsForAnyArgs(IdentityResult.Success);

        IActionResult actionResult = await _userController.UpdateUser(userDto);

        actionResult.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Should_Return_BadRequest_When_UserNotAvailableWithId()
    {
        UserRequestDto userDto = Substitute.For<UserRequestDto>();

        User userModel = Substitute.For<User>();

        _userService.UpdateUser(userModel).ThrowsForAnyArgs(new NotFoundException(""));

        IActionResult actionResult = await _userController.UpdateUser(userDto);

        actionResult.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Should_Return_BadRequest_When_UpdateOperationFailed()
    {
        UserRequestDto userDto = Substitute.For<UserRequestDto>();

        User user = Substitute.For<User>();

        _userService.UpdateUser(user).ReturnsForAnyArgs(IdentityResult.Failed());

        IActionResult actionResult = await _userController.UpdateUser(userDto);

        actionResult.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Should_Return_ServerError_When_SomeErrorOccurred()
    {
        UserRequestDto userDto = Substitute.For<UserRequestDto>();

        User user = Substitute.For<User>();

        _userService.UpdateUser(user).ThrowsForAnyArgs<Exception>();

        IActionResult actionResult = await _userController.UpdateUser(userDto);

        actionResult.Should().BeOfType<ObjectResult>();
    }
}
