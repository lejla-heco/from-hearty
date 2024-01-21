using Microsoft.Extensions.DependencyInjection;

namespace Service
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<LoginService>();

            return services;
        }
    }
}
