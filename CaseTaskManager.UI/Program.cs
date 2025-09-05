using CaseTaskManager.UI.Data;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Services;

namespace CaseTaskManager.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();

            builder.Services.AddHttpClient<ITaskApiService, TaskApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7156/api/");
            });

            builder.Services.AddHttpClient<ICaseApiService, CaseApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7156/api/");
            });

            builder.Services.AddHttpClient<ICaseWorkerApiService, CaseWorkerApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7156/api/");
            });

            builder.Services.AddHttpClient<ITaskTypeApiService, TaskTypeApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7156/api/");
            });

            builder.Services.AddHttpClient<ITaskStatusApiService, TaskStatusApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7156/api/");
            });

            builder.Services.AddHttpClient<ICaseStatusApiService, CaseStatusApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7156/api/");
            });


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
        }
    }
}
