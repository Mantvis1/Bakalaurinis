using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Enums;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
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
                var _userRepository = scope.ServiceProvider.GetService<IUserRepository>();

                var _scheduleGenerationService = scope.ServiceProvider.GetService<IScheduleGenerationService>();

                await GenerateSchedule(users, _scheduleGenerationService);

            }
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        private async Task GenerateSchedule(ICollection<User> users, IScheduleGenerationService _scheduleGenerationService)
        {
            foreach (var user in users)
            {
                await _scheduleGenerationService.Generate(user.Id);
            }

        }
    }
}
