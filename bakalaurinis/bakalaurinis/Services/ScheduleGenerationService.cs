using AutoMapper;
using bakalaurinis.Constants;
using bakalaurinis.Dtos.Activity;
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
        private readonly IMapper _mapper;


        public ScheduleGenerationService(IActivitiesRepository activitiesRepository, IUserRepository userRepository, ITimeService timeService, IMapper mapper)
        {
            _activitiesRepository = activitiesRepository;
            _userRepository = userRepository;
            _timeService = timeService;
            _mapper = mapper;
        }
        public async Task<bool> Generate(int userId)
        {
            int startTime = 8 * 60;
            int endTime = 10 * 60;
            int userActivitiesCount = (await _activitiesRepository.FilterByUserIdAndStartTime(userId)).Count;

            if (userActivitiesCount > 0)
            {
                return await CreateUserSchedule(startTime, endTime, userId);
            }

            return false;
        }

        private async Task<bool> CreateUserSchedule(int startTime, int endTime, int userId)
        {
            var userActivities = (await _activitiesRepository.FilterByUserIdAndStartTime(userId)).OrderBy(x => x.ActivityPriority);
            var lastActivity = await _activitiesRepository.FindLastByUserIdAndStartTime(userId);
            int dayCount = 1;

            if (lastActivity != null)
            {
                int diffentBetweenNowAndLastActivityDay = lastActivity.StartTime.Value.Day - _timeService.GetCurrentDay().Day;
                dayCount += diffentBetweenNowAndLastActivityDay;

                MoveToNextDay(out startTime, out endTime, dayCount);
            }

            foreach (var userActivity in userActivities)
            {
                int finishTime = startTime + userActivity.DurationInMinutes;

                if (finishTime > endTime)
                {
                    MoveToNextDay(out startTime, out endTime, dayCount);
                    dayCount++;
                }

                if (finishTime <= endTime)
                {
                    userActivity.StartTime = _timeService.GetDateTime(startTime);
                    startTime += userActivity.DurationInMinutes;
                    userActivity.EndTime = _timeService.GetDateTime(startTime);
                }

                await _activitiesRepository.Update(userActivity);

            }

            return true;
        }

        private void MoveToNextDay(out int startTime, out int endTime, int dayCount)
        {
            startTime = 8 * 60 + dayCount * 24 * 60;
            endTime = 10 * 60 + dayCount * 24 * 60;
        }

        public async Task UpdateWhenExtendActivity(int userId, int activityId)
        {
            var isFound = false;
            var userActivities = (await _activitiesRepository.FilterByUserIdAndTime(userId, _timeService.GetCurrentDay()))
                .OrderBy(x => x.ActivityPriority);

            foreach (var userActivity in userActivities)
            {
                if (userActivity.Id == activityId)
                {
                    isFound = true;
                }
                else if (isFound)
                {
                    userActivity.StartTime = _timeService.AddMinutesToTime(userActivity.StartTime.Value, ActivityConstatns.ActivityExtensionTime);
                    userActivity.EndTime = _timeService.AddMinutesToTime(userActivity.EndTime.Value, ActivityConstatns.ActivityExtensionTime);

                    await _activitiesRepository.Update(userActivity);
                }
            }
        }

        public async Task UpdateWhenFinishActivity(int userId, int activityId)
        {
            var userActivities = (await _activitiesRepository.FilterByUserIdAndTime(userId, _timeService.GetCurrentDay()))
                .OrderBy(x => x.ActivityPriority);

            foreach (var userActivity in userActivities)
            {
                if (userActivity.Id == activityId)
                {
                    userActivity.IsFinished = true;
                }

                await _activitiesRepository.Update(userActivity);
            }
        }

        public async Task CalculateActivitiesTime(UpdateActivitiesDto updateActivitiesDto)
        {
            var currentTime = 8 * 60;

            foreach (var activityDto in updateActivitiesDto.Activities)
            {
                activityDto.StartTime = _timeService.GetDateTime(currentTime);
                currentTime += activityDto.DurationInMinutes;
                activityDto.EndTime = _timeService.GetDateTime(currentTime);

                var activity = await _activitiesRepository.GetById(activityDto.Id);
                _mapper.Map(activityDto, activity);

                await _activitiesRepository.Update(activity);
            }
        }
    }
}
