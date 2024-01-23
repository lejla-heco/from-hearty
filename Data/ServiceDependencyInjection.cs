using Data.Repositories.Base;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceDependencyInjection
{
    public static IServiceCollection RegisterDatabase(this IServiceCollection services)
    {
        services.AddDbContext<MyContext>();

        //using var db = new MyContext();
        //db.SetUpData();

        return services;
    }

    public static IServiceCollection RegisterRepos(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepo<>), typeof(Repo<>));

        return services;
    }
}

