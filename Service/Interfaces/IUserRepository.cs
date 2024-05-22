using Core.Domain;

namespace Infrastructure.Repositories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllUsers();

        public Task<User?> GetUserById(int bookId);

        public Task<int> AddUser(User userModel);

        public Task<int> UpdateUser(int userId, User userModel);

        public Task DeleteUser(int userId);
    }
}
