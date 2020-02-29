using bakalaurinis.Infrastructure.Enums;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System.Threading.Tasks;

namespace bakalaurinis.Services
{
    public class ScheduleGenerationService : IScheduleGenerationService
    {
        private readonly IActivitiesRepository _activitiesRepository;
        private readonly IUserRepository _userRepository;
        public ScheduleGenerationService(IActivitiesRepository activitiesRepository, IUserRepository userRepository)
        {
            _activitiesRepository = activitiesRepository;
            _userRepository = userRepository;
        }
        public async Task Generate(int userId)
        {
            var isScheduleCreated = (await _userRepository.GetById(userId)).ScheduleStatus;

            if(isScheduleCreated == ScheduleStatusEnum.DoesNotExist)
            {
                await CreateUserSchedule();
            }
        }

        private async Task CreateUserSchedule()
        {

        }
    }
}
