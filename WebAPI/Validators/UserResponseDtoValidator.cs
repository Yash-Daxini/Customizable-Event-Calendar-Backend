using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Validators;

public class UserResponseDtoValidator : AbstractValidator<UserResponseDto>
{
    public UserResponseDtoValidator()
    {
        RuleFor(e => e.Name)
            .NotNull()
            .NotEmpty();

        RuleFor(e => e.Email)
            .NotNull()    
            .NotEmpty()
            .EmailAddress();
    }
}
