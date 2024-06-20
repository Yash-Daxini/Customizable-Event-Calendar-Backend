using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NSubstitute;

namespace UnitTests.ApplicationCore.Services.UserServiceTests;

public class AddUser
{

    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public AddUser()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _userService = new UserService(_userRepository);
    }

    [Fact]
    public async Task Should_ReturnUserIdAfterAddOperation_When_CallsRepositoryMethod()
    {
        User user = new()
        {
            Id = 1,
            Name = "Test",
            Email = "Test@gmail.com",
            Password = "password",
        };

        _userRepository.SignUp(user).Returns(IdentityResult.Success);

        var result = await _userService.SignUp(user);

        await _userRepository.Received().SignUp(user);

        result.Succeeded.Should().BeTrue();
    }

    [Fact]
    public async Task Should_ThrowException_When_UserIsNull()
    {
        User user = null;

        _userRepository.SignUp(user).Returns(IdentityResult.Failed());

        Action action = async () => await _userService.SignUp(user);

        action.Should().Throw<NullArgumentException>();

        await _userRepository.DidNotReceive().SignUp(user);
    }
}
