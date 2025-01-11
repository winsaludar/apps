namespace Authentication.Core.Commands;

public class ResetPasswordCommandHandler(IUserRepository userRepository) : IRequestHandler<ResetPasswordCommand, Unit>
{
    public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        // Make sure email exist
        User? existingUser = await userRepository.GetByEmailAsync(request.Email)
            ?? throw new BadRequestException("Email does not exist");

        // Make sure email is verified
        if (!existingUser.IsEmailConfirmed)
            throw new BadRequestException("Email is not yet verified");

        // Make sure password conforms with our requirement
        bool isPasswordValid = await userRepository.ValidateRegisterPasswordAsync(request.Password);
        if (!isPasswordValid)
            throw new BadRequestException("Password do not meet our minimum requirements");

        // Update password
        string decodedToken = Uri.UnescapeDataString(request.Token);
        bool isSuccessful = await userRepository.ResetPasswordAsync(existingUser.Email, request.Password, decodedToken);
        if (!isSuccessful)
            throw new BadRequestException("Token is invalid");

        return Unit.Value;
    }
}

public record ResetPasswordCommand(string Email, string Password, string RetypePassword, string Token) : IRequest<Unit>;
