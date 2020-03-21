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
            if ((await _activitiesRepository.FilterByUserIdAndStartTime(userId)).Any())
            {
                await UpdateSchedule(userId);
            }

            return false;
        }


        private async Task UpdateSchedule(int userId)
        {
            var currentDay = 0;
            int[] time = await MoveToNextDay(userId, currentDay);
            var activitiesToUpdate = (await _activitiesRepository.FilterByUserIdAndStartTime(userId)).OrderByDescending(x => x.ActivityPriority).ToList();
            var allActivities = (await _activitiesRepository.FilterByUserIdAndStartTimeIsNotNull(userId)).ToList();

            foreach (var activity in activitiesToUpdate)
            {
                var isFound = false;

                if (allActivities.Count == 0)
                {
                    activity.StartTime = _timeService.GetDateTime(time[0]);
                    activity.EndTime = _timeService.AddMinutesToTime(activity.StartTime.Value, activity.DurationInMinutes);

                    isFound = true;
                }

                if (!isFound)
                {
                    currentDay = allActivities.Last().StartTime.Value.Day - _timeService.GetCurrentDay().Day;
                    time = await MoveToNextDay(userId, currentDay);

                    if (time[1] -_timeService.GetDiferrentBetweenTwoDatesInMinutes(
                        _timeService.GetCurrentDay(),
                        _timeService.AddMinutesToTime(allActivities.Last().EndTime.Value, activity.DurationInMinutes))
                        > 0)
                    {
                        activity.StartTime = allActivities.Last().EndTime.Value;
                        activity.EndTime = _timeService.AddMinutesToTime(activity.StartTime.Value, activity.DurationInMinutes);

                        isFound = true;
                    }
                    else
                    {
                        currentDay += 1;
                        time = await MoveToNextDay(userId, currentDay);

                        activity.StartTime = _timeService.GetDateTime(time[0]);
                        activity.EndTime = _timeService.AddMinutesToTime(activity.StartTime.Value, activity.DurationInMinutes);

                        isFound = true;
                    }
                }

                if (isFound)
                {
                    await _activitiesRepository.Update(activity);
                    allActivities.Add(activity);
                }
            }
        }

        private async Task<Work> GetActivityNotToLongActivity(int length, int userId)
        {
            var activities = await _activitiesRepository.FilterByUserIdAndStartTime(userId);

            foreach (var activity in activities)
            {
                if (activity.DurationInMinutes <= length)
                    return activity;
            }

            return null;
        }

        private async Task<int[]> MoveToNextDay(int userId, int dayCount)
        {
            int[] time = new int[2];
            var userSettings = await _userSettingsRepository.GetByUserId(userId);

            time[0] = userSettings.StartTime * TimeConstants.MinutesInHour + dayCount * 24 * TimeConstants.MinutesInHour;
            time[1] = userSettings.EndTime * TimeConstants.MinutesInHour + dayCount * 24 * TimeConstants.MinutesInHour;

            return time;
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
            var currentTime = 8 * TimeConstants.MinutesInHour;

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
