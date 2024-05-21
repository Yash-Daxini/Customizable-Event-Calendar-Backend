using Core.Interfaces;
using Core.Domain;
using Infrastructure.Repositories;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService()
        {
            
        }

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<List<UserModel>> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public UserModel? GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        public Task<int> AddUser(UserModel userModel)
        {
            return _userRepository.AddUser(userModel);
        }

        public Task<int> UpdateUser(int userId, UserModel userModel)
        {
            return _userRepository.UpdateUser(userId, userModel);
        }

        public Task DeleteUser(int userId)
        {
            return _userRepository.DeleteUser(userId);
        }
    }
}
