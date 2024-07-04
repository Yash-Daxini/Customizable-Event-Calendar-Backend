using FluentAssertions;
using WebAPI.Dtos;
using WebAPI.Validators;

namespace UnitTests.WebAPI.Dtos;

public class UserDtoTests
{
    private readonly UserRequestDtoValidator _validator;

    public UserDtoTests()
    {
        _validator = new();
    }

    [Fact]
    public void Should_Return_False_When_InvalidUserDto()
    {
        UserRequestDto userDto = new();

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_NameIsEmpty()
    {
        UserRequestDto userDto = new()
        {
            Name = "",
            Email = "test@gmail.com",
            Password = "Password"
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_NameIsNull()
    {
        UserRequestDto userDto = new()
        {
            Name = null,
            Email = "test@gmail.com",
            Password = "Password"
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_EmailIsEmpty()
    {
        UserRequestDto userDto = new()
        {
            Name = "fdfs",
            Email = "",
            Password = "Password"
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_EmailIsNull()
    {
        UserRequestDto userDto = new()
        {
            Name = "ddfsaf",
            Email = null,
            Password = "Password"
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_EmailIsNotValid()
    {
        UserRequestDto userDto = new()
        {
            Name = "ddfsaf",
            Email = "ksfjlksdj j",
            Password = "Password"
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_PasswordIsEmpty()
    {
        UserRequestDto userDto = new()
        {
            Name = "fdfs",
            Email = "a@gmail.com",
            Password = ""
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_PasswordIsNull()
    {
        UserRequestDto userDto = new()
        {
            Name = "ddfsaf",
            Email = "a@gmail.com",
            Password = null
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_True_When_ValidUserDto()
    {
        UserRequestDto userDto = new()
        {
            Id = 1,
            Name = "Test",
            Email = "Test@gmail.com",
            Password = "Password"
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeTrue();
    }
}
