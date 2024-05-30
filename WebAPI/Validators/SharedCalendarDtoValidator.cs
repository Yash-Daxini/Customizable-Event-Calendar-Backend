using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Validators
{
    public class SharedCalendarDtoValidator : AbstractValidator<SharedCalendarDto>
    {
        public SharedCalendarDtoValidator()
        {
            RuleFor(e => e.Sender)
                .NotNull()
                .SetValidator(new UserDtoValidator());

            RuleFor(e => e.Receiver)
                .NotNull()
                .SetValidator(new UserDtoValidator());

            RuleFor(e => e.FromDate)
                .NotNull()
                .NotEmpty();
            
            RuleFor(e => e.ToDate)
                .NotNull()
                .NotEmpty();
        }
    }
}
