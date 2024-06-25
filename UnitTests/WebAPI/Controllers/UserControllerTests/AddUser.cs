﻿using AutoMapper;
using Core.Entities;
using Core.Interfaces.IServices;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using UnitTests.Builders;
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

        User user = new UserBuilder(49)
                    .WithName("b")
                    .WithEmail("b@gmail.com")
                    .WithPassword("b")
                    .Build();

        _userService.SignUp(user).ReturnsForAnyArgs(IdentityResult.Success);

        IActionResult actionResult = await _userController.AddUser(userDto);

        actionResult.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Should_ReturnBadRequest_When_AddOperationFailed()
    {
        UserDto userDto = Substitute.For<UserDto>();

        User user = Substitute.For<User>();

        _userService.SignUp(user).ReturnsForAnyArgs(IdentityResult.Failed());

        IActionResult actionResult = await _userController.AddUser(userDto);

        actionResult.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccurred()
    {
        UserDto userDto = Substitute.For<UserDto>();

        User user = Substitute.For<User>();

        _userService.SignUp(user).ThrowsForAnyArgs<Exception>();

        IActionResult actionResult = await _userController.AddUser(userDto);

        actionResult.Should().BeOfType<ObjectResult>();
    }
}
