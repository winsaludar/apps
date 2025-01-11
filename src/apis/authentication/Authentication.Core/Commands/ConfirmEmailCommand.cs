namespace Authentication.Core.Commands;

public class ConfirmEmailCommandHandler(IUserRepository userRepository) : IRequestHandler<ConfirmEmailCommand, Unit>
{
    public async Task<Unit> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        // Make sure email is registed
        User? existingUser = await userRepository.GetByEmailAsync(request.Email)
            ?? throw new BadRequestException("Email does not exist");

        // Make sure email is not yet verified
        if (existingUser.IsEmailConfirmed)
            throw new BadRequestException("Email is already verified");

        // This will verify the token
        string decodedToken = Uri.UnescapeDataString(request.Token);
        bool isSuccessful = await userRepository.ConfirmEmailAsync(existingUser.Email, decodedToken);
        if (!isSuccessful)
            throw new BadRequestException("Token is invalid");

        return Unit.Value;
    }
}

public record ConfirmEmailCommand(string Email, string Token) : IRequest<Unit>;
