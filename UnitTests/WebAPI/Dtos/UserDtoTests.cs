using WebAPI.Dtos;
using WebAPI.Validators;

namespace UnitTests.WebAPI.Dtos;

public class UserDtoTests
{
    private readonly UserDtoValidator _validator;

    public UserDtoTests()
    {
        _validator = new();
    }

    [Fact]
    public void Should_ReturnFalse_When_InvalidUserDto()
    {
        UserDto userDto = new();

        var result = _validator.Validate(userDto);

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_ReturnTrue_When_ValidUserDto()
    {
        UserDto userDto = new()
        {
            Id = 1,
            Name = "Test",
            Email = "Test@gmail.com",
            Password = "Test",
        };

        var result = _validator.Validate(userDto);

        Assert.True(result.IsValid);
    }
}
