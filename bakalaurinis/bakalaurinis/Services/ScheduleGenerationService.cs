using AutoMapper;
using bakalaurinis.Infrastructure.Enums;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using bakalaurinis.Dtos.Work;
using bakalaurinis.Services.Generation.Interfaces;
using bakalaurinis.Helpers;
using bakalaurinis.Services.Generation;

namespace bakalaurinis.Services
{
    public class ScheduleGenerationService : IScheduleGenerationService
    {
        private readonly IWorksRepository _worksRepository;
        private readonly ITimeService _timeService;
        private readonly IMapper _mapper;
        private readonly IUserSettingsRepository _userSettingsRepository;
        private readonly IMessageService _messageService;
        private readonly IFactory _factory;
        private readonly IFreeSpaceSaver _freeSpaceSaver;
        private Time _time;

        public ScheduleGenerationService(
            IWorksRepository worksRepository,
            ITimeService timeService,
            IMapper mapper,
            IUserSettingsRepository userSettingsRepository,
            IMessageService messageService,
            IFactory factory,
            IFreeSpaceSaver freeSpaceSarver
            )
        {
            _worksRepository = worksRepository;
            _timeService = timeService;
            _mapper = mapper;
            _userSettingsRepository = userSettingsRepository;
            _messageService = messageService;
            _factory = factory;
            _freeSpaceSaver = freeSpaceSarver;
            _time = new Time(0,0);

        }

        public async Task<bool> Generate(int userId)
        {
            if ((await _worksRepository.FilterByUserIdAndStartTime(userId)).Any())
            {
                await UpdateSchedule(userId);
                await _messageService.Create(userId, 0, MessageTypeEnum.Generation);
            }

            return false;
        }

