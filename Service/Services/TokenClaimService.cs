using Core.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities;
using Core.Interfaces.IServices;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services;

public class TokenClaimService : ITokenClaimService
{
    public async Task<string> GetJWToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = await Task.Run(() =>
        {

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthorizationConstants.JWT_SECRET_KEY));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("userName", user.Name.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(15),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.CreateToken(tokenDescriptor);
        });

        return tokenHandler.WriteToken(token);
    }
}
