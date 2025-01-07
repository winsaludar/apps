using Microsoft.IdentityModel.Tokens;

namespace Authentication.Infrastructure.Repositories;

public class TokenRepository(
    TokenValidationParameters tokenValidationParameters, 
    IConfiguration configuration, 
    IRefreshTokenRepository refreshTokenRepository) : ITokenRepository
{
    public async Task<Token> GenerateJwtAsync(User user, Core.Models.RefreshToken? refreshToken = null)
    {
        // Configure claims that will be added in the token
        List<Claim> authClaims =
        [
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Sub, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        ];

        // TODO: Add user role claims

        // Configure access token properties
        _ = int.TryParse(configuration["JWT:ExpirationInMinutes"], out int expiration);
        SymmetricSecurityKey authSigningKey = new(Encoding.ASCII.GetBytes(configuration["JWT:Secret"] ?? ""));
        JwtSecurityToken token = new(
            issuer: configuration["JWT:Issuer"],
            audience: configuration["JWT:Audience"],
            expires: DateTime.UtcNow.AddMinutes(expiration),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

        // Generate access token
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        // Refresh existing token when requested
        if (refreshToken != null)
            return Token.Create(jwtToken, refreshToken.Token, token.ValidTo);

        // Create refresh token when login
        _ = int.TryParse(configuration["JWT:RefreshTokenExpirationInMonths"], out int refreshTokenExpiration);
        string rtToken = $"{Guid.NewGuid()}-{Guid.NewGuid()}";
        Core.Models.RefreshToken newRefreshToken = Core.Models.RefreshToken.Create(rtToken, token.Id, false, user.Id.ToString(), DateTime.UtcNow, DateTime.UtcNow.AddMonths(refreshTokenExpiration), null);
        await refreshTokenRepository.CreateAsync(newRefreshToken);

        return Token.Create(jwtToken, newRefreshToken.Token, token.ValidTo);
    }

    public async Task<Token> RefreshJwtAsync(Token oldToken, User user, Core.Models.RefreshToken refreshToken)
    {
        JwtSecurityTokenHandler jwtTokenHandler = new();

        // Handle expired token
        try
        {
            var tokenCheckResult = jwtTokenHandler.ValidateToken(oldToken.Value, tokenValidationParameters, out var validatedToken);
            return await GenerateJwtAsync(user, refreshToken);
        }
        catch (SecurityTokenExpiredException)
        {
            if (refreshToken?.DateExpire >= DateTime.UtcNow)
                return await GenerateJwtAsync(user, refreshToken);
            else
                return await GenerateJwtAsync(user);
        }
    }
}
