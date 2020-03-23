using AutoMapper;
using bakalaurinis.Constants;
using bakalaurinis.Dtos.Activity;
using bakalaurinis.Infrastructure.Database.Models;
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
        private readonly ITimeService _timeService;
        private readonly IMapper _mapper;
        private readonly IUserSettingsRepository _userSettingsRepository;

        public ScheduleGenerationService(
            IActivitiesRepository activitiesRepository,
            ITimeService timeService,
            IMapper mapper,
         IUserSettingsRepository userSettingsRepository
            
            )
        {
            _activitiesRepository = activitiesRepository;
            _timeService = timeService;
            _mapper = mapper;
            _userSettingsRepository = userSettingsRepository;
            

    }
    public async Task<bool> Generate(int userId)
        {
            int startTime = 8 * TimeConstants.MinutesInHour;
            int endTime = 10 * TimeConstants.MinutesInHour;

            if ((await _activitiesRepository.FilterByUserIdAndStartTime(userId)).Any())
            {
                if (await IsPossibleToUpdateExistingSchedule(userId))
                {
                    return await CreateUserSchedule(startTime, endTime, userId);
                }
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

        private async Task<bool> IsPossibleToUpdateExistingSchedule(int userId)
        {
            var userSchedule = (await _activitiesRepository.FilterByUserIdAndStartTimeIsNotNull(userId)).ToArray();

            if (userSchedule.Length > 1)
            {
                var currentActivity = userSchedule[0];
                var nextActivity = userSchedule[1];

                for (int i = 1; i < userSchedule.Length - 1; i++)
                {
                    int differentBetweenActivities = _timeService.GetDiferrentBetweenTwoDatesInMinutes(currentActivity.EndTime.Value, nextActivity.StartTime.Value);

                    if (differentBetweenActivities > 0)
                    {
                        await UpdateSchedule(currentActivity, differentBetweenActivities, userId);
                    }

                    userSchedule = (await _activitiesRepository.FilterByUserIdAndStartTimeIsNotNull(userId)).ToArray();

                    currentActivity = userSchedule[i];
                    nextActivity = userSchedule[i+1];

                }
            }
            return await IsActivitiesWithoutDateExsists(userId);
        }

        private async Task<bool> IsActivitiesWithoutDateExsists(int userId)
        {
            if ((await _activitiesRepository.FilterByUserIdAndStartTime(userId)).Any())
            {
                return true;
            }

            return false;
        }

        private async Task UpdateSchedule(Work current, int differentInMinutes, int userId)
        {
            var userActivities = await _activitiesRepository.FilterByUserIdAndStartTime(userId);

            foreach (var activity in userActivities)
            {
                int finishTime = _timeService.GetDiferrentBetweenTwoDatesInMinutes(_timeService.GetCurrentDay(), current.EndTime.Value) + activity.DurationInMinutes;

                if (activity.DurationInMinutes <= differentInMinutes && finishTime <= 600)
                {
                    activity.StartTime = current.EndTime.Value;
                    activity.EndTime = _timeService.AddMinutesToTime(activity.StartTime.Value, activity.DurationInMinutes);

                    differentInMinutes -= activity.DurationInMinutes;

                    await _activitiesRepository.Update(activity);
                }
            }
        }

        private void MoveToNextDay(out int startTime, out int endTime, int dayCount)
        {
            startTime = 8 * TimeConstants.MinutesInHour + dayCount * 24 * TimeConstants.MinutesInHour;
            endTime = 10 * TimeConstants.MinutesInHour + dayCount * 24 * TimeConstants.MinutesInHour;
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

        public async Task CalculateActivitiesTime(int id, DateTime date, UpdateActivitiesDto updateActivitiesDto)
        {
            var currentTime = _timeService.AddMinutesToTime(date, (await _userSettingsRepository.GetByUserId(id)).StartTime * 60);

            foreach (var activityDto in updateActivitiesDto.Activities)
            {
                activityDto.StartTime = currentTime;
                currentTime = _timeService.AddMinutesToTime(currentTime, activityDto.DurationInMinutes);
                activityDto.EndTime = currentTime;

                var activity = await _activitiesRepository.GetById(activityDto.Id);
                _mapper.Map(activityDto, activity);

                await _activitiesRepository.Update(activity);
            }
        }
    }
}
