using Infrastructure.DataModels;
using Core.Domain;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextEventCalendar _dbContextEvent;

        private readonly UserMapper _userMapper = new();

        public UserRepository(DbContextEventCalendar dbContextEvent)
        {
            _dbContextEvent = dbContextEvent;
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _dbContextEvent.Users.Select(user => _userMapper.MapUserEntityToModel(user))
                                              .ToListAsync();
        }

        public UserModel? GetUserById(int userId)
        {
            return _dbContextEvent.Users.Where(user => user.Id == userId)
                                                .Select(user => _userMapper.MapUserEntityToModel(user))
                                                .FirstOrDefault();
        }

        public async Task<int> AddUser(UserModel userModel)
        {
            UserDataModel user = _userMapper.MapUserModelToEntity(userModel);

            _dbContextEvent.Users.Add(user);

            await _dbContextEvent.SaveChangesAsync();

            return user.Id;
        }

        public async Task<int> UpdateUser(int userId, UserModel userModel)
        {
            UserDataModel user = _userMapper.MapUserModelToEntity(userModel);

            user.Id = userId;

            _dbContextEvent.Users.Update(user);

            await _dbContextEvent.SaveChangesAsync();

            return user.Id;
        }

        public async Task DeleteUser(int userId)
        {
            UserDataModel user = new()
            {
                Id = userId,
            };

            _dbContextEvent.Remove(user);

            await _dbContextEvent.SaveChangesAsync();
        }
    }
}