        private async Task UpdateSchedule(int userId)
        {
            var userSettings = await _userSettingsRepository.GetByUserId(userId);
            var activitiesToUpdate = (await _worksRepository.FilterByUserIdAndStartTime(userId)).OrderByDescending(x => x.WorkPriority).ToList();
            var allActivities = (await _worksRepository.FilterByUserIdAndStartTimeIsNotNull(userId)).ToList();

            _time.MoveToNextDay(userSettings.StartTime, userSettings.EndTime);

            foreach (var activity in activitiesToUpdate)
            {
                var isFound = false;

                if (CompareValues.IsEqual(allActivities.Count, 0))
                {
                    activity.StartTime = _timeService.GetDateTime(_time.GetStart());
                    activity.EndTime = _timeService.AddMinutesToTime(activity.StartTime.Value, activity.DurationInMinutes);

                    isFound = true;
                }

                if (!isFound)
                {
                    await GetAllEmptySpaces(userId);

                    foreach (var empty in _freeSpaceSaver.GetAll())
                    {
                        if (CompareValues.IsGreaterOrEqual(empty.Duration, activity.DurationInMinutes))
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
                    var startTime = allActivities.Last().StartTime;
                    var endTime = allActivities.Last().EndTime;

                    if (!CompareValues.IsNull(startTime.Value) && !CompareValues.IsNull(endTime.Value))
                    {
                        _time.Update(startTime.Value.Day - _timeService.GetCurrentDay().Day);
                        _time.MoveToNextDay(userSettings.StartTime, userSettings.EndTime);

                        if (_time.GetEnd() - _timeService.GetDifferentBetweenTwoDatesInMinutes(
                                _timeService.GetCurrentDay(),
                                _timeService.AddMinutesToTime(endTime.Value,
                                    activity.DurationInMinutes))
                            > 0)
                        {
                            activity.StartTime = endTime.Value;
                            activity.EndTime =
                                _timeService.AddMinutesToTime(activity.StartTime.Value, activity.DurationInMinutes);
                        }
                        else
                        {
                            _time.AddOneDayToCurrent();
                            _time.MoveToNextDay(userSettings.StartTime, userSettings.EndTime);

                            activity.StartTime = _timeService.GetDateTime(_time.GetStart());
                            activity.EndTime =
                                _timeService.AddMinutesToTime(activity.StartTime.Value, activity.DurationInMinutes);
                        }
                    }
                }

                await _worksRepository.Update(activity);
                allActivities.Add(activity);
            }
        }

        private async Task GetAllEmptySpaces(int userId)
        {
            var allActivities = (await _worksRepository.FilterByUserIdAndStartTimeIsNotNull(userId)).OrderBy(x => x.StartTime).ToList();
            var userSettings = await _userSettingsRepository.GetByUserId(userId);

            for (var i = 0; i < allActivities.Count - 1; i++)
            {
                _time.MoveToNextDay(userSettings.StartTime, userSettings.EndTime);

                if (CompareValues.IsEqual(i, 0) && allActivities[i].StartTime.Value > _timeService.GetDateTime(_time.GetStart()))
                {
                    AddFreeSpaceIfTimeIsCorrect(_timeService.GetDateTime(_time.GetStart()), allActivities[i].StartTime.Value,_time);
                }

                if (_timeService.GetDifferentBetweenTwoDatesInMinutes(allActivities[i].EndTime.Value, allActivities[i + 1].StartTime.Value) > 0 &&
                    CompareValues.IsEqual(allActivities[i].EndTime.Value.Day,allActivities[i + 1].StartTime.Value.Day))
                {
                    _time.Update(_timeService.GetTimeInMinutes(allActivities[i].EndTime.Value), _timeService.GetTimeInMinutes(allActivities[i + 1].StartTime.Value));

                    _freeSpaceSaver.Add(_factory.GetGeneratedFreeSpace(_time,
                        _timeService.GetDifferentBetweenTwoDatesInMinutes(allActivities[i].EndTime.Value, allActivities[i + 1].StartTime.Value))
                        );
                }

                if (CompareValues.IsEqual(allActivities[i].EndTime.Value.Day + 1, allActivities[i + 1].EndTime.Value.Day))
                {
                    AddFreeSpaceIfTimeIsCorrect(allActivities[i].EndTime.Value, _timeService.GetDateTime(_time.GetEnd()), _time);
                    AddFreeSpaceIfTimeIsCorrect(_timeService.GetDateTime(_time.GetStart() + (int)TimeEnum.MinutesInDay), allActivities[i + 1].StartTime.Value, _time);
                }

                if (CompareValues.IsGreater(allActivities[i + 1].EndTime.Value.Day - allActivities[i].EndTime.Value.Day, 1))
                {
                    _time.UpdateCurrentDay(1 + allActivities[i].StartTime.Value.Day - _timeService.GetCurrentDay().Day);
                    _time.MoveToNextDay(userSettings.StartTime, userSettings.EndTime);

                    _freeSpaceSaver.Add(_factory.GetGeneratedFreeSpace(_time, _time.GetEnd() - _time.GetStart()));
                }
            }
        }

        public void AddFreeSpaceIfTimeIsCorrect(DateTime first, DateTime second, Time time)
        {
            var differenceBetweenDays = _timeService.GetDifferentBetweenTwoDatesInMinutes(first, second);

            if (CompareValues.IsGreater(differenceBetweenDays, 0))
            {
                _freeSpaceSaver.Add(_factory.GetGeneratedFreeSpace(time, differenceBetweenDays));
            }
        }

        public async Task CalculateActivitiesTime(int id, DateTime date, UpdateWorkDto updateActivitiesDto)
        {
            var currentTime = _timeService.AddMinutesToTime(date, (await _userSettingsRepository.GetByUserId(id)).StartTime * (int)TimeEnum.MinutesInHour);

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

        public async Task RecalculateWorkTimeWhenUserChangesSettings(int userId)
        {
            var worksToUpdate = (await _worksRepository.FilterByUserIdAndStartTimeIsNotNull(userId)).ToList();

            foreach (var workToUpdate in worksToUpdate)
            {
                workToUpdate.StartTime = null;
                workToUpdate.EndTime = null;

                await _worksRepository.Update(workToUpdate);
            }

            await Generate(userId);
        }
    }
}
