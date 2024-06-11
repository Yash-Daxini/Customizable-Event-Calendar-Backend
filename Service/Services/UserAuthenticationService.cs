using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;

namespace Core.Services;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly IUserRepository _userRepository;

    private readonly IMultipleInviteesEventService _multipleInviteesEventService;

    public UserAuthenticationService(IUserRepository userRepository,
                                     IMultipleInviteesEventService multipleInviteesEventService)
    {
        _userRepository = userRepository;
        _multipleInviteesEventService = multipleInviteesEventService;
    }

    public async Task<AuthenticateResponse?> Authenticate(User user)
    {
        if (user is null)
            throw new NullArgumentException($"User can't be null");

        await _userRepository.GetUserById(user.Id);

        AuthenticateResponse? loggedInUser = await _userRepository.AuthenticateUser(user)
                                   ?? throw new AuthenticationFailedException("Invalid user name or password!");

        await ScheduleProposedEventsForLoggedInUser(loggedInUser.Id);

        return loggedInUser;
    }

    private async Task ScheduleProposedEventsForLoggedInUser(int userId) //TODO: Work on this service
    {
        await _multipleInviteesEventService.StartSchedulingProcessOfProposedEvent(userId);
    }
}
