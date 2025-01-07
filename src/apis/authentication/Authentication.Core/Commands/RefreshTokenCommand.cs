namespace Authentication.Core.Commands;

public class RefreshTokenCommandHandler(
    IUserRepository userRepository, 
    ITokenRepository tokenRepository,
    IRefreshTokenRepository refreshTokenRepository) : IRequestHandler<RefreshTokenCommand, (User, Token)>
{
    public async Task<(User, Token)> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // Validate refresh token
        RefreshToken? existingRefreshToken = await refreshTokenRepository.GetByOldRefreshTokenAsync(request.RefreshToken) 
            ?? throw new BadRequestException("Invalid refresh token");

        // Make sure refresh token isn't revoked yet
        if (existingRefreshToken.IsRevoked)
            throw new BadRequestException("Invalid refresh token");

        // Make sure user still exist
        User? existingUser = await userRepository.GetByUserIdAsync(existingRefreshToken.UserId)
            ?? throw new BadRequestException("Invalid refresh token");

        Token oldToken = Token.Create(request.Token, request.RefreshToken, DateTime.UtcNow);
        Token? newToken = await tokenRepository.RefreshJwtAsync(oldToken, existingUser, existingRefreshToken)
            ?? throw new TokenException("Unable to generate new token");

        return (existingUser, newToken);
    }
}


public record RefreshTokenCommand(string Token, string RefreshToken) : IRequest<(User, Token)>;
