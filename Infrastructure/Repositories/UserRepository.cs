using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Core.Interfaces.IRepositories;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Core.Interfaces.IServices;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    private readonly UserManager<UserDataModel> _userManager;

    private readonly SignInManager<UserDataModel> _signInManager;

    public UserRepository(DbContextEventCalendar dbContextEvent, IMapper mapper, UserManager<UserDataModel> userManager, SignInManager<UserDataModel> signInManager)
    {
        _dbContext = dbContextEvent;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<User?> GetUserById(int userId)
    {
        return _mapper.Map<User>(await _dbContext.Users
                               .Where(user => user.Id == userId)
                               .FirstOrDefaultAsync());
    }

    public async Task<IdentityResult> Update(User user)
    {
        UserDataModel? userDataModel = await _userManager.FindByIdAsync(user.Id + "");

        if (userDataModel is null)
            return IdentityResult.Failed();

        userDataModel.UserName = user.Name;
        userDataModel.Email = user.Email;

        return await _userManager.UpdateAsync(userDataModel);
    }

    public async Task<IdentityResult> Delete(User user)
    {
        UserDataModel? userDataModel = await _userManager.FindByIdAsync(user.Id + "");

        return await _userManager.DeleteAsync(userDataModel);
    }

    public async Task<IdentityResult> SignUp(User user)
    {
        UserDataModel userDataModel = _mapper.Map<UserDataModel>(user);

        return await _userManager.CreateAsync(userDataModel, user.Password);
    }

    public async Task<SignInResult> LogIn(User user)
    {
        return await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, false);
    }
}
