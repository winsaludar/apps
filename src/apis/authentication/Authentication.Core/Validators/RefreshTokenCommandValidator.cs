namespace Authentication.Core.Validators;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Field 'token' is required");

        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Field 'refreshToken' is required");
    }
}
