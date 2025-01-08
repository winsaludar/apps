namespace Shared.Http.HttpClients;

public interface IAuthenticationHttpClient
{
    Task<BaseResponse> LoginAsync(string email, string password);
}
