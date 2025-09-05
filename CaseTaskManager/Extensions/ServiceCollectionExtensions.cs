using CaseTaskManager.Interfaces;
using CaseTaskManager.Services;

namespace CaseTaskManager.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register your services here
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ICaseWorkerService, CaseWorkerService>();
            services.AddScoped<ICaseService, CaseService>();
            services.AddScoped<ICaseStatusService, CaseStatusService>();

            return services;
        }
    }
}
