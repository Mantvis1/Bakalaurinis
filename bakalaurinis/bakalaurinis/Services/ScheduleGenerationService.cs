using bakalaurinis.Infrastructure.Enums;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace bakalaurinis.Services
{
    public class ScheduleGenerationService : IScheduleGenerationService
    {
        private readonly IActivitiesRepository _activitiesRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITimeService _timeService;

        private int startTime = 8 * 60;
        private int endTime = 10 * 60;
        public ScheduleGenerationService(IActivitiesRepository activitiesRepository, IUserRepository userRepository, ITimeService timeService)
        {
            _activitiesRepository = activitiesRepository;
            _userRepository = userRepository;
            _timeService = timeService;
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
            var userActivities = (await _activitiesRepository.FilterByUserIdAndStartTime(userId)).OrderBy(x => x.ActivityPriority);

            foreach (var userActivity in userActivities)
            {
                int finishTime = startTime + userActivity.DurationInMinutes;

                if (finishTime > endTime)
                {
                    MoveToNextDay();
                }

                if (finishTime <= endTime)
                {
                    userActivity.StartTime = _timeService.GetDateTime(startTime);
                    startTime += userActivity.DurationInMinutes;
                    userActivity.EndTime = _timeService.GetDateTime(startTime);
                }

            }

            return await UpdateUserScheduleStatus(userId);
        }

        private void MoveToNextDay()
        {
            startTime = 8 * 60 + 24 * 60;
            endTime = 10 * 60 + 24 * 60;
        }

        private async Task<bool> UpdateUserScheduleStatus(int userId)
        {
            var currentUser = await _userRepository.GetById(userId);

            currentUser.ScheduleStatus = ScheduleStatusEnum.Ready;

            return await _userRepository.Update(currentUser);
        }

        public async Task UpdateWhenExtemdActivity(int userId, int activityId)
        {
            var isFound = false;
            var userActivities = await _activitiesRepository.FilterByUserIdAndTime(userId, _timeService.GetCurrentDay());

            foreach (var userActivity in userActivities)
            {
                if (userActivity.Id == activityId)
                {
                    isFound = true;
                }
                else if (isFound)
                {
                    userActivity.StartTime = _timeService.AddMinutesToTime(userActivity.StartTime.Value, 10);
                    userActivity.EndTime = _timeService.AddMinutesToTime(userActivity.EndTime.Value, 10);

                    await _activitiesRepository.Update(userActivity);
                }
            }
        }
    }
}
