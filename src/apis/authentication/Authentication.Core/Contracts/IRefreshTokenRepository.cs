namespace Authentication.Core.Contracts;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByUserIdAsync(string userId);
    Task<RefreshToken?> GetByOldRefreshTokenAsync(string token);
    Task CreateAsync(RefreshToken refreshToken);
    Task RemoveByUserIdAsync(string userId);
}
