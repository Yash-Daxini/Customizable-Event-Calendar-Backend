using WebAPI.Dtos;
using WebAPI.Validators;

namespace UnitTests.WebAPI.Dtos;

public class DurationDtoTest
{
    private readonly DurationDtoValidator _durationDtoValidator;

    public DurationDtoTest()
    {
        _durationDtoValidator = new();
    }

    [Theory]
    [InlineData(-1,-1)]
    [InlineData(1,0)]
    [InlineData(-1,0)]
    [InlineData(23,24)]
    public void Should_ReturnFalse_When_InvalidDuration(int startHour,int endHour)
    {
        DurationDto durationDto = new DurationDto()
        {
            StartHour = startHour,
            EndHour = endHour
        };

        bool result = _durationDtoValidator.Validate(durationDto).IsValid;

        Assert.False(result);
    }
    
    [Theory]
    [InlineData(11,12)]
    [InlineData(0,1)]
    [InlineData(22,23)]
    public void Should_ReturnTrue_When_ValidDuration(int startHour,int endHour)
    {
        DurationDto durationDto = new ()
        {
            StartHour = startHour,
            EndHour = endHour
        };

        var result = _durationDtoValidator.Validate(durationDto);

        Assert.True(result.IsValid);
    }
}
