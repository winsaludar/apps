namespace Authentication.Core.Validators;

public class ResendEmailConfirmationCommandValidator : AbstractValidator<ResendEmailConfirmationCommand>
{
    public ResendEmailConfirmationCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Field 'email' is required")
            .EmailAddress().WithMessage("Field 'email' should be a valid email address");
    }
}
