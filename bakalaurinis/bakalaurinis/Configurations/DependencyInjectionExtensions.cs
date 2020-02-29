using bakalaurinis.Infrastructure.Repositories;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Infrastructure.Utils;
using bakalaurinis.Infrastructure.Utils.Interfaces;
using bakalaurinis.Services;
using bakalaurinis.Services.Interfaces;
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
            return service
                .AddScoped<IActivitiesRepository, ActivitiesRepository>()
                .AddScoped<IUserRepository, UsersRepository>()
                .AddSingleton<ITimeService, TimeService>();
        }

        public static IServiceCollection AddApplicationDependencies(this IServiceCollection service)
        {
            return service.AddScoped<IActivitiesService, ActivitiesService>()
                 .AddScoped<IUserService, UserService>()
                 .AddScoped<IScheduleService, ScheduleService>()
                 .AddScoped<IScheduleGenerationService, ScheduleGenerationService>();
        }
    }
}
