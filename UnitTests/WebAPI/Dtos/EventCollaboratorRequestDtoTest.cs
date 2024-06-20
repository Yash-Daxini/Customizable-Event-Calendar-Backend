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
