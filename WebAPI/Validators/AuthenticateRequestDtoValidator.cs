using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Validators;

public class AuthenticateRequestDtoValidator : AbstractValidator<AuthenticateRequestDto>
{
    public AuthenticateRequestDtoValidator()
    {
        RuleFor(e => e.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Enter valid name");

        RuleFor(e => e.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage("Enter not null and not empty password");
    }
}
