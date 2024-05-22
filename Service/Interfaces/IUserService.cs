using Core.Domain;

namespace Core.Interfaces;

public interface IUserService
{
    public Task<List<User>> GetAllUsers();

    public Task<User?> GetUserById(int userId);

    public Task<int> AddUser(User userModel);

    public Task<int> UpdateUser(int userId, User userModel);

    public Task DeleteUser(int userId);
}
