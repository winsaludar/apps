namespace Authentication.Core.Commands;

public class ForgotPasswordCommandHandler(
    IUserRepository userRepository, 
    ITokenRepository tokenRepository,
    IEmailService emailService) : IRequestHandler<ForgotPasswordCommand, Unit>
{
    public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        // Make sure email exist
        User? existingUser = await userRepository.GetByEmailAsync(request.Email)
            ?? throw new BadRequestException("Email does not exist");

        // Make sure email is verified
        if (!existingUser.IsEmailConfirmed)
            throw new BadRequestException("Email is not yet verified");

        // Generate token
        string? resetToken = await tokenRepository.GeneratePasswordResetTokenAsync(existingUser);
        if (string.IsNullOrEmpty(resetToken))
            throw new TokenException("Unable to generate forgot password link");

        // Send email
        string encodedToken = Uri.EscapeDataString(resetToken);
        await emailService.SendForgotPasswordUrl(existingUser.Email, encodedToken, existingUser.Username);

        return Unit.Value;
    }
}

public record ForgotPasswordCommand(string Email) : IRequest<Unit>;
