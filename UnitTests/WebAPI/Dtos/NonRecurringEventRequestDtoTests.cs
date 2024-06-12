using WebAPI.Dtos;
using WebAPI.Validators;

namespace UnitTests.WebAPI.Dtos;

public class NonRecurringEventRequestDtoTests
{

    private readonly NonRecurringEventRequestDtoValidator _validator;
    public NonRecurringEventRequestDtoTests()
    {
        _validator = new();
    }

    [Fact]
    public void Should_ReturnFalse_When_InvalidNonRecurringEventRequestDto()
    {
        NonRecurringEventRequestDto nonRecurringEventRequestDto = new();

        var result = _validator.Validate(nonRecurringEventRequestDto);

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_ReturnTrue_When_ValidNonRecurringEventRequestDto()
    {
        NonRecurringEventRequestDto nonRecurringEventRequestDto = new()
        {
            Id = 1,
            Title = "Title",
            Description = "Description",
            Location = "Location",
            Duration = new()
            {
                StartHour = 1,
                EndHour = 2,
            },
            StartDate = new DateOnly(2024,2,1),
            EndDate = new DateOnly(2024,2,2),
            EventCollaborators = [new() {
                Id = 1,
                UserId = 1,
                EventCollaboratorRole = "Organizer",
                ConfirmationStatus = "Accept"
            }]
        };

        var result = _validator.Validate(nonRecurringEventRequestDto);

        Assert.True(result.IsValid);
    }
}
