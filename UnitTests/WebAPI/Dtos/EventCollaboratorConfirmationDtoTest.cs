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
