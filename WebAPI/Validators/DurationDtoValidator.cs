using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Validators;

public class DurationDtoValidator : AbstractValidator<DurationDto>
{
    public DurationDtoValidator()
    {

        RuleFor(e => e.StartHour)
            .InclusiveBetween(0, 22)
            .WithMessage("StartHour must be between 0 and 22."); ;

        RuleFor(e => e.EndHour)
            .InclusiveBetween(1, 23)
            .WithMessage("EndHour must be between 1 and 23.");

        RuleFor(e => e)
            .Must(e => e.StartHour < e.EndHour)
            .WithMessage("StartHour must be less than EndHour.");
    }
}
