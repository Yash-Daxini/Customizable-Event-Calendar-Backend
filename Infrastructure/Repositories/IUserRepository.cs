using Infrastructure.DomainEntities;

namespace Infrastructure.Repositories
{
    public interface IUserRepository
    {
        public Task<List<UserModel>> GetAllUsers();

        public Task<UserModel?> GetUserById(int bookId);

        public Task<int> AddUser(UserModel userModel);

        public Task<int> UpdateUser(int userId, UserModel userModel);

        public Task DeleteUser(int userId);
    }
}
