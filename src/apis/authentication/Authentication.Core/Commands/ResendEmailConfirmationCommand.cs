namespace Authentication.Core.Commands;

public class ResendEmailConfirmationCommandHandler(
    IUserRepository userRepository, 
    ITokenRepository tokenRepository, 
    IEmailService emailService) : IRequestHandler<ResendEmailConfirmationCommand>
{
    public async Task Handle(ResendEmailConfirmationCommand request, CancellationToken cancellationToken)
    {
        // Make sure email is registered
        User? existingUser = await userRepository.GetByEmailAsync(request.Email)
            ?? throw new BadRequestException("Email does not exist");

        // Make sure email is not yet verified
        if (existingUser.IsEmailConfirmed)
            throw new BadRequestException("Email is already verified");

        // Generate token (1 day validity)
        Token token = await tokenRepository.GenerateEmailConfirmationTokenAsync(existingUser)
            ?? throw new TokenException("Unable to generate email confirmation link");

        // Send email
        await emailService.SendEmailConfirmation(existingUser.Email, token.Value, existingUser.Username);

        // MediatR custom pipeline won't trigger when we don't return a response
        //return true;
    }
}

public record ResendEmailConfirmationCommand(string Email) : IRequest;
