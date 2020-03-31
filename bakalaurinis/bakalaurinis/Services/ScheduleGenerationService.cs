using AutoMapper;
using bakalaurinis.Constants;
using bakalaurinis.Dtos;
using bakalaurinis.Dtos.Activity;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Enums;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakalaurinis.Services
{
    public class ScheduleGenerationService : IScheduleGenerationService
    {
        private readonly IWorksRepository _worksRepository;
        private readonly ITimeService _timeService;
        private readonly IMapper _mapper;
        private readonly IUserSettingsRepository _userSettingsRepository;
        private readonly IInvitationRepository _invitationRepository;
        private readonly IMessageService _messageService;
        private readonly IUserRepository _userRepository;
        private readonly IWorksService _worksService;
        public ScheduleGenerationService(
            IWorksRepository worksRepository,
            ITimeService timeService,
            IMapper mapper,
            IUserSettingsRepository userSettingsRepository,
            IInvitationRepository invitationRepository,
            IMessageService messageService,
            IUserRepository userRepository
            )
        {
            _worksRepository = worksRepository;
            _timeService = timeService;
            _mapper = mapper;
            _userSettingsRepository = userSettingsRepository;
            _invitationRepository = invitationRepository;
            _messageService = messageService;
            _userRepository = userRepository;
        }
        
        public async Task<bool> Generate(int userId)
        {
            if ((await _worksRepository.FilterByUserIdAndStartTime(userId)).Any())
            {
                await ResetSchedule(userId);
                await UpdateSchedule(userId);
                await _messageService.Create(userId, 0, MessageTypeEnum.Generation);
            }

            return false;
        }

        private async Task UpdateSchedule(int userId)
        {
            var currentDay = 0;
            int[] time = await MoveToNextDay(userId, currentDay);
            var activitiesToUpdate = (await _worksRepository.FilterByUserIdAndStartTime(userId)).OrderByDescending(x => x.ActivityPriority).ToList();
            var allActivities = (await _worksRepository.FilterByUserIdAndStartTimeIsNotNull(userId)).ToList();

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
                    var empties = await GetAllEmptySpaces(userId);

                    foreach (var empty in empties)
                    {
                        if (IsActivityNotToLong(activity.DurationInMinutes, empty.Duration))
                        {
                            activity.StartTime = empty.Start;
                            activity.EndTime = _timeService.AddMinutesToTime(activity.StartTime.Value, activity.DurationInMinutes);

                            isFound = true;
                            break;
                        }
                    }
                }

                if (!isFound)
                {
                    currentDay = allActivities.Last().StartTime.Value.Day - _timeService.GetCurrentDay().Day;
                    time = await MoveToNextDay(userId, currentDay);

                    if (time[1] - _timeService.GetDiferrentBetweenTwoDatesInMinutes(
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
                    await _worksRepository.Update(activity);
                    allActivities.Add(activity);
                }
            }
        }

        private bool IsActivityNotToLong(int currentActivityDuration, int maxDuration)
        {
            if (currentActivityDuration <= maxDuration)
                return true;

            return false;
        }

        private async Task<ICollection<GeneratorFreeSpaceDto>> GetAllEmptySpaces(int userId)
        {
            var currentDay = 0;
            int[] time = await MoveToNextDay(userId, currentDay);
            var allActivities = (await _worksRepository.FilterByUserIdAndStartTimeIsNotNull(userId)).OrderBy(x => x.StartTime).ToList();
            var result = new List<GeneratorFreeSpaceDto>();

            for (int i = 0; i < allActivities.Count - 1; i++)
            {
                var diferenceBetweenDays = 0;
                time = await MoveToNextDay(userId, currentDay);

                if (i == 0 && allActivities[i].StartTime.Value > _timeService.GetDateTime(time[0]))
                {
                    diferenceBetweenDays = _timeService.GetDiferrentBetweenTwoDatesInMinutes(_timeService.GetDateTime(time[0]), allActivities[i].StartTime.Value);

                    result.Add(new GeneratorFreeSpaceDto(
                           _timeService.GetDateTime(time[0]),
                            allActivities[i].StartTime.Value,
                            diferenceBetweenDays
                            ));
                }

                if (_timeService.GetDiferrentBetweenTwoDatesInMinutes(allActivities[i].EndTime.Value, allActivities[i + 1].StartTime.Value) > 0 &&
                    allActivities[i].EndTime.Value.Day == allActivities[i + 1].StartTime.Value.Day)
                {
                    result.Add(new GeneratorFreeSpaceDto(
                        allActivities[i].EndTime.Value,
                        allActivities[i + 1].StartTime.Value,
                        _timeService.GetDiferrentBetweenTwoDatesInMinutes(allActivities[i].EndTime.Value, allActivities[i + 1].StartTime.Value))
                        );
                }

                if (allActivities[i].EndTime.Value.Day + 1 == allActivities[i + 1].EndTime.Value.Day)
                {
                    diferenceBetweenDays = _timeService.GetDiferrentBetweenTwoDatesInMinutes(allActivities[i].EndTime.Value, _timeService.GetDateTime(time[1]));

                    if (diferenceBetweenDays > 0)
                    {
                        result.Add(new GeneratorFreeSpaceDto(
                            allActivities[i].EndTime.Value,
                            _timeService.GetDateTime(time[1]),
                            diferenceBetweenDays
                            ));
                    }

                    diferenceBetweenDays = _timeService.GetDiferrentBetweenTwoDatesInMinutes(_timeService.GetDateTime(time[0] + 1440), allActivities[i + 1].StartTime.Value);

                    if (diferenceBetweenDays > 0)
                    {
                        result.Add(new GeneratorFreeSpaceDto(
                               _timeService.GetDateTime(time[0]),
                                allActivities[i].StartTime.Value,
                                diferenceBetweenDays
                                ));
                    }
                }

                if (allActivities[i + 1].EndTime.Value.Day - allActivities[i].EndTime.Value.Day > 1)
                {
                    time = await MoveToNextDay(userId, 1 + allActivities[i].StartTime.Value.Day - _timeService.GetCurrentDay().Day);

                    result.Add(new GeneratorFreeSpaceDto(
                        _timeService.GetDateTime(time[0]),
                        _timeService.GetDateTime(time[1]),
                        time[1] - time[0]
                        ));
                }

            }

            return result;
        }

        private async Task<int[]> MoveToNextDay(int userId, int dayCount)
        {
            int[] time = new int[2];
            var userSettings = await _userSettingsRepository.GetByUserId(userId);

            time[0] = userSettings.StartTime * TimeConstants.MinutesInHour + dayCount * 24 * TimeConstants.MinutesInHour;
            time[1] = userSettings.EndTime * TimeConstants.MinutesInHour + dayCount * 24 * TimeConstants.MinutesInHour;

            return time;
        }

        public async Task CalculateActivitiesTime(int id, DateTime date, UpdateActivitiesDto updateActivitiesDto)
        {
            var currentTime = _timeService.AddMinutesToTime(date, (await _userSettingsRepository.GetByUserId(id)).StartTime * 60);

            foreach (var activityDto in updateActivitiesDto.Activities)
            {
                activityDto.StartTime = currentTime;
                currentTime = _timeService.AddMinutesToTime(currentTime, activityDto.DurationInMinutes);
                activityDto.EndTime = currentTime;

                var activity = await _worksRepository.GetById(activityDto.Id);
                _mapper.Map(activityDto, activity);

                await _worksRepository.Update(activity);
            }
        }

        public async Task CreateWorkCopy(int workId, int userId)
        {
            var work = await _worksRepository.GetById(workId);

            work.Id = 0;
            work.UserId = userId;
            work.IsAuthor = false;

            await _worksRepository.Create(work);
        }

        public async Task ResetSchedule(int userId)
        {
            var works = (await _worksRepository.FilterByUserIdAndStartTimeIsNotNull(userId)).Where(x => !x.WillBeParticipant).ToList();

            foreach(var work in works)
            {
                work.StartTime = null;
                work.EndTime = null;

               await _worksRepository.Update(work);
            }
        }
    }
}
