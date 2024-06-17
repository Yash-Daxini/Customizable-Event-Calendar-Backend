using Core.Entities;

namespace Core.Interfaces.IServices;

public interface ITokenClaimService
{
    public Task<string> GetJWToken(User user);
}
