using Microsoft.Extensions.DependencyInjection;
using Service.Services;

namespace Service
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.RegisterDatabase();

            services.AddSingleton<LoginService>();
            services.AddScoped<IPredictionService, PredictionService>();

            return services;
        }
    }
}
