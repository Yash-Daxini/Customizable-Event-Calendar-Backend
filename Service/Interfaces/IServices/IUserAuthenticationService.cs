using Core.Domain;

namespace Core.Interfaces.IServices;

public interface IUserAuthenticationService
{
    public Task<bool> Authenticate(User user);
}
