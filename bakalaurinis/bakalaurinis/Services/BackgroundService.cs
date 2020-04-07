using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace bakalaurinis.Services
{
    public class BackgroundService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public BackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            { 
                var users = await (scope.ServiceProvider.GetService<IUserService>()).GetAll();
                var scheduleGenerationService = scope.ServiceProvider.GetService<IScheduleGenerationService>();

                await GenerateSchedule(users, scheduleGenerationService);

            }
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        private async Task GenerateSchedule(ICollection<User> users, IScheduleGenerationService scheduleGenerationService)
        {
            foreach (var user in users)
            {
                await scheduleGenerationService.Generate(user.Id);
            }

        }
    }
}
