using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IServices;
using ArgumentNullException = Core.Exceptions.ArgumentNullException;

namespace Core.Services;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly IUserService _userService;

    private readonly IMultipleInviteesEventService _multipleInviteesEventService;

    public UserAuthenticationService(IUserService userService,
                                     IMultipleInviteesEventService multipleInviteesEventService)
    {
        _userService = userService;
        _multipleInviteesEventService = multipleInviteesEventService;
    }

    public async Task Authenticate(User user)
    {
        if (user is null)
            throw new ArgumentNullException($"User can't be null");

        await _userService.GetUserById(user.Id);

        User? loggedInUser = await _userService.AuthenticateUser(user) 
                                   ?? throw new AuthenticationFailedException("Invalid user name or password!");

        await ScheduleProposedEventsForLoggedInUser(loggedInUser.Id);
    }

    private async Task ScheduleProposedEventsForLoggedInUser(int userId) //TODO: Work on this service
    {
        if (userId is <= 0)
            throw new ArgumentException($"Invalid user id");

        await _multipleInviteesEventService.StartSchedulingProcessOfProposedEvent(userId);
    }
}
