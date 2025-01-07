namespace Authentication.Core.Contracts;

public interface ITokenRepository
{
    Task<Token> GenerateJwtAsync(User user, RefreshToken? refreshToken = null);
    Task<Token> RefreshJwtAsync(Token oldToken, User user, RefreshToken refreshToken);
    Task<string?> GenerateEmailConfirmationTokenAsync(User user);
    Task<string?> GeneratePasswordResetTokenAsync(User user);
}
