using Core.Entities;
using Core.Interfaces.IServices;

namespace UnitTests.ApplicationCore.Services.TokenClaimServiceTests;

public class GetJWTToken
{
    private readonly ITokenClaimService _tokenClaimService;

    public GetJWTToken(ITokenClaimService tokenClaimService)
    {
        _tokenClaimService = tokenClaimService;
    }

    //[Fact]
    public async Task Should_ReturnJWTToken_When_NotNullUser()
    {
        User user = new ()
        {
            Id = 1,
            Name = "Test",
            Email = "Test@gmail.com"
        };

        string token = await _tokenClaimService.GetJWToken(user);

        Assert.NotEmpty(token);

        Assert.NotNull(token);
    }
}
