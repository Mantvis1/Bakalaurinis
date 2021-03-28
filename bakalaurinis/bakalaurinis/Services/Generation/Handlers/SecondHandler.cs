using bakalaurinis.Helpers;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Enums;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Generation.Interfaces;
using bakalaurinis.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Generation.Handlers
{
    public class SecondHandler : JobTimeHandler<Work>
    {
        private readonly IWorksRepository _worksRepository;
        private readonly ITimeService _timeService;
        private readonly IFreeSpaceSaver _freeSpaceSaver;
        private readonly IUserSettingsRepository _userSettingsRepository;
        private readonly IFactory _factory;

        public SecondHandler(
           IWorksRepository worksRepository,
           ITimeService timeService,
           IFreeSpaceSaver freeSpaceSaver,
           IUserSettingsRepository userSettingsRepository,
             IFactory factory
           )
        {
            _worksRepository = worksRepository;
            _timeService = timeService;
            _freeSpaceSaver = freeSpaceSaver;
            _userSettingsRepository = userSettingsRepository;
            _factory = factory;
        }

        public override async Task Handle(Work work, int userId, Time time)
        {
            var isFound = false;

            await GetAllEmptySpaces(userId, time);

            foreach (var empty in _freeSpaceSaver.GetAll())
            {
                if (CompareValues.IsGreaterOrEqual(empty.Duration, work.DurationInMinutes))
                {
                    work.StartTime = empty.Start;
                    work.EndTime = _timeService.AddMinutesToTime(work.StartTime.Value, work.DurationInMinutes);

                    isFound = true;
                }
            }

            if (isFound)
            {
                await _worksRepository.Update(work);
            }
            else
            {
                await base.Handle(work, userId, time);
            }
        }

        private async Task GetAllEmptySpaces(int userId, Time time)
        {
            var allActivities = (await _worksRepository.FilterByUserIdAndStartTimeIsNotNull(userId)).OrderBy(x => x.StartTime).ToList();
            var userSettings = await _userSettingsRepository.GetByUserId(userId);

            for (var i = 0; i < allActivities.Count - 1; i++)
            {
                time.MoveToNextDay(userSettings.StartTime, userSettings.EndTime);

                if (CompareValues.IsEqual(i, 0) && allActivities[i].StartTime.Value > _timeService.GetDateTime(time.GetStart()))
                {
                    AddFreeSpaceIfTimeIsCorrect(_timeService.GetDateTime(time.GetStart()), allActivities[i].StartTime.Value, time);
                }

                if (_timeService.GetDifferentBetweenTwoDatesInMinutes(allActivities[i].EndTime.Value, allActivities[i + 1].StartTime.Value) > 0 &&
                    CompareValues.IsEqual(allActivities[i].EndTime.Value.Day, allActivities[i + 1].StartTime.Value.Day))
                {
                    time.Update(_timeService.GetTimeInMinutes(allActivities[i].EndTime.Value), _timeService.GetTimeInMinutes(allActivities[i + 1].StartTime.Value));

                    _freeSpaceSaver.Add(_factory.GetGeneratedFreeSpace(time,
                        _timeService.GetDifferentBetweenTwoDatesInMinutes(allActivities[i].EndTime.Value, allActivities[i + 1].StartTime.Value))
                        );
                }

                if (CompareValues.IsEqual(allActivities[i].EndTime.Value.Day + 1, allActivities[i + 1].EndTime.Value.Day))
                {
                    AddFreeSpaceIfTimeIsCorrect(allActivities[i].EndTime.Value, _timeService.GetDateTime(time.GetEnd()), time);
                    AddFreeSpaceIfTimeIsCorrect(_timeService.GetDateTime(time.GetStart() + (int)TimeEnum.MinutesInDay), allActivities[i + 1].StartTime.Value, time);
                }

                if (CompareValues.IsGreater(allActivities[i + 1].EndTime.Value.Day - allActivities[i].EndTime.Value.Day, 1))
                {
                    time.UpdateCurrentDay(1 + allActivities[i].StartTime.Value.Day - _timeService.GetCurrentDay().Day);
                    time.MoveToNextDay(userSettings.StartTime, userSettings.EndTime);

                    _freeSpaceSaver.Add(_factory.GetGeneratedFreeSpace(time, time.GetEnd() - time.GetStart()));
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
    }
}
