namespace Shared.Http.HttpClients;

public interface IAuthenticationHttpClient
{
    Task<ClientResponse<TokenDto>> LoginAsync(string email, string password);
}
