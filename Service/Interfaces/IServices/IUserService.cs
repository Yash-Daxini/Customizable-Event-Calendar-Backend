using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces.IServices;

public interface IUserService
{
    public Task<User> GetUserById(int userId);

    public Task<IdentityResult> SignUp(User user);

    public Task<IdentityResult> UpdateUser(User user);

    public Task<IdentityResult> DeleteUser(int userId);
}
