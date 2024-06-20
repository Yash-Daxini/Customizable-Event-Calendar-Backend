using FluentAssertions;
using WebAPI.Dtos;
using WebAPI.Validators;

namespace UnitTests.WebAPI.Dtos;

public class RecurringEventRequestDtoTests
{
    RecurringEventRequestDtoValidator _validator;

    public RecurringEventRequestDtoTests()
    {
        _validator = new();
    }

    [Fact]
    public void Should_ReturnFalse_When_InvalidRecurringEventRequestDto()
    {
        RecurringEventRequestDto recurringEventRequestDto = new();

        var result = _validator.Validate(recurringEventRequestDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnTrue_When_ValidRecurringEventRequestDto()
    {
        RecurringEventRequestDto recurringEventRequestDto = new()
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
            RecurrencePattern = new()
            {
                StartDate = new DateOnly(2024, 1, 1),
                EndDate = new DateOnly(2024, 2, 2),
                Frequency = "None",
                Interval = 1,
                ByMonth = null,
                ByMonthDay = null,
                ByWeekDay = null,
                WeekOrder = null,
            },
            EventCollaborators = [new() {
                Id = 1,
                UserId = 1,
                EventCollaboratorRole = "Organizer",
                ConfirmationStatus = "Accept"
            }]
        };

        var result = _validator.Validate(recurringEventRequestDto);

        result.IsValid.Should().BeTrue();
    }
}
