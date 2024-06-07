using AutoMapper;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using WebAPI.Controllers;
using WebAPI.Dtos;

namespace UnitTests.WebAPI.Controllers.UserControllerTests;

public class UpdateUser
{
    private readonly IUserService _userService;
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly IMapper _mapper;
    private readonly UserController _userController;

    public UpdateUser()
    {
        _userService = Substitute.For<IUserService>();
        _userAuthenticationService = Substitute.For<IUserAuthenticationService>();
        _mapper = Substitute.For<IMapper>();
        _userController = new UserController(_userService, _userAuthenticationService, _mapper);
    }

    [Fact]
    public async Task Should_UpdateUserAndReturnActionResult_When_CallsTheMethod()
    {
        UserDto userDto = Substitute.For<UserDto>();

        IActionResult actionResult = await _userController.AddUser(userDto);

        Assert.IsType<CreatedAtActionResult>(actionResult);
    }
}
