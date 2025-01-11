namespace Authentication.Core.Commands;

public class RegisterUserCommandHandler(
    IUserRepository userRepository, 
    ITokenRepository tokenRepository,
    IEmailService emailService) : IRequestHandler<RegisterUserCommand, User>
{
    public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Make sure email and username are unique
        User? existingUser;        
        existingUser = await userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
            throw new BadRequestException("Email already exist");
        existingUser = await userRepository.GetByUsernameAsync(request.Username);
        if (existingUser != null)
            throw new BadRequestException("Username already exist");

        // Make sure password conforms with our requirement
        bool isPasswordValid = await userRepository.ValidateRegisterPasswordAsync(request.Password);
        if (!isPasswordValid)
            throw new BadRequestException("Password do not meet our minimum requirements");

        User newUser = User.Create(request.Username, request.Email);
        Guid newId = await userRepository.RegisterAsync(newUser, request.Password);
        newUser.SetId(newId);

        // Send email confirmation link
        string? confirmationToken = await tokenRepository.GenerateEmailConfirmationTokenAsync(newUser)
            ?? throw new TokenException("Unable to generate email confirmation link");

        string encodedToken = Uri.EscapeDataString(confirmationToken);
        await emailService.SendEmailConfirmation(newUser.Email, encodedToken, newUser.Username);

        return newUser;
    }
}

public record RegisterUserCommand(string Username, string Email, string Password, string RetypePassword) : IRequest<User>;