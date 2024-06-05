using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using ArgumentNullException = Core.Exceptions.ArgumentNullException;

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
        if (userId is <= 0)
            throw new ArgumentException($"Invalid user id");

        User? user = await _userRepository.GetUserById(userId);

        return user == null
               ? throw new NotFoundException($"User with id {userId} not found.")
               : user;
    }

    public async Task<int> AddUser(User user)
    {
        if (user is null)
            throw new ArgumentNullException($" Event collaborator can't be null");

        return await _userRepository.Add(user);
    }

    public async Task UpdateUser(User user)
    {
        if (user is null)
            throw new ArgumentNullException($" Event collaborator can't be null");

        await GetUserById(user.Id);
        await _userRepository.Update(user);
    }

    public async Task DeleteUser(int userId)
    {
        if (userId is <= 0)
            throw new ArgumentException($"Invalid event id");

        User? user = await _userRepository.GetUserById(userId);

        if (user is null)
            throw new NotFoundException($"User with id {userId} not found.");

        await _userRepository.Delete(user);
    }

    public async Task<User?> AuthenticateUser(User user)
    {
        if (user is null)
            throw new ArgumentNullException($" Event collaborator can't be null");

        return await _userRepository.AuthenticateUser(user);
    }
}
