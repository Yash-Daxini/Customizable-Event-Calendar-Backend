using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Validators;

public class EventRequestDtoValidator : AbstractValidator<EventRequestDto>
{
    public EventRequestDtoValidator()
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

        RuleFor(e => e.RecurrencePattern)
            .SetValidator(new RecurrencePatternDtoValidator());

        RuleForEach(e => e.EventCollaborators)
            .SetValidator(new EventCollaboratorDtoValidator());

    }
}
