using FluentAssertions;
using WebAPI.Dtos;
using WebAPI.Validators;

namespace UnitTests.WebAPI.Dtos;

public class CollaborationRequestDtoTests
{

    private readonly CollaborationRequestDtoValidator _collaborationRequestDtoValidator;

    public CollaborationRequestDtoTests()
    {
        _collaborationRequestDtoValidator = new CollaborationRequestDtoValidator();
    }

    [Fact]
    public void Should_Return_False_When_InvalidEventCollaborationRequestDto()
    {
        CollaborationRequestDto collaborationRequestDto = new();

        var result = _collaborationRequestDtoValidator.Validate(collaborationRequestDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_EventDateIsEmpty()
    {
        CollaborationRequestDto collaborationRequestDto = new()
        {
            Id = 5,
            EventId = 5,
            UserId = 5,
            EventDate = new DateOnly()
        };

        var result = _collaborationRequestDtoValidator.Validate(collaborationRequestDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_EventIdIsLessThanZero()
    {
        CollaborationRequestDto collaborationRequestDto = new()
        {
            Id = 5,
            EventId = -1,
            UserId = 5,
            EventDate = new DateOnly(2024, 5, 21)
        };

        var result = _collaborationRequestDtoValidator.Validate(collaborationRequestDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_True_When_ValidEventCollaborationRequestDto()
    {
        CollaborationRequestDto collaborationRequestDto = new()
        {
            Id = 1,
            EventId = 1,
            UserId = 1,
            EventDate = new DateOnly(2024, 2, 1),
        };

        var result = _collaborationRequestDtoValidator.Validate(collaborationRequestDto);

        result.IsValid.Should().BeTrue();
    }
}
