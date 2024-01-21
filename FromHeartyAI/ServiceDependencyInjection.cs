using FromHeartyAI.ML_Model;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceDependencyInjection
{
    public static IServiceCollection RegisterMachineLearning(this IServiceCollection services)
    {
		services.AddSingleton<MLModel>();

		return services;
    }
}

