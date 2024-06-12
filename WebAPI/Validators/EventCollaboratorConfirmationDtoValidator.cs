using Core.Entities.Enums;
using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Validators;

public class EventCollaboratorConfirmationDtoValidator : AbstractValidator<EventCollaboratorConfirmationDto>
{
    public EventCollaboratorConfirmationDtoValidator()
    {
        RuleFor(e => e.EventId)
            .GreaterThan(0);

        RuleFor(e => e.ConfirmationStatus)
            .NotEmpty()
            .NotNull()
            .IsEnumName(typeof(ConfirmationStatus));

        When(e => e.ConfirmationStatus is not null && !e.ConfirmationStatus.Equals("Proposed"), () =>
        {
            RuleFor(e => e.ProposedDuration)
                .Null()
                .WithMessage("Only propose time when confirmation status is Proposed");
        });

        When(x => x.ProposedDuration != null, () =>
        {
            RuleFor(e => e.ProposedDuration)
                 .SetValidator(new DurationDtoValidator());
        });
    }
}
