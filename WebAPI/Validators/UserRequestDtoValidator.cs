using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Validators;

public class UserRequestDtoValidator : AbstractValidator<UserRequestDto>
{
    public UserRequestDtoValidator()
    {
        RuleFor(e => e.Name)
            .NotNull()
            .NotEmpty();

        RuleFor(e => e.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();

        RuleFor(e => e.Password)
            .NotNull()
            .NotEmpty();
    }
}
