using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Validators;

public class NonRecurringEventRequestDtoValidator : AbstractValidator<NonRecurringEventRequestDto>
{
    public NonRecurringEventRequestDtoValidator()
    {
        RuleFor(e => e.Title)
            .NotEmpty()
            .NotNull()
            .WithMessage("Title of event is not null and not empty.");

        RuleFor(e => e.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Description of event is not null and not empty.");

        RuleFor(e => e.Location)
            .NotEmpty()
            .NotNull()
            .WithMessage("Location of event is not null and not empty.");

        RuleFor(e => e.Duration)
            .SetValidator(new DurationDtoValidator());

        RuleFor(e => e.EndDate)
            .NotNull().DependentRules(() =>
            {
                RuleFor(e => e.StartDate)
                  .NotEmpty()
                  .GreaterThanOrEqualTo(e => e.EndDate);
            });

        RuleForEach(e => e.EventCollaborators)
            .SetValidator(new EventCollaboratorRequestDtoValidator());
    }
}
