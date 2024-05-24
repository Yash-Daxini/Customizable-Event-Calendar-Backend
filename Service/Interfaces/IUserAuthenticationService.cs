using Core.Domain;

namespace Core.Interfaces;

public interface IUserAuthenticationService
{
    public Task<bool> Authenticate(User user);
}
