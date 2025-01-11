namespace Shared.Http.HttpClients;

public class AuthenticationHttpClient(HttpClient httpClient, AuthenticationApiSettings authApiSettings) : IAuthenticationHttpClient
{
    private const string FRIENDLY_ERROR = "Something went wrong, please try again later.";
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };

    public async Task<ClientResponse> LoginAsync(string email, string password)
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
            return new ClientResponse { IsSuccessful = false, Errors = [FRIENDLY_ERROR] };
        }
    }

    public async Task<ClientResponse> RegisterAsync(string username, string email, string password, string retypePassword)
    {
        try
        {
            string url = $"{authApiSettings.BaseUrl}{authApiSettings.RegisterRoute}";
            var payload = new { username, email, password, retypePassword };

            HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, payload);
            string responseContent = await response.Content.ReadAsStringAsync();
            BaseResponse? result = response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<RegisterResponse?>(responseContent, _jsonSerializerOptions)
                : JsonSerializer.Deserialize<ErrorResponse?>(responseContent, _jsonSerializerOptions);

            return result switch
            {
                ErrorResponse errorResult when !response.IsSuccessStatusCode => new ClientResponse<string>
                {
                    IsSuccessful = false,
                    Errors = errorResult.Details
                },
                RegisterResponse successResult => new ClientResponse<string>
                {
                    IsSuccessful = true,
                    Data = successResult.Id.ToString()
                },
                _ => new ClientResponse<string>
                {
                    IsSuccessful = false,
                    Errors = [FRIENDLY_ERROR]
                },
            };
        }
        catch (Exception)
        {
            return new ClientResponse { IsSuccessful = false, Errors = [FRIENDLY_ERROR] };
        }
    }

    public async Task<ClientResponse> ConfirmEmailAsync(string email, string token)
    {
        try
        {
            string url = $"{authApiSettings.BaseUrl}{authApiSettings.ConfirmEmailRoute}";
            var payload = new { email, token };

            HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, payload);
            string responseContent = await response.Content.ReadAsStringAsync();
            BaseResponse? result = response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<SuccessResponse?>(responseContent, _jsonSerializerOptions)
                : JsonSerializer.Deserialize<ErrorResponse?>(responseContent, _jsonSerializerOptions);

            return (result is ErrorResponse errorResult)
                ? new() { IsSuccessful = false, Errors = errorResult.Details }
                : new() { IsSuccessful = true };
        }
        catch (Exception)
        {
            return new ClientResponse { IsSuccessful = false, Errors = [FRIENDLY_ERROR] };
        }
    }

    public async Task<ClientResponse> ResendEmailConfirmationAsync(string email)
    {
        try
        {
            string url = $"{authApiSettings.BaseUrl}{authApiSettings.ResendEmailConfirmationRoute}";
            var payload = new { email };

            HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, payload);
            string responseContent = await response.Content.ReadAsStringAsync();
            BaseResponse? result = response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<SuccessResponse?>(responseContent, _jsonSerializerOptions)
                : JsonSerializer.Deserialize<ErrorResponse?>(responseContent, _jsonSerializerOptions);

            return (result is ErrorResponse errorResult)
                ? new() { IsSuccessful = false, Errors = errorResult.Details }
                : new() { IsSuccessful = true };
        }
        catch (Exception)
        {
            return new ClientResponse { IsSuccessful = false, Errors = [FRIENDLY_ERROR] };
        }
    }

    public async Task<ClientResponse> ForgotPasswordAsync(string email)
    {
        try
        {
            string url = $"{authApiSettings.BaseUrl}{authApiSettings.ForgotPasswordRoute}";
            var payload = new { email };

            HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, payload);
            string responseContent = await response.Content.ReadAsStringAsync();
            BaseResponse? result = response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<SuccessResponse?>(responseContent, _jsonSerializerOptions)
                : JsonSerializer.Deserialize<ErrorResponse?>(responseContent, _jsonSerializerOptions);

            return (result is ErrorResponse errorResult)
                ? new() { IsSuccessful = false, Errors = errorResult.Details }
                : new() { IsSuccessful = true };
        }
        catch (Exception)
        {
            return new ClientResponse { IsSuccessful = false, Errors = [FRIENDLY_ERROR] };
        }
    }

    public async Task<ClientResponse> ResetPasswordAsync(string email, string password, string retypePassword, string token)
    {
        try
        {
            string url = $"{authApiSettings.BaseUrl}{authApiSettings.ResetPasswordRoute}";
            var payload = new { email, password, retypePassword, token };

            HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, payload);
            string responseContent = await response.Content.ReadAsStringAsync();
            BaseResponse? result = response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<SuccessResponse?>(responseContent, _jsonSerializerOptions)
                : JsonSerializer.Deserialize<ErrorResponse?>(responseContent, _jsonSerializerOptions);

            return (result is ErrorResponse errorResult)
                ? new() { IsSuccessful = false, Errors = errorResult.Details }
                : new() { IsSuccessful = true };
        }
        catch (Exception)
        {
            return new ClientResponse { IsSuccessful = false, Errors = [FRIENDLY_ERROR] };
        }
    }
}
