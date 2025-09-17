using CaseTaskManager.UI;
using CaseTaskManager.UI.Configuration;
using CaseTaskManager.UI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add Razor/Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Bind API config & register HttpClients
builder.Services
    .AddAndBindApiOptions(builder.Configuration)
    .AddApiClients()
    .AddUiServices(); // e.g., WeatherForecastService

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
