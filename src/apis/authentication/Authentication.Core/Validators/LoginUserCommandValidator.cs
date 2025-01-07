namespace Authentication.Core.Validators;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Field 'email' is required");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Field 'password' is required");
    }
}
