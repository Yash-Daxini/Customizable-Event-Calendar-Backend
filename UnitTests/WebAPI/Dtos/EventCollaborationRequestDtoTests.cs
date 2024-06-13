using WebAPI.Dtos;
using WebAPI.Validators;

namespace UnitTests.WebAPI.Dtos;

public class EventCollaborationRequestDtoTests
{

    private readonly CollaborationRequestDtoValidator _collaborationRequestDtoValidator;

    public EventCollaborationRequestDtoTests()
    {
        _collaborationRequestDtoValidator = new CollaborationRequestDtoValidator();
    }

    [Fact]
    public void Should_ReturnFalse_When_InvalidEventCollaborationRequestDto()
    {
        CollaborationRequestDto collaborationRequestDto = new();

        var result = _collaborationRequestDtoValidator.Validate(collaborationRequestDto);

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_ReturnTrue_When_ValidEventCollaborationRequestDto()
    {
        CollaborationRequestDto collaborationRequestDto = new()
        {
            Id = 1,
            EventId = 1,
            UserId = 1,
            EventDate = new DateOnly(2024,2,1),
        };

        var result = _collaborationRequestDtoValidator.Validate(collaborationRequestDto);

        Assert.True(result.IsValid);
    }
}
