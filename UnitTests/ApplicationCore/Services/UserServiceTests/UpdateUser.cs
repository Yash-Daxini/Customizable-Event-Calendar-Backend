﻿using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace UnitTests.ApplicationCore.Services.UserServiceTests;

public class UpdateUser
{

    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public UpdateUser()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _userService = new UserService(_userRepository);
    }

    [Fact]
    public async Task Should_UpdateUser_When_UserWithIdAvailable()
    {
        User user = new()
        {
            Id = 1,
            Name = "Test",
            Email = "Test@gmail.com",
            Password = "password",
        };

        _userRepository.GetUserById(1).Returns(user);

        await _userService.UpdateUser(user);

        await _userRepository.Received().Update(user);
    }

    [Fact]
    public async Task Should_ThrowException_When_UserWithIdNotAvailable()
    {
        User user = new()
        {
            Id = 1,
        };

        _userRepository.GetUserById(1).ReturnsNull();

        Action action = async () => await _userService.UpdateUser(user);

        action.Should().Throw<NotFoundException>();

        await _userRepository.DidNotReceive().Update(user);
    }

    [Fact]
    public async Task Should_ThrowException_When_UserIsNull()
    {
        User user = null;

        _userRepository.GetUserById(1).ReturnsNull();

        Action action = async () => await _userService.UpdateUser(user);

        action.Should().Throw<NullArgumentException>();

        await _userRepository.DidNotReceive().Update(user);
    }
}
