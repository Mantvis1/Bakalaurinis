using AutoMapper;
using bakalaurinis.Constants;
using bakalaurinis.Dtos.Activity;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
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
            int currentDay = 0;
            int[] time = await MoveToNextDay(userId, currentDay);
            var activitiesToUpdate = (await _activitiesRepository.FilterByUserIdAndStartTime(userId)).OrderByDescending(x => x.ActivityPriority).ToList();
            var allActivities = (await _activitiesRepository.FilterByUserIdAndStartTimeIsNotNull(userId)).ToList();

            foreach (var activity in activitiesToUpdate)
            {
                var isFound = false;

                if (!isFound)
                {
                    var days = allActivities.Last().StartTime.Value.Day - _timeService.GetCurrentDay().Day + 1;
                    time = await MoveToNextDay(userId, days);

                    activity.StartTime = _timeService.GetDateTime(time[0]);
                    activity.EndTime = _timeService.AddMinutesToTime(activity.StartTime.Value, activity.DurationInMinutes);

                    isFound = true;
                }

                if (isFound)
                {
                    await _activitiesRepository.Update(activity);
                    allActivities.Add(activity);
                    isFound = false;
                }
            }
        }

        private async Task<Work> GetActivitywithCorrectLength(int length, int userId)
        {
            var activities = await _activitiesRepository.FilterByUserIdAndStartTime(userId);

            foreach (var activity in activities)
            {
                if (activity.DurationInMinutes <= length)
                    return activity;
            }

            return null;
        }

        /*
            private async Task<bool> CreateUserSchedule(int[] time, int userId)
             {
                 var userActivities = (await _activitiesRepository.FilterByUserIdAndStartTime(userId)).OrderBy(x => x.ActivityPriority);
                 var lastActivity = await _activitiesRepository.FindLastByUserIdAndStartTime(userId);
                 int dayCount = 0;

                 if (lastActivity != null)
                 {
                     int diffentBetweenNowAndLastActivityDay = lastActivity.StartTime.Value.Day - _timeService.GetCurrentDay().Day;
                     dayCount += diffentBetweenNowAndLastActivityDay;

                     time = await MoveToNextDay(userId, dayCount);
                 }

                 while (userActivities.Count() > 0)
                 {
                     int finishTime = time[0] + userActivities.First().DurationInMinutes;

                     if (finishTime > time[1])
                     {
                         time = await MoveToNextDay(userId, dayCount);
                         dayCount++;
                     }

                     if (finishTime <= time[1])
                     {
                         userActivities.First().StartTime = _timeService.GetDateTime(time[0]);
                         time[0] += userActivities.First().DurationInMinutes;
                         userActivities.First().EndTime = _timeService.GetDateTime(time[0]);
                     }

                     await _activitiesRepository.Update(userActivities.First());

                     userActivities = (await _activitiesRepository.FilterByUserIdAndStartTime(userId)).OrderBy(x => x.ActivityPriority);
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
                         nextActivity = userSchedule[i + 1];

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
              }*/

        private async Task<int[]> MoveToNextDay(int userId, int dayCount)
        {
            int[] time = new int[2];
            time[0] = (await _userSettingsRepository.GetByUserId(userId)).StartTime * TimeConstants.MinutesInHour + dayCount * 24 * TimeConstants.MinutesInHour;
            time[1] = (await _userSettingsRepository.GetByUserId(userId)).EndTime * TimeConstants.MinutesInHour + dayCount * 24 * TimeConstants.MinutesInHour;

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
