namespace Authentication.Infrastructure.Repositories;

public class UserRepository(UserManager<AppUser> userManager) : IUserRepository
{
    public async Task<User?> GetByUserIdAsync(string userId)
    {
        AppUser? dbUser = await userManager.FindByIdAsync(userId);
        if (dbUser is null)
            return null;

        return ConvertToUser(dbUser);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        AppUser? dbUser = await userManager.FindByEmailAsync(email);
        if (dbUser is null)
            return null;

        return ConvertToUser(dbUser);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        AppUser? dbUser = await userManager.FindByNameAsync(username);
        if (dbUser is null)
            return null;

        return ConvertToUser(dbUser);
    }

    public async Task<Guid> RegisterAsync(User user)
    {
        AppUser newUser = new() 
        {
            Id = Guid.NewGuid().ToString(),
            UserName = user.Username,
            Email = user.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            FirstName = "DEFAULT", // TODO
            LastName = "DEFAULT", // TODO
        };

        await userManager.CreateAsync(newUser, user.Password);

        return Guid.Parse(newUser.Id);
    }

    public async Task<bool> ValidateLoginPasswordAsync(string email, string password)
    {
        AppUser? dbUser = await userManager.FindByEmailAsync(email);
        if (dbUser is null) 
            return false;

        return await userManager.CheckPasswordAsync(dbUser, password);
    }

    public async Task<bool> ValidateRegisterPasswordAsync(string password)
    {
        var passwordValidators = userManager.PasswordValidators;
        foreach (var validator in passwordValidators)
        {
            var result = await validator.ValidateAsync(userManager, null!, password);
            if (!result.Succeeded)
                return false;
        }

        return true;
    }

    private static User ConvertToUser(AppUser dbUser) => User.Create(dbUser.UserName!, dbUser.Email!, dbUser.PasswordHash!, Guid.Parse(dbUser.Id), dbUser.EmailConfirmed);
}
