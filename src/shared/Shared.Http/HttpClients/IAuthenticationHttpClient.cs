namespace Shared.Http.HttpClients;

public interface IAuthenticationHttpClient
{
    Task<ClientResponse> LoginAsync(string email, string password);
    Task<ClientResponse> RegisterAsync(string username, string email, string password, string retypePassword);
    Task<ClientResponse> ConfirmEmailAsync(string email, string token);
    Task<ClientResponse> ResendEmailConfirmationAsync(string email);
    Task<ClientResponse> ForgotPasswordAsync(string email);
    Task<ClientResponse> ResetPasswordAsync(string email, string password, string retypePassword, string token);
}
