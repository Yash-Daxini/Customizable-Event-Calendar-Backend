using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Validators;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
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
            //.MinimumLength(8);
    }
}
