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

        await _userRepository.GetUserById(user.Id);

        var result = await _userRepository.LogIn(user);

        if (result == SignInResult.Failed)
            throw new AuthenticationFailedException("Invalid user name or password!");

        await ScheduleProposedEventsForLoggedInUser(user.Id);

        string token = await _tokenClaimService.GetJWToken(user);

        return new AuthenticateResponse(user, token);
    }

    private async Task ScheduleProposedEventsForLoggedInUser(int userId)
    {
        await _multipleInviteesEventService.StartSchedulingProcessOfProposedEvent(userId);
    }
}
