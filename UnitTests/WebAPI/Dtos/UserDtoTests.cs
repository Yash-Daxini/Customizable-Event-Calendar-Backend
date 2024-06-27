using System.Xml;
using FluentAssertions;
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
    public void Should_Return_False_When_InvalidUserDto()
    {
        UserDto userDto = new();

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_NameIsEmpty()
    {
        UserDto userDto = new()
        {
            Name = "",
            Email = "test@gmail.com",
            Password = "abc"
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_NameIsNull()
    {
        UserDto userDto = new()
        {
            Name = null,
            Email = "test@gmail.com",
            Password = "abc"
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_EmailIsEmpty()
    {
        UserDto userDto = new()
        {
            Name = "fdfs",
            Email = "",
            Password = "abc"
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_EmailIsNull()
    {
        UserDto userDto = new()
        {
            Name = "ddfsaf",
            Email = null,
            Password = "abc"
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_EmailIsNotValid()
    {
        UserDto userDto = new()
        {
            Name = "ddfsaf",
            Email = "ksfjlksdj j",
            Password = "abc"
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_PasswordIsEmpty()
    {
        UserDto userDto = new()
        {
            Name = "fdsf",
            Email = "test@gmail.com",
            Password = ""
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_PasswordIsNull()
    {
        UserDto userDto = new()
        {
            Name = "dfdsf",
            Email = "test@gmail.com",
            Password = null
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_True_When_ValidUserDto()
    {
        UserDto userDto = new()
        {
            Id = 1,
            Name = "Test",
            Email = "Test@gmail.com",
            Password = "Test",
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeTrue();
    }
}
