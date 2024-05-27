using Core.Domain;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<User?> GetUserById(int userId)
        {
            return await _userRepository.GetUserById(userId);
        }

        public Task<int> AddUser(User userModel)
        {
            return _userRepository.AddUser(userModel);
        }

        public Task<int> UpdateUser(User userModel)
        {
            return _userRepository.UpdateUser(userModel);
        }

        public Task DeleteUser(int userId)
        {
            return _userRepository.DeleteUser(userId);
        }
    }
}
