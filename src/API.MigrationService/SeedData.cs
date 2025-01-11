namespace API.MigrationService;

public static class SeedData
{
    public static async Task SeedUserAsync(UserManager<AppUser> userManager)
    {
        string username = "wnsldr01";
        string email = "wnsldr01@gmail.com";

        var user = await userManager.FindByEmailAsync(email);
        if (user is not null)
            return;

        user = new AppUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = username,
            Email = email,
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            FirstName = "John",
            LastName = "Doe"
        };

        await userManager.CreateAsync(user, "Password_2025!");
    }
}
