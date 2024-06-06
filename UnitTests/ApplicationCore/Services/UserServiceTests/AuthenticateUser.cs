using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NullArgumentException = Core.Exceptions.NullArgumentException;

namespace UnitTests.ApplicationCore.Services.UserServiceTests;

public class AuthenticateUser
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public AuthenticateUser()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _userService = new UserService(_userRepository);
    }

    [Fact]
    public async Task Should_ReturnUser_When_UserWithCorrectCredentials()
    {
        User user = new()
        {
            Id = 1,
            Name = "Test",
            Email = "Test@gmail.com",
            Password = "password",
        };

        _userRepository.AuthenticateUser(user).Returns(user);

        User? authUser = await _userService.AuthenticateUser(user);

        Assert.Equal(user, authUser);

        await _userRepository.Received().AuthenticateUser(user);
    }

    [Fact]
    public async Task Should_ReturnNull_When_UserWithInCorrectCredentials()
    {
        User user = new()
        {
            Id = 1,
            Name = "Test",
            Email = "Test@gmail.com",
            Password = "password",
        };

        _userRepository.AuthenticateUser(user).ReturnsNull();

        User? authUser = await _userService.AuthenticateUser(user);

        Assert.Null(authUser);

        await _userRepository.Received().AuthenticateUser(user);
    }

    [Fact]
    public async Task Should_Throw_When_UserIsNull()
    {
        User user = null;

        _userRepository.AuthenticateUser(user).ReturnsNull();

        await Assert.ThrowsAsync<NullArgumentException>(async () => await _userService.AuthenticateUser(user));

        await _userRepository.DidNotReceive().AuthenticateUser(user);
    }
}
