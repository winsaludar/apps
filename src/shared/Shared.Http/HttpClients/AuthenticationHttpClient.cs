namespace Shared.Http.HttpClients;

public class AuthenticationHttpClient(HttpClient httpClient, AuthenticationApiSettings authApiSettings) : IAuthenticationHttpClient
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };

    public async Task<BaseResponse> LoginAsync(string email, string password)
    {
        var payload = new { email, password };
        HttpResponseMessage? response = await httpClient.PostAsJsonAsync($"{authApiSettings.BaseUrl}{authApiSettings.LoginRoute}", payload);
        if (response is null)
            return new ErrorResponse(500, "Internal server error", ["Unable to call login api"]);

        string responseContent = await response.Content.ReadAsStringAsync();

        BaseResponse? result = response.IsSuccessStatusCode
            ? JsonSerializer.Deserialize<LoginResponse?>(responseContent, JsonSerializerOptions)
            : JsonSerializer.Deserialize<ErrorResponse?>(responseContent, JsonSerializerOptions);

        if (result is null)
            return new ErrorResponse(500, "Internal server error", ["Unable to parse login api response"]);

        return result;
    }
}
