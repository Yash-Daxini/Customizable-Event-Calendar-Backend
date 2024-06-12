using WebAPI.Dtos;
using WebAPI.Validators;

namespace UnitTests.WebAPI.Dtos;

public class AuthenticateRequestDtoTest
{
    private readonly AuthenticateRequestDtoValidator _authenticateRequestDtoValidation;
    public AuthenticateRequestDtoTest()
    {
        _authenticateRequestDtoValidation = new AuthenticateRequestDtoValidator();
    }

    [Fact]
    public void Should_ReturnFalse_When_InvalidAuthenticateReqeustDto()
    {
        AuthenticateRequestDto authenticateRequestDto = new()
        {
            Id = 1
        };

        var result = _authenticateRequestDtoValidation.Validate(authenticateRequestDto);

        Assert.False(result.IsValid);

        Assert.True(result.Errors.Count != 0);
    }

    [Fact]
    public void Should_ReturnTrue_When_ValidAuthenticateReqeustDto()
    {
        AuthenticateRequestDto authenticateRequestDto = new()
        {
            Id = 1,
            Name = "a",
            Email = "b@gmail.com",    
            Password = "c", 
        };

        var result = _authenticateRequestDtoValidation.Validate(authenticateRequestDto);

        Assert.True(result.IsValid);
    }
}
