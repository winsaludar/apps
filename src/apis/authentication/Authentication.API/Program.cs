var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRegistrarServices(builder.Configuration);
builder.Services.AddControllers();
AddMiddlewares(builder);

var app = builder.Build();
EnableMiddlewares(app);

app.Run();

static void AddMiddlewares(WebApplicationBuilder builder)
{
    builder.Services.AddRouting(options => options.LowercaseUrls = true);
    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddTransient<ExceptionHandlingMiddleware>();
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

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseAuthorization();
    app.UseAuthorization();

    app.MapControllers();
}