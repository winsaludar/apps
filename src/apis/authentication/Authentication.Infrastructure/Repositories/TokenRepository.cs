﻿namespace Authentication.Infrastructure.Repositories;

public class TokenRepository(
    TokenValidationParameters tokenValidationParameters,
    IRefreshTokenRepository refreshTokenRepository,
    JwtSettings jwtSettings,
    EmailSettings emailConfirmationSettings) : ITokenRepository
{
    public async Task<Token> GenerateJwtAsync(User user, Core.Models.RefreshToken? refreshToken = null)
    {
        IEnumerable<Claim> authClaims = GetAuthClaims(user);   
        JwtSecurityToken token = GetJwtSecurityToken(jwtSettings.Secret, jwtSettings.Issuer, jwtSettings.Audience, jwtSettings.ExpirationInMinutes, authClaims);

        // Generate access token
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        // Refresh existing token when requested
        if (refreshToken != null)
            return Token.Create(jwtToken, refreshToken.Token, token.ValidTo);

        // Create refresh token when login
        string rtToken = $"{Guid.NewGuid()}-{Guid.NewGuid()}";
        Core.Models.RefreshToken newRefreshToken = Core.Models.RefreshToken.Create(rtToken, token.Id, false, user.Id.ToString(), DateTime.UtcNow, DateTime.UtcNow.AddMonths(jwtSettings.RefreshTokenExpirationInMonths), null);
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

    public async Task<Token> GenerateEmailConfirmationTokenAsync(User user)
    {
        return await Task.Run(() => 
        {
            IEnumerable<Claim> authClaims = GetAuthClaims(user);
            JwtSecurityToken token = GetJwtSecurityToken(jwtSettings.Secret, jwtSettings.Issuer, jwtSettings.Audience, emailConfirmationSettings.EmailConfirmationExpirationInMinutes, authClaims);

            // Generate access token
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Token.Create(jwtToken, "", token.ValidTo);
        });
    }

    private static IEnumerable<Claim> GetAuthClaims(User user)
    {
        // TODO: Add user role claims
        return 
        [
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Sub, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        ];
    }

    private static JwtSecurityToken GetJwtSecurityToken(string secret, string issuer, string audience, int expirationInMinutes, IEnumerable<Claim> authClaims)
    {
        SymmetricSecurityKey authSigningKey = new(Encoding.ASCII.GetBytes(secret));

        return new(
            issuer: issuer,
            audience: audience,
            expires: DateTime.UtcNow.AddMinutes(expirationInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
    }
}
