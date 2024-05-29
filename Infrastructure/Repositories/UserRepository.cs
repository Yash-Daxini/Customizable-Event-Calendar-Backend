using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Core.Interfaces.IRepositories;
using AutoMapper.QueryableExtensions;
using Core.Domain.Models;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User,UserDataModel>,IUserRepository
{
    private readonly DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    public UserRepository(DbContextEventCalendar dbContextEvent, IMapper mapper) : base(dbContextEvent, mapper)
    {
        _dbContext = dbContextEvent;
        _mapper = mapper;
    }

    public async Task<User?> GetUserById(int userId)
    {
        return await _dbContext.Users
                               .Where(user => user.Id == userId)
                               .ProjectTo<User>(_mapper.ConfigurationProvider)
                               .FirstOrDefaultAsync();
    }

    public async Task<User?> AuthenticateUser(User user) //Extra
    {
        UserDataModel? userDataModel = await _dbContext
                                      .Users
                                      .FirstOrDefaultAsync(userObj => userObj.Name == user.Name
                                                                   && userObj.Password == user.Password);

        return _mapper.Map<User>(userDataModel);
    }
}
