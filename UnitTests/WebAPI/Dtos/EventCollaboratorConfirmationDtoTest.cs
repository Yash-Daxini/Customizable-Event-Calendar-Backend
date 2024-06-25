using FluentAssertions;
using WebAPI.Dtos;
using WebAPI.Validators;

namespace UnitTests.WebAPI.Dtos;

public class EventCollaboratorConfirmationDtoTest
{
    EventCollaboratorConfirmationDtoValidator _eventCollaboratorConfirmationDtoValidator;

    public EventCollaboratorConfirmationDtoTest()
    {
        _eventCollaboratorConfirmationDtoValidator = new();
    }

    [Fact]
    public void Should_ReturnFalse_When_InvalidEventCollaboratorConfirmationDto()
    {
        EventCollaboratorConfirmationDto eventCollaboratorConfirmationDto = new()
        {
            Id = 1
        };

        var result = _eventCollaboratorConfirmationDtoValidator.Validate(eventCollaboratorConfirmationDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnFalse_When_EventIdLessThanZero()
    {
        EventCollaboratorConfirmationDto eventCollaboratorConfirmationDto = new()
        {
            Id = 1,
            EventId = -1,
            UserId = 1,
            ConfirmationStatus = "Accept",
            ProposedDuration = null,    
        };

        var result = _eventCollaboratorConfirmationDtoValidator.Validate(eventCollaboratorConfirmationDto);

        result.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData("accept")]
    [InlineData("reject")]
    [InlineData("maybe")]
    [InlineData("proposed")]
    [InlineData("fewv")]
    [InlineData("pending")]
    [InlineData("sdad")]
    [InlineData("")]
    [InlineData(null)]
    public void Should_ReturnFalse_When_InValidConfirmationStatus(string confirmationStatus)
    {
        EventCollaboratorConfirmationDto eventCollaboratorConfirmationDto = new()
        {
            Id = -1,
            EventId = 1,
            UserId = 1,
            ConfirmationStatus = confirmationStatus,
            ProposedDuration = null,
        };

        var result = _eventCollaboratorConfirmationDtoValidator.Validate(eventCollaboratorConfirmationDto);

        result.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData("Accept")]
    [InlineData("Reject")]
    [InlineData("Maybe")]
    [InlineData("Pending")]
    public void Should_ReturnTrue_When_ValidConfirmationStatus(string confirmationStatus)
    {
        EventCollaboratorConfirmationDto eventCollaboratorConfirmationDto = new()
        {
            Id = 1,
            EventId = 1,
            UserId = 1,
            ConfirmationStatus = confirmationStatus,
            ProposedDuration = null,
        };

        var result = _eventCollaboratorConfirmationDtoValidator.Validate(eventCollaboratorConfirmationDto);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("accept")]
    [InlineData("reject")]
    [InlineData("maybe")]
    [InlineData("pending")]
    public void Should_ReturnFalse_When_ProposedDurationNotNullWhenConfirmationStatusIsNotProposed(string confirmationStatus)
    {
        EventCollaboratorConfirmationDto eventCollaboratorConfirmationDto = new()
        {
            Id = -1,
            EventId = 1,
            UserId = 1,
            ConfirmationStatus = confirmationStatus,
            ProposedDuration = new DurationDto()
            {
                StartHour = 0,
                EndHour = 1,
            },
        };

        var result = _eventCollaboratorConfirmationDtoValidator.Validate(eventCollaboratorConfirmationDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnFalse_When_ProposedDurationNullWhenConfirmationStatusIsProposed()
    {
        EventCollaboratorConfirmationDto eventCollaboratorConfirmationDto = new()
        {
            Id = -1,
            EventId = 1,
            UserId = 1,
            ConfirmationStatus = "Proposed",
            ProposedDuration = null,
        };

        var result = _eventCollaboratorConfirmationDtoValidator.Validate(eventCollaboratorConfirmationDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnTrue_When_ValidEventCollaboratorConfirmationDto()
    {
        EventCollaboratorConfirmationDto eventCollaboratorConfirmationDto = new()
        {
            Id = 1,
            EventId = 1,
            UserId = 1,
            ProposedDuration = null,
            ConfirmationStatus = "Accept"
        };

        var result = _eventCollaboratorConfirmationDtoValidator.Validate(eventCollaboratorConfirmationDto);

        result.IsValid.Should().BeTrue();
    }
}
