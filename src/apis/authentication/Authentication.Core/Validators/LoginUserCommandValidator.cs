namespace Authentication.Core.Validators;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Field 'email' is required")
            .EmailAddress().WithMessage("Field 'email' should be a valid email address");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Field 'password' is required");
    }
}
