using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Validators;

public class AuthenticateRequestDtoValidator : AbstractValidator<AuthenticateRequestDto>
{
    public AuthenticateRequestDtoValidator()
    {
        RuleFor(e => e.Name)
            .NotNull()
            .NotEmpty();

        RuleFor(e => e.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();

        RuleFor(e => e.Password)
            .NotEmpty()
            .NotNull();
    }
}
