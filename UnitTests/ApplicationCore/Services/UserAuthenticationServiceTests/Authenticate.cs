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
    private readonly IUserAuthenticationService _userAuthenticationService;

    public Authenticate()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _multipleInviteesEventService = Substitute.For<IMultipleInviteesEventService>();
        _userAuthenticationService = new UserAuthenticationService(_userRepository, _multipleInviteesEventService);
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

        AuthenticateResponse authenticateResponse = new (user,"auth");

        _userRepository.GetUserById(1).Returns(user);

        _userRepository.AuthenticateUser(user).Returns(authenticateResponse);

        AuthenticateResponse? auth = await _userAuthenticationService.Authenticate(user);

        await _userRepository.Received().AuthenticateUser(user);

        await _multipleInviteesEventService.Received().StartSchedulingProcessOfProposedEvent(1);

        Assert.Equivalent(authenticateResponse, auth);  
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

        _userRepository.GetUserById(1).Returns(user);

        _userRepository.AuthenticateUser(user).ReturnsNull();

        await Assert.ThrowsAsync<AuthenticationFailedException>(async () => await _userAuthenticationService.Authenticate(user));

        await _userRepository.Received().AuthenticateUser(user);
    }

    [Fact]
    public async Task Should_ThrowException_When_UserIsNull()
    {
        User user = null;

        _userRepository.GetUserById(1).Returns(user);

        _userRepository.AuthenticateUser(new User()).ReturnsNull();

        await Assert.ThrowsAsync<NullArgumentException>(async () => await _userAuthenticationService.Authenticate(user));

        await _userRepository.DidNotReceive().AuthenticateUser(user);
    }
}
