namespace Shared.Http.HttpClients;

public interface IAuthenticationHttpClient
{
    Task<ClientResponse<TokenDto>> LoginAsync(string email, string password);
    Task<ClientResponse<string>> RegisterAsync(string username, string email, string password, string retypePassword);
}
