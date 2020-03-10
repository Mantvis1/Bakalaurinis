using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Repositories;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
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
                .AddSingleton<ITimeService, TimeService>()
                .AddScoped<IUserSettingsRepository, UserSettingsRepository>()
                .AddScoped<IMessageRepository, MessageRepository>()
                .AddScoped<IRepository<MessageTemplate>, MessageTemplateRepository>()
                .AddScoped<IInvitationRepository, InvitationRepository>();
        }

        public static IServiceCollection AddApplicationDependencies(this IServiceCollection service)
        {
            return service.AddScoped<IActivitiesService, ActivitiesService>()
                 .AddScoped<IUserService, UserService>()
                 .AddScoped<IScheduleService, ScheduleService>()
                 .AddScoped<IScheduleGenerationService, ScheduleGenerationService>()
                 .AddScoped<IUserSettingsService, UserSettingsService>()
                 .AddScoped<IMessageService, MessageService>()
                 .AddScoped<IInvitationService, InvitationService>();
        }
    }
}
