using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        public Task<User?> GetUserById(int userId);

        public Task<User?> GetUserByUserName(string userName);

        public Task<IdentityResult> SignUp(User user);

        public Task<SignInResult> LogIn(User user);

        public Task<IdentityResult> Update(User user);

        public Task<IdentityResult> Delete(User user);
    }
}
