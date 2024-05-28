using Core.Domain;

namespace Core.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllUsers();

        public Task<User?> GetUserById(int bookId);

        public Task<int> AddUser(User userModel);

        public Task UpdateUser(User userModel);

        public Task DeleteUser(User user);

        public Task<User?> AuthenticateUser(User user);
    }
}
