namespace Authentication.Core.Validators;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Field 'email' is required")
           .EmailAddress().WithMessage("Field 'email' should be a valid email address");

        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Field 'token' is required");
    }
}
