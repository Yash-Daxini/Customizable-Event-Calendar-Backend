using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Validators;

public class CollaborationRequestDtoValidator : AbstractValidator<CollaborationRequestDto>
{
    public CollaborationRequestDtoValidator()
    {
        RuleFor(e => e.EventId)
            .GreaterThan(0);

        RuleFor(e => e.EventDate)
            .NotEmpty()
            .WithMessage("Enter valid event date");
    }
}
