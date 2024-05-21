using Core.Domain;

namespace Core.Interfaces;

public interface IUserService
{
    public Task<List<UserModel>> GetAllUsers();

    public UserModel? GetUserById(int userId);

    public Task<int> AddUser(UserModel userModel);

    public Task<int> UpdateUser(int userId, UserModel userModel);

    public Task DeleteUser(int userId);
}
