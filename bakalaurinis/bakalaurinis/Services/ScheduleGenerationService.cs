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
        public async Task<bool> Generate(int userId)
        {
            var isScheduleCreated = (await _userRepository.GetById(userId)).ScheduleStatus;

            if (isScheduleCreated == ScheduleStatusEnum.DoesNotExist)
            {
                return await CreateUserSchedule(userId);
            }

            return false;
        }

        private async Task<bool> CreateUserSchedule(int userId)
        {
            var userActivities = await _activitiesRepository.FindAllByUserId(userId);

            return await UpdateUserScheduleStatus(userId);
        }

        private async Task<bool> UpdateUserScheduleStatus(int userId)
        {
            var currentUser = await _userRepository.GetById(userId);

            currentUser.ScheduleStatus = ScheduleStatusEnum.Ready;

            return await _userRepository.Update(currentUser);

        }
    }
}
