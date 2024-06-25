using FluentAssertions;
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

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnFalse_When_TitleIsEmpty()
    {
        NonRecurringEventRequestDto nonRecurringEventRequestDto = new()
        {
            Title = "",
            Description = "dfsa",
            Location = "fsdf",
            Duration = new DurationDto()
            {
                StartHour = 1,
                EndHour = 2,
            },
            EventDate = new DateOnly(2024, 2, 1),
            EventCollaborators = []
        };

        var result = _validator.Validate(nonRecurringEventRequestDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnFalse_When_TitleIsNull()
    {
        NonRecurringEventRequestDto nonRecurringEventRequestDto = new()
        {
            Title = null,
            Description = "dfsa",
            Location = "fsdf",
            Duration = new DurationDto()
            {
                StartHour = 1,
                EndHour = 2,
            },
            EventDate = new DateOnly(2024, 2, 1),
            EventCollaborators = []
        };

        var result = _validator.Validate(nonRecurringEventRequestDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnFalse_When_DescriptionIsEmpty()
    {
        NonRecurringEventRequestDto nonRecurringEventRequestDto = new()
        {
            Title = "fsdfds",
            Description = "",
            Location = "fsdf",
            Duration = new DurationDto()
            {
                StartHour = 1,
                EndHour = 2,
            },
            EventDate = new DateOnly(2024, 2, 1),
            EventCollaborators = []
        };

        var result = _validator.Validate(nonRecurringEventRequestDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnFalse_When_DescriptionIsNull()
    {
        NonRecurringEventRequestDto nonRecurringEventRequestDto = new()
        {
            Title = "fsdfds",
            Description = null,
            Location = "fsdf",
            Duration = new DurationDto()
            {
                StartHour = 1,
                EndHour = 2,
            },
            EventDate = new DateOnly(2024, 2, 1),
            EventCollaborators = []
        };

        var result = _validator.Validate(nonRecurringEventRequestDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnFalse_When_LocationIsEmpty()
    {
        NonRecurringEventRequestDto nonRecurringEventRequestDto = new()
        {
            Title = "fsdfds",
            Description = "fdsff",
            Location = "",
            Duration = new DurationDto()
            {
                StartHour = 1,
                EndHour = 2,
            },
            EventDate = new DateOnly(2024, 2, 1),
            EventCollaborators = []
        };

        var result = _validator.Validate(nonRecurringEventRequestDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnFalse_When_LocationIsNull()
    {
        NonRecurringEventRequestDto nonRecurringEventRequestDto = new()
        {
            Title = "fsdfds",
            Description = "fdsff",
            Location = null,
            Duration = new DurationDto()
            {
                StartHour = 1,
                EndHour = 2,
            },
            EventDate = new DateOnly(2024, 2, 1),
            EventCollaborators = []
        };

        var result = _validator.Validate(nonRecurringEventRequestDto);

        result.IsValid.Should().BeFalse();
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
            EventDate = new DateOnly(2024, 2, 1),
            EventCollaborators = [new() {
                Id = 1,
                UserId = 1,
                EventCollaboratorRole = "Organizer",
                ConfirmationStatus = "Accept"
            }]
        };

        var result = _validator.Validate(nonRecurringEventRequestDto);

        result.IsValid.Should().BeTrue();
    }
}
