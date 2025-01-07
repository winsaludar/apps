
using Authentication.Core.Models;

namespace Authentication.Infrastructure.Repositories;

public class RefreshTokenRepository(AuthenticationDbContext dbContext) : IRefreshTokenRepository
{
    public async Task<Core.Models.RefreshToken?> GetByUserIdAsync(string userId)
    {
        Entities.RefreshToken? dbRefreshToken = await dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == userId && x.DateExpire > DateTime.UtcNow);
        if (dbRefreshToken is null)
            return null;

        return Core.Models.RefreshToken.Create(
            dbRefreshToken.Token,
            dbRefreshToken.JwtId,
            dbRefreshToken.IsRevoked,
            dbRefreshToken.UserId,
            dbRefreshToken.DateAdded,
            dbRefreshToken.DateExpire,
            dbRefreshToken.Id);
    }

    public async Task<Core.Models.RefreshToken?> GetByOldRefreshTokenAsync(string token)
    {
        Entities.RefreshToken? dbRefreshToken = await dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token);
        if (dbRefreshToken is null)
            return null;

        return Core.Models.RefreshToken.Create(
            dbRefreshToken.Token, 
            dbRefreshToken.JwtId, 
            dbRefreshToken.IsRevoked, 
            dbRefreshToken.UserId, 
            dbRefreshToken.DateAdded, 
            dbRefreshToken.DateExpire, 
            dbRefreshToken.Id);
    }

    public async Task CreateAsync(Core.Models.RefreshToken refreshToken)
    {
        Entities.RefreshToken newToken = new()
        {
            Id = Guid.NewGuid(),
            Token = refreshToken.Token,
            JwtId = refreshToken.JwtId,
            UserId = refreshToken.UserId,
            DateAdded = refreshToken.DateAdded,
            DateExpire = refreshToken.DateExpire,
            IsRevoked = refreshToken.IsRevoked,
        };

        await dbContext.RefreshTokens.AddAsync(newToken);
        await dbContext.SaveChangesAsync();
    }    

    public async Task RemoveByUserIdAsync(string userId)
    {
        Entities.RefreshToken? dbRefreshToken = await dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == userId);
        if (dbRefreshToken is null)
            return;

        dbContext.RefreshTokens.Remove(dbRefreshToken);
        await dbContext.SaveChangesAsync();
    }
}
