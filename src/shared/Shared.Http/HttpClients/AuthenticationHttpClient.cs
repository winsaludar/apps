namespace Shared.Http.HttpClients;

public class AuthenticationHttpClient(HttpClient httpClient, AuthenticationApiSettings authApiSettings) : BaseHttpClient(httpClient), IAuthenticationHttpClient
{
    public async Task<ClientResponse> LoginAsync(string email, string password)
    {
        var payload = new { email, password };

        return await PostAsJsonAsync<LoginResponse>(authApiSettings.LoginRoute, payload, (response) =>
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
        var payload = new { username, email, password, retypePassword };

        return await PostAsJsonAsync<RegisterResponse>(authApiSettings.RegisterRoute, payload, (response) => 
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
        var payload = new { email, token };
        return await PostAsJsonAsync(authApiSettings.ConfirmEmailRoute, payload);
    }

    public async Task<ClientResponse> ResendEmailConfirmationAsync(string email)
    {
        var payload = new { email };
        return await PostAsJsonAsync(authApiSettings.ResendEmailConfirmationRoute, payload);
    }

    public async Task<ClientResponse> ForgotPasswordAsync(string email)
    {
        var payload = new { email };
        return await PostAsJsonAsync(authApiSettings.ForgotPasswordRoute, payload);
    }

    public async Task<ClientResponse> ResetPasswordAsync(string email, string password, string retypePassword, string token)
    {
        var payload = new { email, password, retypePassword, token };
        return await PostAsJsonAsync(authApiSettings.ResetPasswordRoute, payload);
    }
}
