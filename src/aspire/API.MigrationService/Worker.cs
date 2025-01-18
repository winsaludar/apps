namespace API.MigrationService;

public class Worker(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var authDbContext = scope.ServiceProvider.GetRequiredService<AuthenticationDbContext>();
            var budgetDbContext = scope.ServiceProvider.GetRequiredService<BudgetDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            // Apply pending migrations
            await authDbContext.Database.MigrateAsync(cancellationToken);
            await budgetDbContext.Database.MigrateAsync(cancellationToken);

            // Seed database
            await SeedData.SeedUserAsync(userManager);
            await SeedData.SeedExpensesAsync(budgetDbContext);
        }
        catch (Exception ex) 
        {
            activity?.RecordException(ex);
            throw;
        }


        hostApplicationLifetime.StopApplication();
    }
}
