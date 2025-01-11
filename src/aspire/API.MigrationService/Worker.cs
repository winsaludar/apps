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
            var dbContext = scope.ServiceProvider.GetRequiredService<AuthenticationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            // Apply pending migrations
            await dbContext.Database.MigrateAsync(cancellationToken);

            // Seed database
            await SeedData.SeedUserAsync(userManager);
        }
        catch (Exception ex) 
        {
            activity?.RecordException(ex);
            throw;
        }


        hostApplicationLifetime.StopApplication();
    }
}
