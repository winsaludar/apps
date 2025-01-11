using API.MigrationService;
using Authentication.Infrastructure;
using Authentication.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();
builder.Services.AddOpenTelemetry().WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

SetupDbContext();

var host = builder.Build();
host.Run();

void SetupDbContext()
{
    builder.Services.AddDbContext<AuthenticationDbContext>(options =>
    {
        string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__db") ?? throw new InvalidOperationException("Database connection string not found");
        options.UseNpgsql(connectionString);
    }, ServiceLifetime.Scoped);

    // Must be the same setup with Authentication API
    builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequiredUniqueChars = 1;

        // Lockout user for 15 minutes when password is incorrect after 3 tries.
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        options.Lockout.MaxFailedAccessAttempts = 3;
        options.Lockout.AllowedForNewUsers = true;

        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<AuthenticationDbContext>()
    .AddDefaultTokenProviders();
}