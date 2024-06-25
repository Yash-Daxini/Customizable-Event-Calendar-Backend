using Core.Entities.Enums;
using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Validators
{
    public class EventCollaboratorRequestDtoValidator : AbstractValidator<EventCollaboratorRequestDto>
    {
        public EventCollaboratorRequestDtoValidator()
        {
            RuleFor(e => e.UserId)
                .GreaterThanOrEqualTo(0);

            RuleFor(e => e.EventCollaboratorRole)
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
