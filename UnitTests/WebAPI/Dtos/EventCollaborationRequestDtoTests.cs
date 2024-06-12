using WebAPI.Dtos;
using WebAPI.Validators;

namespace UnitTests.WebAPI.Dtos;

public class EventCollaborationRequestDtoTests
{

    private readonly EventCollaborationRequestDtoValidator _eventCollaborationRequestDtoValidator;

    public EventCollaborationRequestDtoTests()
    {
        _eventCollaborationRequestDtoValidator = new EventCollaborationRequestDtoValidator();
    }

    [Fact]
    public void Should_ReturnFalse_When_InvalidEventCollaborationRequestDto()
    {
        EventCollaborationRequestDto eventCollaborationRequestDto = new();

        var result = _eventCollaborationRequestDtoValidator.Validate(eventCollaborationRequestDto);

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_ReturnTrue_When_ValidEventCollaborationRequestDto()
    {
        EventCollaborationRequestDto eventCollaborationRequestDto = new()
        {
            Id = 1,
            EventId = 1,
            UserId = 1,
            EventDate = new DateOnly(2024,2,1),
            EventCollaboratorRole = "Organizer",
            ConfirmationStatus = "Accept"
        };

        var result = _eventCollaborationRequestDtoValidator.Validate(eventCollaborationRequestDto);

        Assert.True(result.IsValid);
    }
}
