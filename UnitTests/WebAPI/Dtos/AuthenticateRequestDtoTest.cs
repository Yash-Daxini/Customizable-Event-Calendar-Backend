using FluentAssertions;
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
    public void Should_Return_False_When_InvalidAuthenticateRequestDto()
    {
        AuthenticateRequestDto authenticateRequestDto = new()
        {
            Name = "",
            Password = ""
        };

        var result = _authenticateRequestDtoValidation.Validate(authenticateRequestDto);

        result.IsValid.Should().BeFalse();

        result.Errors.Count.Should().NotBe(0);
    }

    [Fact]
    public void Should_Return_False_When_NameIsEmpty()
    {
        AuthenticateRequestDto authenticateRequestDto = new()
        {
            Name = "",
            Password = "fadr"
        };

        var result = _authenticateRequestDtoValidation.Validate(authenticateRequestDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_PasswordIsEmpty()
    {
        AuthenticateRequestDto authenticateRequestDto = new()
        {
            Name = "fdsfs",
            Password = ""
        };

        var result = _authenticateRequestDtoValidation.Validate(authenticateRequestDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_NameIsNull()
    {
        AuthenticateRequestDto authenticateRequestDto = new()
        {
            Name = null,
            Password = "fadr"
        };

        var result = _authenticateRequestDtoValidation.Validate(authenticateRequestDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_PasswordIsNull()
    {
        AuthenticateRequestDto authenticateRequestDto = new()
        {
            Name = "fdsfs",
            Password = null
        };

        var result = _authenticateRequestDtoValidation.Validate(authenticateRequestDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_True_When_ValidAuthenticateRequestDto()
    {
        AuthenticateRequestDto authenticateRequestDto = new()
        {
            Name = "a",
            Password = "c",
        };

        var result = _authenticateRequestDtoValidation.Validate(authenticateRequestDto);

        result.IsValid.Should().BeTrue();
    }
}
