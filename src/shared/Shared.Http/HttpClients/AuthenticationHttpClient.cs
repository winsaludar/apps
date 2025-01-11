namespace Shared.Http.HttpClients;

public class AuthenticationHttpClient(HttpClient httpClient, AuthenticationApiSettings authApiSettings) : BaseHttpClient(httpClient), IAuthenticationHttpClient
{
    public async Task<ClientResponse> LoginAsync(string email, string password)
    {
        string url = $"{authApiSettings.BaseUrl}{authApiSettings.LoginRoute}";
        var payload = new { email, password };

        return await PostAsJsonAsync<LoginResponse>(url, payload, (response) =>
        {
            if (response is ErrorResponse errorResponse)
            {
                return new ClientResponse { IsSuccessful = false, Errors = errorResponse.Details };
            }
            else
            {
                TokenDto? token = (response as LoginResponse)?.Token;
                return new ClientResponse<TokenDto> { IsSuccessful = true, Data = token };
            }
        });
    }

    public async Task<ClientResponse> RegisterAsync(string username, string email, string password, string retypePassword)
    {
        string url = $"{authApiSettings.BaseUrl}{authApiSettings.RegisterRoute}";
        var payload = new { username, email, password, retypePassword };

        return await PostAsJsonAsync<RegisterResponse>(url, payload, (response) => 
        {
            if (response is ErrorResponse errorResponse)
            {
                return new ClientResponse { IsSuccessful = false, Errors = errorResponse.Details };
            }
            else
            {
                string? userId = (response as RegisterResponse)?.Id.ToString();
                return new ClientResponse<string> { IsSuccessful = true, Data = userId };
            }
        });
    }

    public async Task<ClientResponse> ConfirmEmailAsync(string email, string token)
    {
        string url = $"{authApiSettings.BaseUrl}{authApiSettings.ConfirmEmailRoute}";
        var payload = new { email, token };

        return await PostAsJsonAsync(url, payload);
    }

    public async Task<ClientResponse> ResendEmailConfirmationAsync(string email)
    {
        string url = $"{authApiSettings.BaseUrl}{authApiSettings.ResendEmailConfirmationRoute}";
        var payload = new { email };

        return await PostAsJsonAsync(url, payload);
    }

    public async Task<ClientResponse> ForgotPasswordAsync(string email)
    {
        string url = $"{authApiSettings.BaseUrl}{authApiSettings.ForgotPasswordRoute}";
        var payload = new { email };

        return await PostAsJsonAsync(url, payload);
    }

    public async Task<ClientResponse> ResetPasswordAsync(string email, string password, string retypePassword, string token)
    {
        string url = $"{authApiSettings.BaseUrl}{authApiSettings.ResetPasswordRoute}";
        var payload = new { email, password, retypePassword, token };

        return await PostAsJsonAsync(url, payload);
    }
}
