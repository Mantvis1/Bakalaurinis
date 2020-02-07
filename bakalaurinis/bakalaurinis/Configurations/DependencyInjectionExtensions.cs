using Microsoft.Extensions.DependencyInjection;

namespace bakalaurinis.Configurations
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddAllDependencies(this IServiceCollection service)
        {
            return service
                .AddInfrastructureDependencies()
                .AddApplicationDependencies();
        }

        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection service)
        {
            return service;
        }

        public static IServiceCollection AddApplicationDependencies(this IServiceCollection service)
        {
            return service;
        }
    }
}
