using Core.Entities;

namespace Core.Interfaces.IServices;

public interface IUserAuthenticationService
{
    public Task<AuthenticateResponse?> Authenticate(User user);
}
