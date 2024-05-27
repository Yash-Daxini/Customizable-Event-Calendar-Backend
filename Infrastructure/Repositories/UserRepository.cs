using Infrastructure.DataModels;
using Core.Domain;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Core.Interfaces.IRepositories;
using AutoMapper.QueryableExtensions;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextEventCalendar _dbContext;

        private readonly IMapper _mapper;

        public UserRepository(DbContextEventCalendar dbContextEvent, IMapper mapper)
        {
            _dbContext = dbContextEvent;
            _mapper = mapper;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _dbContext
                        .Users
                        .ProjectTo<User>(_mapper.ConfigurationProvider)
                        .ToListAsync();
        }

        public async Task<User?> GetUserById(int userId)
        {
            return await _dbContext
                   .Users
                   .Where(user => user.Id == userId)
                   .ProjectTo<User>(_mapper.ConfigurationProvider)
                   .FirstOrDefaultAsync();
        }

        public async Task<int> AddUser(User userModel)
        {
            UserDataModel user = _mapper.Map<UserDataModel>(userModel);

            _dbContext.Users.Add(user);

            await _dbContext.SaveChangesAsync();

            return user.Id;
        }

        public async Task<int> UpdateUser(User userModel)
        {
            UserDataModel user = _mapper.Map<UserDataModel>(userModel);

            _dbContext.Users.Update(user);

            await _dbContext.SaveChangesAsync();

            return user.Id;
        }

        public async Task DeleteUser(int userId)
        {
            UserDataModel user = new()
            {
                Id = userId,
            };

            _dbContext.Remove(user);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> AuthenticateUser(User user)
        {
            UserDataModel? userDataModel = await _dbContext
                                          .Users
                                          .FirstOrDefaultAsync(userObj => userObj.Name == user.Name
                                                                       && userObj.Password == user.Password);

            if (userDataModel is null) return null;
            return _mapper.Map<User>(userDataModel);
        }
    }
}
