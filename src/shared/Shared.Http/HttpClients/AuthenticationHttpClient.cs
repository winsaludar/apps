namespace Shared.Http.HttpClients;

public class AuthenticationHttpClient(
    HttpClient httpClient, 
    AuthenticationApiSettings authApiSettings) : IAuthenticationHttpClient
{
    private const string FRIENDLY_ERROR = "Something went wrong, please try again later.";
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };
    
    public async Task<ClientResponse<TokenDto>> LoginAsync(string email, string password)
    {
        try
        {
            string url = $"{authApiSettings.BaseUrl}{authApiSettings.LoginRoute}";
            var payload = new { email, password };

            HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, payload);
            string responseContent = await response.Content.ReadAsStringAsync();
            BaseResponse? result = response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<LoginResponse?>(responseContent, _jsonSerializerOptions)
                : JsonSerializer.Deserialize<ErrorResponse?>(responseContent, _jsonSerializerOptions);

            return result switch
            {
                ErrorResponse errorResult when !response.IsSuccessStatusCode => new ClientResponse<TokenDto>
                {
                    IsSuccessful = false,
                    Errors = errorResult.Details
                },
                LoginResponse successResult => new ClientResponse<TokenDto>
                {
                    IsSuccessful = true,
                    Data = successResult.Token
                },
                _ => new ClientResponse<TokenDto>
                {
                    IsSuccessful = false,
                    Errors = [FRIENDLY_ERROR]
                },
            };
        }
        catch (Exception)
        {
            return new ClientResponse<TokenDto> { IsSuccessful = false, Errors = [FRIENDLY_ERROR] };
        }
    }
}
