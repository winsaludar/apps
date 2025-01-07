namespace Authentication.Core.Validators;

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Field 'email' is required")
            .EmailAddress().WithMessage("Field 'email' should be a valid email address");
    }
}
