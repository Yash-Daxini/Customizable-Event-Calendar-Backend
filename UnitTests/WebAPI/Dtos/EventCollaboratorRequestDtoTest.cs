using FluentAssertions;
using WebAPI.Dtos;
using WebAPI.Validators;

namespace UnitTests.WebAPI.Dtos;

public class EventCollaboratorRequestDtoTest
{
    private readonly EventCollaboratorRequestDtoValidator _eventCollaboratorRequestDtoValidator;

    public EventCollaboratorRequestDtoTest()
    {
        _eventCollaboratorRequestDtoValidator = new();
    }

    [Fact]
    public void Should_ReturnFalse_When_InvalidEventCollaboratorRequestDtoValidator()
    {
        EventCollaboratorRequestDto eventCollaboratorRequestDto = new(); 

        var result = _eventCollaboratorRequestDtoValidator.Validate(eventCollaboratorRequestDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnFalse_When_UserIdLessThanZero()
    {
        EventCollaboratorRequestDto eventCollaboratorRequestDto = new()
        {
            Id = 3,
            UserId = -1,
            EventCollaboratorRole = "Organizer",
            ConfirmationStatus = "Accept"
        };

        var result = _eventCollaboratorRequestDtoValidator.Validate(eventCollaboratorRequestDto);

        result.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData("organizer")]
    [InlineData("participant")]
    [InlineData("collaborator")]
    [InlineData("ddfd")]
    [InlineData("")]
    [InlineData(null)]
    public void Should_ReturnFalse_When_InValidEventCollaboratorRole(string eventCollaboratorRole)
    {
        EventCollaboratorRequestDto eventCollaboratorRequestDto = new()
        {
            Id = 3,
            UserId = 1,
            EventCollaboratorRole = eventCollaboratorRole,
            ConfirmationStatus = "Accept"
        };

        var result = _eventCollaboratorRequestDtoValidator.Validate(eventCollaboratorRequestDto);

        result.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData("Organizer")]
    [InlineData("Participant")]
    [InlineData("Collaborator")]
    public void Should_ReturnTrue_When_ValidEventCollaboratorRole(string eventCollaboratorRole)
    {
        EventCollaboratorRequestDto eventCollaboratorRequestDto = new()
        {
            Id = 3,
            UserId = 1,
            EventCollaboratorRole = eventCollaboratorRole,
            ConfirmationStatus = "Accept"
        };

        var result = _eventCollaboratorRequestDtoValidator.Validate(eventCollaboratorRequestDto);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("accept")]
    [InlineData("reject")]
    [InlineData("maybe")]
    [InlineData("pending")]
    [InlineData("proposed")]
    [InlineData("")]
    [InlineData(null)]
    public void Should_ReturnFalse_When_InValidConfirmation(string confirmationStatus)
    {
        EventCollaboratorRequestDto eventCollaboratorRequestDto = new()
        {
            Id = 3,
            UserId = 1,
            EventCollaboratorRole = "Organizer",
            ConfirmationStatus = confirmationStatus,
        };

        var result = _eventCollaboratorRequestDtoValidator.Validate(eventCollaboratorRequestDto);

        result.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData("Accept")]
    [InlineData("Reject")]
    [InlineData("Maybe")]
    [InlineData("Pending")]
    [InlineData("Proposed")]
    public void Should_ReturnTrue_When_ValidConfirmation(string confirmationStatus)
    {
        EventCollaboratorRequestDto eventCollaboratorRequestDto = new()
        {
            Id = 3,
            UserId = 1,
            EventCollaboratorRole = "Organizer",
            ConfirmationStatus = confirmationStatus,
        };

        var result = _eventCollaboratorRequestDtoValidator.Validate(eventCollaboratorRequestDto);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_ReturnTrue_When_ValidEventCollaboratorRequestDtoValidator()
    {
        EventCollaboratorRequestDto eventCollaboratorRequestDto = new()
        {
            Id = 1,
            UserId = 1,
            EventCollaboratorRole = "Organizer",
            ConfirmationStatus = "Accept"
        };

        var result = _eventCollaboratorRequestDtoValidator.Validate(eventCollaboratorRequestDto);

        result.IsValid.Should().BeTrue();
    }
}
