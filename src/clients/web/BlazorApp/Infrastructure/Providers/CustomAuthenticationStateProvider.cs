namespace BlazorApp.Infrastructure.Providers;

public class CustomAuthenticationStateProvider(
    ApplicationSettings appSettings,
    ILocalStorageService localStorageService) : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string? token = await localStorageService.GetItemAsync<string>(appSettings.TokenName);
        ClaimsIdentity identity = string.IsNullOrEmpty(token) || IsTokenExpired(token)
            ? new ClaimsIdentity()
            : GetClaimsIdentity(token);
        ClaimsPrincipal user = new(identity);
        return new AuthenticationState(user);
    }

    public async Task AuthenticateUser(TokenDto token)
    {
        await localStorageService.SetItemAsync(appSettings.TokenName, token.Value);
        await localStorageService.SetItemAsync(appSettings.RefreshTokenName, token.RefreshToken);

        ClaimsIdentity identity = GetClaimsIdentity(token.Value);
        ClaimsPrincipal user = new(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task LogoutUser()
    {
        await localStorageService.RemoveItemAsync(appSettings.TokenName);
        await localStorageService.RemoveItemAsync(appSettings.RefreshTokenName);

        ClaimsIdentity identity = new();
        ClaimsPrincipal user = new(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    private static ClaimsIdentity GetClaimsIdentity(string token)
    {
        JwtSecurityTokenHandler handler = new();
        JwtSecurityToken jwtToken = handler.ReadJwtToken(token);
        return new ClaimsIdentity(jwtToken.Claims, JwtBearerDefaults.AuthenticationScheme);
    }

    private static bool IsTokenExpired(string token)
    {
        JwtSecurityTokenHandler handler = new();
        JwtSecurityToken jwtToken = handler.ReadJwtToken(token);
        DateTime expiry = jwtToken.ValidTo;
        return expiry < DateTime.UtcNow;
    }
}
