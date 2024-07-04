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
            .WithMessage("Title of event should not null and not empty.");

        RuleFor(e => e.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Description of event should not null and not empty.");

        RuleFor(e => e.Location)
            .NotEmpty()
            .NotNull()
            .WithMessage("Location of event should not null and not empty.");

        RuleFor(e => e.Duration)
            .SetValidator(new DurationDtoValidator());

        RuleFor(e => e.EventDate)
            .NotEmpty()
            .WithMessage("Event date should be in valid format");

        RuleForEach(e => e.EventCollaborators)
            .SetValidator(new EventCollaboratorRequestDtoValidator());
    }
}
