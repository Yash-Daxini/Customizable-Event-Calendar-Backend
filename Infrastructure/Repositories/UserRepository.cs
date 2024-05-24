using Infrastructure.DataModels;
using Core.Domain;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextEventCalendar _dbContextEvent;

        private readonly IMapper _mapper;

        public UserRepository(DbContextEventCalendar dbContextEvent, IMapper mapper)
        {
            _dbContextEvent = dbContextEvent;
            _mapper = mapper;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _dbContextEvent
                        .Users
                        .Select(user => _mapper.Map<User>(user))
                        .ToListAsync();
        }

        public async Task<User?> GetUserById(int userId)
        {
            return await _dbContextEvent
                   .Users
                   .Where(user => user.Id == userId)
                   .Select(user => _mapper.Map<User>(user))
                   .FirstOrDefaultAsync();
        }

        public async Task<int> AddUser(User userModel)
        {
            UserDataModel user = _mapper.Map<UserDataModel>(userModel);

            _dbContextEvent.Users.Add(user);

            await _dbContextEvent.SaveChangesAsync();

            return user.Id;
        }

        public async Task<int> UpdateUser(int userId, User userModel)
        {
            UserDataModel user = _mapper.Map<UserDataModel>(userModel);

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

        public async Task<User?> AuthenticateUser(User user)
        {
            UserDataModel? userDataModel = await _dbContextEvent
                                          .Users
                                          .FirstOrDefaultAsync(userObj => userObj.Name == user.Name
                                                                       && userObj.Password == user.Password);

            if (userDataModel is null) return null;
            return _mapper.Map<User>(userDataModel);
        }
    }
}
