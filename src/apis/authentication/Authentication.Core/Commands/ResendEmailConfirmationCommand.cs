﻿namespace Authentication.Core.Commands;

public class ResendEmailConfirmationCommandHandler(
    IUserRepository userRepository, 
    ITokenRepository tokenRepository, 
    IEmailService emailService) : IRequestHandler<ResendEmailConfirmationCommand, Unit>
{
    public async Task<Unit> Handle(ResendEmailConfirmationCommand request, CancellationToken cancellationToken)
    {
        // Make sure email is registered
        User? existingUser = await userRepository.GetByEmailAsync(request.Email)
            ?? throw new BadRequestException("Email does not exist");

        // Make sure email is not yet verified
        if (existingUser.IsEmailConfirmed)
            throw new BadRequestException("Email is already verified");

        // Generate token
        string? confirmationToken = await tokenRepository.GenerateEmailConfirmationTokenAsync(existingUser) 
            ?? throw new TokenException("Unable to generate email confirmation link");

        // Send email
        string encodedToken = Uri.EscapeDataString(confirmationToken);
        await emailService.SendEmailConfirmation(existingUser.Email, encodedToken, existingUser.Username);

        return Unit.Value;
    }
}

public record ResendEmailConfirmationCommand(string Email) : IRequest<Unit>;
