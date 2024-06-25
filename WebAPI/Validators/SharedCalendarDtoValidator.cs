using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Validators
{
    public class SharedCalendarDtoValidator : AbstractValidator<SharedCalendarDto>
    {
        public SharedCalendarDtoValidator()
        {
            RuleFor(e => e.SenderUserId)
                .GreaterThanOrEqualTo(0);

            RuleFor(e => e.ReceiverUserId)
                .GreaterThanOrEqualTo(0);

            RuleFor(e => e.FromDate)
                .NotNull()
                .NotEmpty();
            
            RuleFor(e => e.ToDate)
                .NotNull()
                .NotEmpty();

            RuleFor(e => e)
                .Must(e => e.FromDate <= e.ToDate);
        }
    }
}
