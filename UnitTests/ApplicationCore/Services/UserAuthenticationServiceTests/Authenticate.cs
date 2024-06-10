using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NullArgumentException = Core.Exceptions.NullArgumentException;

namespace UnitTests.ApplicationCore.Services.UserAuthenticationServiceTests;

public class Authenticate
{

    private readonly IMultipleInviteesEventService _multipleInviteesEventService;
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly IUserAuthenticationService _userAuthenticationService;

    public Authenticate()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _multipleInviteesEventService = Substitute.For<IMultipleInviteesEventService>();
        _userService = new UserService(_userRepository);
        _userAuthenticationService = new UserAuthenticationService(_userService, _multipleInviteesEventService);
    }

    [Fact]
    public async Task Should_LogIn_When_ValidUser()
    {
        User user = new()
        {
            Id = 1,
            Name = "Test",
            Email = "Test@gmail.com",
            Password = "password",
        };

        _userRepository.AuthenticateUser(user).Returns(user);

        _userService.GetUserById(1).Returns(user);

        _userService.AuthenticateUser(user).Returns(user);

        await _userAuthenticationService.Authenticate(user);

        await _userRepository.Received().AuthenticateUser(user);

        await _multipleInviteesEventService.Received().StartSchedulingProcessOfProposedEvent(1);
    }

    [Fact]
    public async Task Should_ThrowException_When_InValidUser()
    {
        User user = new()
        {
            Id = 1,
            Name = "Test",
            Email = "Test@gmail.com",
            Password = "password",
        };

        _userService.GetUserById(1).Returns(user);

        _userService.AuthenticateUser(user).ReturnsNull();

        await Assert.ThrowsAsync<AuthenticationFailedException>(async () => await _userAuthenticationService.Authenticate(user));

        await _userRepository.Received().AuthenticateUser(user);
    }

    [Fact]
    public async Task Should_ThrowException_When_UserIsNull()
    {
        User user = null;

        _userService.GetUserById(1).Returns(user);

        _userService.AuthenticateUser(new User()).ReturnsNull();

        await Assert.ThrowsAsync<NullArgumentException>(async () => await _userAuthenticationService.Authenticate(user));

        await _userRepository.DidNotReceive().AuthenticateUser(user);
    }
}
