using Core.Domain.Enums;
using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Validators;

public class EventCollaboratorResponseDtoValidator : AbstractValidator<EventCollaboratorResponseDto>
{
    public EventCollaboratorResponseDtoValidator()
    {
        RuleFor(e => e.EventId)
            .GreaterThan(0);

        RuleFor(e => e.User)
            .NotNull()
            .SetValidator(new UserDtoValidator());


        RuleFor(e => e.ParticipantRole)
            .NotEmpty()
            .NotNull()
            .IsEnumName(typeof(ParticipantRole));

        RuleFor(e => e.ConfirmationStatus)
            .NotEmpty()
            .NotNull()
            .IsEnumName(typeof(ConfirmationStatus));

        When(x => x.ProposedDuration != null, () =>
        {
            RuleFor(e => e.ProposedDuration)
                 .SetValidator(new DurationDtoValidator());
        });

        RuleFor(e => e.EventDate)
            .NotEmpty();
    }
}
