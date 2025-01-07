namespace Authentication.Core.Contracts;

public interface IUserRepository
{
    Task<User?> GetByUserIdAsync(string userId);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task<Guid> RegisterAsync(User user, string password);
    Task<bool> ValidateLoginPasswordAsync(string email, string password);
    Task<bool> ValidateRegisterPasswordAsync(string password);
    Task<bool> ConfirmEmailAsync(string email, string token);
    Task<bool> ResetPasswordAsync(string email, string password, string token);
}
