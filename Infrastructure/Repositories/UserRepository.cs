using Infrastructure.DataModels;
using Infrastructure.DomainEntities;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextEvent _dbContextEvent;

        private readonly UserMapper _userMapper = new();

        public UserRepository(DbContextEvent dbContextEvent)
        {
            _dbContextEvent = dbContextEvent;
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _dbContextEvent.Users.Select(user => _userMapper.MapUserEntityToModel(user))
                                              .ToListAsync();
        }

        public async Task<UserModel?> GetUserById(int bookId)
        {
            return await _dbContextEvent.Users.Where(book => book.Id == bookId)
                                                .Select(user => _userMapper.MapUserEntityToModel(user))
                                                .FirstOrDefaultAsync();
        }

        public async Task<int> AddUser(UserModel userModel)
        {
            User user = _userMapper.MapUserModelToEntity(userModel);

            _dbContextEvent.Users.Add(_userMapper.MapUserModelToEntity(userModel));

            await _dbContextEvent.SaveChangesAsync();

            return user.Id;
        }

        public async Task<int> UpdateUser(int userId, UserModel userModel)
        {
            User user = _userMapper.MapUserModelToEntity(userModel);

            _dbContextEvent.Users.Update(user);

            await _dbContextEvent.SaveChangesAsync();

            return user.Id;
        }

        public async Task DeleteUser(int userId)
        {
            User user = new()
            {
                Id = userId,
            };

            _dbContextEvent.Remove(user);

            await _dbContextEvent.SaveChangesAsync();
        }
    }
}
