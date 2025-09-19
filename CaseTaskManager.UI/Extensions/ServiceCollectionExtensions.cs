using System.Net.Http.Headers;
using CaseTaskManager.UI.Configuration;
using CaseTaskManager.UI.Data;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Services;
using Microsoft.Extensions.Options;

namespace CaseTaskManager.UI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAndBindApiOptions(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<ApiOptions>(configuration.GetSection(ApiOptions.SectionName));
            return services;
        }

        public static IServiceCollection AddApiClients(this IServiceCollection services)
        {
            static void Configure(HttpClient client, ApiOptions opts)
            {
                client.BaseAddress = new Uri(opts.BaseUrl, UriKind.Absolute);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                // Optional:
                // client.Timeout = TimeSpan.FromSeconds(100);
            }

            services.AddHttpClient<ITaskApiService, TaskApiService>((sp, client) =>
            {
                var opts = sp.GetRequiredService<IOptions<ApiOptions>>().Value;
                Configure(client, opts);
            });

            services.AddHttpClient<ICaseApiService, CaseApiService>((sp, client) =>
            {
                var opts = sp.GetRequiredService<IOptions<ApiOptions>>().Value;
                Configure(client, opts);
            });

            services.AddHttpClient<ICaseWorkerApiService, CaseWorkerApiService>((sp, client) =>
            {
                var opts = sp.GetRequiredService<IOptions<ApiOptions>>().Value;
                Configure(client, opts);
            });

            services.AddHttpClient<ITaskTypeApiService, TaskTypeApiService>((sp, client) =>
            {
                var opts = sp.GetRequiredService<IOptions<ApiOptions>>().Value;
                Configure(client, opts);
            });

            services.AddHttpClient<ITaskStatusApiService, TaskStatusApiService>((sp, client) =>
            {
                var opts = sp.GetRequiredService<IOptions<ApiOptions>>().Value;
                Configure(client, opts);
            });

            services.AddHttpClient<ICaseStatusApiService, CaseStatusApiService>((sp, client) =>
            {
                var opts = sp.GetRequiredService<IOptions<ApiOptions>>().Value;
                Configure(client, opts);
            });

            return services;
        }

        public static IServiceCollection AddUiServices(this IServiceCollection services)
        {
            services.AddSingleton<WeatherForecastService>();
            return services;
        }
    }
}
