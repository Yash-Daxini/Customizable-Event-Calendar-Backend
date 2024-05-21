using Core.Domain;

namespace Infrastructure.Repositories
{
    public interface IUserRepository
    {
        public Task<List<UserModel>> GetAllUsers();

        public UserModel? GetUserById(int bookId);

        public Task<int> AddUser(UserModel userModel);

        public Task<int> UpdateUser(int userId, UserModel userModel);

        public Task DeleteUser(int userId);
    }
}
