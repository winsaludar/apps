var builder = WebApplication.CreateBuilder(args);

// Register Blazor services
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddBlazoredSessionStorage().AddBlazoredLocalStorage();
builder.Services.AddHttpClient();
builder.Services.AddAuthentication();
builder.Services.AddCascadingAuthenticationState();

// Register custom services
builder.Services.AddRegistrarServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Note: The order of the middlewares here are important
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
