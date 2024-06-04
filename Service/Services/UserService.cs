using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;

namespace Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserById(int userId)
    {
        User? user = await _userRepository.GetUserById(userId);

        return user == null
               ? throw new NotFoundException($"User with id {userId} not found.")
               : user;
    }

    public async Task<int> AddUser(User userModel)
    {
        return await _userRepository.Add(userModel);
    }

    public async Task UpdateUser(User userModel)
    {
        await GetUserById(userModel.Id);
        await _userRepository.Update(userModel);
    }

    public async Task DeleteUser(int userId)
    {
        User? user = await _userRepository.GetUserById(userId);

        if (user is null)
            throw new NotFoundException($"User with id {userId} not found.");

        await _userRepository.Delete(user);
    }

    public async Task<User?> AuthenticateUser(User user)
    {
        return await _userRepository.AuthenticateUser(user);
    }
}
