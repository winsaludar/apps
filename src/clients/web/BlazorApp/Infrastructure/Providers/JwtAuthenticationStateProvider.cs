namespace BlazorApp.Infrastructure.Providers;

public class JwtAuthenticationStateProvider(ISessionStorageService sessionStorage) : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string? token = await sessionStorage.GetItemAsStringAsync("authToken");
        ClaimsIdentity identity = !string.IsNullOrEmpty(token)
            ? new(ParseClaimsFromJwt(token), "Bearer")
            : new();

        ClaimsPrincipal user = new(identity);
        return new AuthenticationState(user);
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        string payload = jwt.Split('.')[1];
        byte[] jsonBytes = WebEncoders.Base64UrlDecode(payload);
        Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        if (keyValuePairs is null)
            return [];

        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString() ?? ""));
    }
}
