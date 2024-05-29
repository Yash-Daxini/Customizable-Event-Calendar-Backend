using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Validators;

public class DurationDtoValidator : AbstractValidator<DurationDto>
{
    public DurationDtoValidator()
    {

        RuleFor(e => e.EndHour)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(23)
            .NotEmpty().DependentRules(() =>
            {
                RuleFor(e => e.StartHour)
                    .GreaterThanOrEqualTo(0)
                    .LessThanOrEqualTo(23)
                    .LessThan(e => e.EndHour)
                    .NotEmpty();
            });
    }
}
