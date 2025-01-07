namespace Authentication.Core.Commands;

public class LoginUserCommandHandler(IUserRepository userRepository, ITokenRepository tokenRepository) : IRequestHandler<LoginUserCommand, (User, Token)>
{
    public async Task<(User, Token)> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // Verify email
        User? existingUser = await userRepository.GetByEmailAsync(request.Email) ?? throw new BadRequestException("Invalid email or password");

        // Verify password
        bool isPasswordCorrect = await userRepository.ValidateLoginPasswordAsync(request.Email, request.Password);
        if (!isPasswordCorrect)
            throw new BadRequestException("Invalid email or password");

        // Verify if email is validated
        if (!existingUser.IsEmailConfirmed)
            throw new UnauthorizedAccessException("Email not yet confirmed");

        // Generate token
        Token token = await tokenRepository.GenerateJwtAsync(existingUser);

        return (existingUser, token);
    }
}

public record LoginUserCommand(string Email, string Password) : IRequest<(User, Token)>;
