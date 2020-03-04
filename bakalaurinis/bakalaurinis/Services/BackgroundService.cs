using bakalaurinis.Infrastructure.Enums;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace bakalaurinis.Services
{
    public class BackgroundService : IHostedService, IDisposable
    {
       // private readonly IUserService _userService;
       // private readonly IUserRepository _userRepository;

        public BackgroundService(/*IUserService userService, IUserRepository userRepository*/)
        {
       //     _userService = userService;
        //    _userRepository = userRepository;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            GenerateSchedule();

            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        private void GenerateSchedule()
        {
            var o = 111;
            var b = o + 7;

           /* var users = await _userService.GetAll();

            foreach(var user in users)
            {
                user.ScheduleStatus = ScheduleStatusEnum.DoesNotExist;

                await _userRepository.Update(user);
            }
            */
        }

        
    }
}
