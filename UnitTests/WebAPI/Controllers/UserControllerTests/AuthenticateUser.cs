using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IServices;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;
using WebAPI.Dtos;

namespace UnitTests.WebAPI.Controllers.UserControllerTests;

public class AuthenticateUser : IClassFixture<AutoMapperFixture>
{
    private readonly IUserService _userService;
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly IMapper _mapper;
    private readonly UserController _userController;

    public AuthenticateUser(AutoMapperFixture autoMapperFixture)
    {
        _userService = Substitute.For<IUserService>();
        _userAuthenticationService = Substitute.For<IUserAuthenticationService>();
        _mapper = autoMapperFixture.Mapper;
        _userController = new UserController(_userService, _userAuthenticationService, _mapper);
    }

    [Fact]
    public async Task Should_ReturnAuthenticationResponse_When_UserWithValidCredentials()
    {
        User user = new()
        {
            Id = 1,
            Name = "a",
            Email = "b",
            Password = "c",
        };

        AuthenticateResponse authenticateResponse = new(user, "c");

        AuthenticateRequestDto authenticateRequestDto = new()
        {
            Name = "a",
            Password = "c",
        };

        _userAuthenticationService.LogIn(user).ReturnsForAnyArgs(authenticateResponse);

        IActionResult actionResult = await _userController.LogIn(authenticateRequestDto);

        actionResult.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Should_ReturnNotFoundResponse_When_UserNotAvailable()
    {
        User user = new()
        {
            Id = 1,
            Name = "a",
            Email = "b",
            Password = "c",
        };

        AuthenticateRequestDto authenticateRequestDto = new()
        {
            Name = "a",
            Password = "c",
        };

        _userAuthenticationService.LogIn(user).ThrowsForAnyArgs<NotFoundException>();

        IActionResult actionResult = await _userController.LogIn(authenticateRequestDto);

        actionResult.Should().BeOfType<NotFoundObjectResult>();
    }
    
    
    [Fact]
    public async Task Should_ReturnAuthenticationFailedResponse_When_UserWithInvalidCredentials()
    {
        User user = new()
        {
            Id = 1,
            Name = "a",
            Email = "b",
            Password = "c",
        };

        AuthenticateRequestDto authenticateRequestDto = new()
        {
            Name = "a",
            Password = "c",
        };

        _userAuthenticationService.LogIn(user).ThrowsForAnyArgs<AuthenticationFailedException>();

        IActionResult actionResult = await _userController.LogIn(authenticateRequestDto);

        actionResult.Should().BeOfType<BadRequestObjectResult>();
    }
    
    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccurred()
    {
        User user = new()
        {
            Id = 1,
            Name = "a",
            Email = "b",
            Password = "c",
        };

        AuthenticateRequestDto authenticateRequestDto = new()
        {
            Name = "a",
            Password = "c",
        };

        _userAuthenticationService.LogIn(user).ThrowsForAnyArgs<Exception>();

        IActionResult actionResult = await _userController.LogIn(authenticateRequestDto);

        actionResult.Should().BeOfType<ObjectResult>();
    }
}
