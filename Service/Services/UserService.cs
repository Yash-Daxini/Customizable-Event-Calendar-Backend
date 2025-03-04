﻿using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;

namespace Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> GetUsersForInvite(int userId)
    {
        return await _userRepository.GetUsersForInvite(userId);
    }

    public async Task<User> GetUserById(int userId)
    {
        User? user = await _userRepository.GetUserById(userId);

        return user == null
               ? throw new NotFoundException($"User with id {userId} not found.")
               : user;
    }

    public async Task<IdentityResult> SignUp(User user)
    {
        if (user is null)
            throw new NullArgumentException($" User can't be null");

        return await _userRepository.SignUp(user);
    }

    public async Task<IdentityResult> UpdateUser(User user)
    {
        if (user is null)
            throw new NullArgumentException($" User can't be null");

        User? userById = await _userRepository.GetUserById(user.Id);

        if (userById is null)
            throw new NotFoundException($"User with id {user.Id} not found.");

        return await _userRepository.Update(user);
    }

    public async Task<IdentityResult> DeleteUser(int userId)
    {
        User? user = await _userRepository.GetUserById(userId);

        if (user is null)
            throw new NotFoundException($"User with id {userId} not found.");

        return await _userRepository.Delete(user);
    }
}
