namespace Authentication.Core.Validators;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Field 'username' is required");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Field 'email' is required")
            .EmailAddress().WithMessage("Field 'email' should be a valid email address");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Field 'password' is required")
            .MinimumLength(8).WithMessage("Field 'password' should be at least 8 characters long");

        RuleFor(x => x.RetypePassword)
            .NotEmpty().WithMessage("Field 'retypePassword' is required")
            .Equal(x => x.Password).WithMessage("Field 'password' and 'retypePassword' do not match");
    }
}
