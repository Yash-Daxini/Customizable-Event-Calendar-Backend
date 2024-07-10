using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;

namespace Core.Services;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly IUserRepository _userRepository;

    private readonly IMultipleInviteesEventService _multipleInviteesEventService;

    private readonly ITokenClaimService _tokenClaimService;

    public UserAuthenticationService(IUserRepository userRepository,
                                     IMultipleInviteesEventService multipleInviteesEventService,
                                     ITokenClaimService tokenClaimService)
    {
        _userRepository = userRepository;
        _multipleInviteesEventService = multipleInviteesEventService;
        _tokenClaimService = tokenClaimService;
    }

    public async Task<AuthenticateResponse?> LogIn(User user)
    {
        if (user is null)
            throw new NullArgumentException($"User can't be null");

        var result = await _userRepository.LogIn(user);

        User? loggedInUser = await _userRepository.GetUserByUserName(user.Name);  

        if (result == SignInResult.Failed)
            throw new AuthenticationFailedException("Invalid user name or password!");

        await ScheduleProposedEventsForLoggedInUser(loggedInUser.Id);

        string token = await _tokenClaimService.GetJWToken(loggedInUser);

        return new AuthenticateResponse(loggedInUser, token);
    }

    private async Task ScheduleProposedEventsForLoggedInUser(int userId)
    {
        await _multipleInviteesEventService.StartSchedulingProcessOfProposedEvent(userId);
    }
}
