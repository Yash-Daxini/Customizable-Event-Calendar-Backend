using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;
using WebAPI.Dtos;

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
    public async Task Should_ReturnActionResultNotFound_When_UserWithIdNotAvailable()
    {
        User user = Substitute.For<User>();

        _userService.GetUserById(1).ThrowsForAnyArgs<NotFoundException>();

        IActionResult actionResult = await _userController.GetUserById(1);

        Assert.IsType<NotFoundObjectResult>(actionResult);  
    }
    
    [Fact]
    public async Task Should_ReturnActionResultOk_When_UserWithIdAvailable()
    {
        User user = Substitute.For<User>();

        UserDto userDto = Substitute.For<UserDto>();

        _userService.GetUserById(1).ReturnsForAnyArgs(user);

        IActionResult actionResult = await _userController.GetUserById(1);

        var returedResult = Assert.IsType<OkObjectResult>(actionResult);

        Assert.Equivalent(userDto,returedResult.Value);
    }
    
    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccurred()
    {
        User user = Substitute.For<User>();

        UserDto userDto = Substitute.For<UserDto>();

        _userService.GetUserById(1).ThrowsForAnyArgs<Exception>();

        IActionResult actionResult = await _userController.GetUserById(1);

        Assert.IsType<ObjectResult>(actionResult);
    }
}
