using AutoMapper;
using Core.Entities;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;
using WebAPI.Dtos;

namespace UnitTests.WebAPI.Controllers.UserControllerTests;

public class AddUser : IClassFixture<AutoMapperFixture>
{
    private readonly IUserService _userService;
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly IMapper _mapper;
    private readonly UserController _userController;

    public AddUser(AutoMapperFixture autoMapperFixture)
    {
        _userService = Substitute.For<IUserService>();
        _userAuthenticationService = Substitute.For<IUserAuthenticationService>();
        _mapper = autoMapperFixture.Mapper;
        _userController = new UserController(_userService, _userAuthenticationService, _mapper);
    }

    [Fact]
    public async Task Should_AddUserAndReturnActionResult_When_CallsTheMethod()
    {
        UserDto userDto = new() { Id = 49, Name = "b", Email = "b@gmail.com", Password = "b" };

        User user = new(49, "b", "b@gmail.com", "b");

        _userService.AddUser(user).ReturnsForAnyArgs(1);

        IActionResult actionResult = await _userController.AddUser(userDto);

        var returnedResult = Assert.IsType<CreatedAtActionResult>(actionResult);

        Assert.Equivalent(new { addedUserId = 1 }, returnedResult.Value);
    }

    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccurred()
    {
        UserDto userDto = Substitute.For<UserDto>();

        User user = Substitute.For<User>();

        _userService.AddUser(user).ThrowsForAnyArgs<Exception>();

        IActionResult actionResult = await _userController.AddUser(userDto);

        Assert.IsType<ObjectResult>(actionResult);
    }
}
