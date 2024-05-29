using Core.Domain.Models;

namespace Core.Interfaces.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User?> GetUserById(int userId);

        public Task<User?> AuthenticateUser(User user);
    }
}
