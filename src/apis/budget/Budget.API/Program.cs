var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddRegistrarServices(builder.Configuration);
AddMiddlewares(builder);
var app = builder.Build();
app.MapDefaultEndpoints();
EnableMiddlewares(app);
app.Run();

static void AddMiddlewares(WebApplicationBuilder builder)
{
    builder.Services.AddRouting(options => options.LowercaseUrls = true);

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddTransient<ExceptionHandlingMiddleware>();
    builder.Services.AddTransient<LoggingMiddleware>();
}

static void EnableMiddlewares(WebApplication app)
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseMiddleware<LoggingMiddleware>();

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    // Map our custom endpoints
    app.MapGroup("api")
        .RequireAuthorization()
        .MapCustomEndpoints();
}