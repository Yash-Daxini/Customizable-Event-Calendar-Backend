using AutoMapper;
using Core.Entities;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using WebAPI.Controllers;

namespace UnitTests.WebAPI.Controllers.UserControllerTests;

public class GetUserById
{
    private readonly IUserService _userService;
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly IMapper _mapper;
    private readonly UserController _userController;

    public GetUserById()
    {
        _userService = Substitute.For<IUserService>();
        _userAuthenticationService = Substitute.For<IUserAuthenticationService>();
        _mapper = Substitute.For<IMapper>();
        _userController = new UserController(_userService, _userAuthenticationService, _mapper);
    }

    [Fact]
    public async Task Should_ReturnActionResultNotFound_When_UserWithIdNotAvailable()
    {
        IActionResult actionResult = await _userController.GetUserById(1);

        Assert.IsType<NotFoundResult>(actionResult);  
    }
    
    [Fact]
    public async Task Should_ReturnActionResultOk_When_UserWithIdAvailable()
    {
        _userService.GetUserById(1).Returns(new User());

        IActionResult actionResult = await _userController.GetUserById(1);

        Assert.IsType<OkObjectResult>(actionResult);  
    }
}
