using AutoMapper;
using Core.Exceptions;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using UnitTests.Infrastructure.Repositories;
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
        IActionResult actionResult = await _userController.DeleteUser(1);

        Assert.IsType<OkResult>(actionResult);
    }
    
    [Fact]
    public async Task Should_DeleteUserAndReturnActionResult_When_UserNotAvailableWithId()
    {
        _userService.DeleteUser(1).Throws<NotFoundException>();

        IActionResult actionResult = await _userController.DeleteUser(1);

        Assert.IsType<NotFoundObjectResult>(actionResult);
    }
    
    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccured()
    {
        _userService.DeleteUser(1).Throws<Exception>();

        IActionResult actionResult = await _userController.DeleteUser(1);

        Assert.IsType<ObjectResult>(actionResult);
    }
}
