namespace API.MigrationService;

public static class SeedData
{
    public static Guid UserId => new("2d730636-88cb-4424-bf69-29cdd1ee44f1");

    public static async Task SeedUserAsync(UserManager<AppUser> userManager)
    {
        string username = "wnsldr01";
        string email = "wnsldr01@gmail.com";

        var user = await userManager.FindByEmailAsync(email);
        if (user is not null)
            return;

        user = new AppUser
        {
            Id = UserId.ToString(),
            UserName = username,
            Email = email,
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            FirstName = "John",
            LastName = "Doe"
        };

        await userManager.CreateAsync(user, "Password_2025!");
    }

    public static async Task SeedExpensesAsync(BudgetDbContext dbContext)
    {
        ExpenseCategory foodCategory = new("Food", "Food Categories", UserId);
        ExpenseCategory transportCategory = new("Transportation", "Transportation Category", UserId);
        ExpenseCategory apparelCategory = new("Apparel", "Apparel Category", UserId);
        dbContext.ExpensesCategories.AddRange([foodCategory, transportCategory, apparelCategory]);

        Expense foodExpense = new(Guid.NewGuid(), UserId, 500, "PHP", DateTime.Today.ToUniversalTime(), "Lunch", foodCategory.Id);
        Expense transportExpense = new(Guid.NewGuid(), UserId, 250, "PHP", DateTime.Today.ToUniversalTime(), "Grab Taxi", transportCategory.Id);
        Expense apparelExpense = new(Guid.NewGuid(), UserId, 1500, "PHP", DateTime.Today.ToUniversalTime(), "T-Shirt (Uniqlo)", apparelCategory.Id);
        dbContext.Expenses.AddRange([foodExpense, transportExpense, apparelExpense]);

        await dbContext.SaveChangesAsync();
    }
}
