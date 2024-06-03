using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IServices;

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
        await _userService.GetUserById(user.Id);

        User? loggedInUser = await _userService.AuthenticateUser(user) 
                                   ?? throw new AuthenticationFailedException("Invalid user name or password!");

        await ScheduleProposedEventsForLoggedInUser(loggedInUser.Id);
    }

    private async Task ScheduleProposedEventsForLoggedInUser(int userId) //TODO: Work on this service
    {
        await _multipleInviteesEventService.StartSchedulingProcessOfProposedEvent(userId);
    }
}
