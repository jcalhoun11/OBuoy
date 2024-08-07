using GoogleMapsComponents;
using OBuoy.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var googleApiKey = Environment.GetEnvironmentVariable("GoogleApiKey") ?? builder.Configuration["GoogleApiKey"];
builder.Services.AddBlazorGoogleMaps(googleApiKey);
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    Environment.SetEnvironmentVariable("MongoDBConnection", builder.Configuration["MongoDBConnection"]);
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
