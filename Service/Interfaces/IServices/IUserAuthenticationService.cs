using Core.Domain.Models;

namespace Core.Interfaces.IServices;

public interface IUserAuthenticationService
{
    public Task Authenticate(User user);
}
