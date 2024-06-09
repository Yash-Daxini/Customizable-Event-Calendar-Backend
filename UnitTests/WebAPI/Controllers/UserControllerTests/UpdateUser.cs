using AutoMapper;
using Core.Entities;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
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
    public async Task Should_UpdateUserAndReturnActionResult_When_CallsTheMethod()
    {
        UserDto userDto = Substitute.For<UserDto>();

        IActionResult actionResult = await _userController.UpdateUser(userDto);

        Assert.IsType<CreatedAtActionResult>(actionResult);
    }
    
    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccured()
    {
        UserDto userDto = Substitute.For<UserDto>();

        User user = Substitute.For<User>();

        _userService.UpdateUser(user).ThrowsForAnyArgs<Exception>();

        IActionResult actionResult = await _userController.UpdateUser(userDto);

        Assert.IsType<ObjectResult>(actionResult);
    }
}
