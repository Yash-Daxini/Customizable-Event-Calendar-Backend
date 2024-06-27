using Core.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Core.Entities;
using Core.Interfaces.IServices;
using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using Core.Services;

namespace UnitTests.ApplicationCore.Services.TokenClaimServiceTests;

public class GetJWTToken
{
    private readonly ITokenClaimService _tokenClaimService;

    public GetJWTToken()
    {
        _tokenClaimService = new TokenClaimService();
    }

    [Fact]
    public async Task Should_Return_JWTToken_When_UserIsNotNull()
    {
        User user = new ()
        {
            Id = 1,
            Name = "Test",
            Email = "Test@gmail.com"
        };

        string token = await _tokenClaimService.GetJWToken(user);

        // Assert
        token.Should().NotBeNullOrEmpty();

        var tokenHandler = new JwtSecurityTokenHandler();
        tokenHandler.CanReadToken(token).Should().BeTrue();

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthorizationConstants.JWT_SECRET_KEY))
        };

        SecurityToken validatedToken;
        tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

        validatedToken.Should().NotBeNull();
    }
}
