using Infrastructure.DataModels;
using AutoMapper;
using Core.Interfaces.IRepositories;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMapper _mapper;

    private readonly UserManager<UserDataModel> _userManager;

    private readonly SignInManager<UserDataModel> _signInManager;

    public UserRepository(IMapper mapper, UserManager<UserDataModel> userManager, SignInManager<UserDataModel> signInManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<User?> GetUserById(int userId)
    {
        return _mapper.Map<User>(await _userManager
                                       .FindByIdAsync(userId.ToString()));
    }

    public async Task<IdentityResult> Update(User user)
    {
        UserDataModel? userDataModel = await _userManager
                                            .FindByIdAsync(user.Id.ToString());

        if (userDataModel is null)
            return IdentityResult.Failed();

        userDataModel.UserName = user.Name;
        userDataModel.Email = user.Email;

        return await _userManager.UpdateAsync(userDataModel);
    }

    public async Task<IdentityResult> Delete(User user)
    {
        UserDataModel? userDataModel = await _userManager
                                             .FindByIdAsync(user.Id.ToString());

        return await _userManager.DeleteAsync(userDataModel);
    }

    public async Task<IdentityResult> SignUp(User user)
    {
        UserDataModel userDataModel = _mapper.Map<UserDataModel>(user);

        return await _userManager.CreateAsync(userDataModel, user.Password);
    }

    public async Task<SignInResult> LogIn(User user)
    {
        return await _signInManager.PasswordSignInAsync(user.Name, 
                                                        user.Password,
                                                        false,
                                                        false);
    }
}
