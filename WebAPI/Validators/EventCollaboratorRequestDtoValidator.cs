using Core.Entities.Enums;
using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Validators
{
    public class EventCollaboratorRequestDtoValidator : AbstractValidator<EventCollaboratorRequestDto>
    {
        public EventCollaboratorRequestDtoValidator()
        {
            RuleFor(e => e.ParticipantRole)
                .NotEmpty()
                .NotNull()
                .IsEnumName(typeof(EventCollaboratorRole));

            RuleFor(e => e.ConfirmationStatus)
                .NotEmpty()
                .NotNull()
                .IsEnumName(typeof(ConfirmationStatus));
        }
    }
}
