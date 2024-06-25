using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Services.UserAuthenticationServiceTests;

public class LogIn
{

    private readonly IMultipleInviteesEventService _multipleInviteesEventService;
    private readonly IUserRepository _userRepository;
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly ITokenClaimService _tokenClaimService;

    public LogIn()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _multipleInviteesEventService = Substitute.For<IMultipleInviteesEventService>();
        _tokenClaimService = Substitute.For<ITokenClaimService>();  
        _userAuthenticationService = new UserAuthenticationService(_userRepository, _multipleInviteesEventService,_tokenClaimService);
    }

    [Fact]
    public async Task Should_LogIn_When_ValidUser()
    {
        User user = new UserBuilder(1)
                    .WithId(1)
                    .WithName("Test")
                    .WithEmail("Test@gmail.com")
                    .WithPassword("password")
                    .Build();

        AuthenticateResponse authenticateResponse = new (user,"auth");

        _userRepository.GetUserById(1).Returns(user);

        _userRepository.LogIn(user).Returns(SignInResult.Success);

        _tokenClaimService.GetJWToken(user).Returns("auth");

        AuthenticateResponse? auth = await _userAuthenticationService.LogIn(user);

        await _userRepository.Received().LogIn(user);

        await _multipleInviteesEventService.Received().StartSchedulingProcessOfProposedEvent(1);

        auth.Should().BeEquivalentTo(authenticateResponse);  
    }

    [Fact]
    public async Task Should_ThrowException_When_InValidUser()
    {
        User user = new UserBuilder(1)
                    .WithName("Test")
                    .WithEmail("Test@gmail.com")
                    .WithPassword("password")
                    .Build();

        _userRepository.GetUserById(1).Returns(user);

        _userRepository.LogIn(user).Returns(SignInResult.Failed);

        var action = async () => await _userAuthenticationService.LogIn(user);

        await action.Should().ThrowAsync<AuthenticationFailedException>();

        await _userRepository.Received().LogIn(user);
    }

    [Fact]
    public async Task Should_ThrowException_When_UserIsNull()
    {
        User user = null;

        _userRepository.GetUserById(1).Returns(user);

        _userRepository.LogIn(new User()).ReturnsNull();

        var action = async () => await _userAuthenticationService.LogIn(user);

        await action.Should().ThrowAsync<NullArgumentException>();

        await _userRepository.DidNotReceive().LogIn(user);
    }
}
