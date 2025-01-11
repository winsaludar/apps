var builder = DistributedApplication.CreateBuilder(args);

var database = builder.AddPostgres("db").WithPgAdmin();

var authentication = builder.AddProject<Projects.Authentication_API>("authentication-api")
    .WithReference(database);

builder.AddProject<Projects.API_MigrationService>("api-migrationservice")
    .WithReference(database);

builder.AddProject<Projects.BlazorApp>("blazorapp")
    .WithReference(authentication);

builder.Build().Run();
