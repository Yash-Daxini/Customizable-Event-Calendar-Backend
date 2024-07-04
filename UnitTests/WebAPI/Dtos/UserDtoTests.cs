using System.Xml;
using FluentAssertions;
using WebAPI.Dtos;
using WebAPI.Validators;

namespace UnitTests.WebAPI.Dtos;

public class UserDtoTests
{
    private readonly UserResponseDtoValidator _validator;

    public UserDtoTests()
    {
        _validator = new();
    }

    [Fact]
    public void Should_Return_False_When_InvalidUserDto()
    {
        UserResponseDto userDto = new();

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_NameIsEmpty()
    {
        UserResponseDto userDto = new()
        {
            Name = "",
            Email = "test@gmail.com"
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_NameIsNull()
    {
        UserResponseDto userDto = new()
        {
            Name = null,
            Email = "test@gmail.com"
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_EmailIsEmpty()
    {
        UserResponseDto userDto = new()
        {
            Name = "fdfs",
            Email = ""
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_EmailIsNull()
    {
        UserResponseDto userDto = new()
        {
            Name = "ddfsaf",
            Email = null
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_EmailIsNotValid()
    {
        UserResponseDto userDto = new()
        {
            Name = "ddfsaf",
            Email = "ksfjlksdj j"
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_True_When_ValidUserDto()
    {
        UserResponseDto userDto = new()
        {
            Id = 1,
            Name = "Test",
            Email = "Test@gmail.com"
        };

        var result = _validator.Validate(userDto);

        result.IsValid.Should().BeTrue();
    }
}
