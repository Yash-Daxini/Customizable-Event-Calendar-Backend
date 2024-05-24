﻿using Core.Domain;
using Core.Interfaces;
using Infrastructure.Repositories;

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

    public async Task<bool> Authenticate(User user)
    {
        User? loggedInUser = await _userRepository.AuthenticateUser(user);

        if (loggedInUser is not null)
        {
            //ScheduleProposedEventsForLoggedInUser();
        }

        return loggedInUser != null;
    }

    private async void ScheduleProposedEventsForLoggedInUser()
    {
        await _multipleInviteesEventService.StartSchedulingProcessOfProposedEvent();
    }
}
