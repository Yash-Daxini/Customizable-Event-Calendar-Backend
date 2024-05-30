using Core.Domain.Enums;
using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Validators;

public class EventCollaborationRequestDtoValidator : AbstractValidator<EventCollaborationRequestDto>
{
    public EventCollaborationRequestDtoValidator()
    {
        RuleFor(e => e.EventId)
            .GreaterThan(0);

        RuleFor(e => e.User)
            .NotNull()
            .SetValidator(new UserDtoValidator());


        RuleFor(e => e.ParticipantRole)
            .NotEmpty()
            .NotNull()
            .IsEnumName(typeof(EventCollaboratorRole));

        RuleFor(e => e.ConfirmationStatus)
            .NotEmpty()
            .NotNull()
            .IsEnumName(typeof(ConfirmationStatus));

        RuleFor(e => e.EventDate)
            .NotEmpty();
    }
}
