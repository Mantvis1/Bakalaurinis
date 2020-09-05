using bakalaurinis.Helpers;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Generation.Handlers
{
    public class ThirdHandler : JobTimeHandler<Work>
    {
        private readonly IWorksRepository _worksRepository;
        private readonly ITimeService _timeService;
        private readonly IUserSettingsRepository _userSettingsRepository;

        public ThirdHandler(
           IWorksRepository worksRepository,
           ITimeService timeService,
             IUserSettingsRepository userSettingsRepository
           )
        {
            _worksRepository = worksRepository;
            _timeService = timeService;
            _userSettingsRepository = userSettingsRepository;
        }

        public override async Task Handle(Work work, int userId, Time time)
        {
            var allActivities = (await _worksRepository.FilterByUserIdAndStartTimeIsNotNull(userId)).ToList();
            var userSettings = await _userSettingsRepository.GetByUserId(userId);
            var startTime = allActivities.Last().StartTime;
            var endTime = allActivities.Last().EndTime;

            if (!CompareValues.IsNull(startTime.Value) && !CompareValues.IsNull(endTime.Value))
            {
                time.Update(startTime.Value.Day - _timeService.GetCurrentDay().Day);
                time.MoveToNextDay(userSettings.StartTime, userSettings.EndTime);

                if (time.GetEnd() - _timeService.GetDifferentBetweenTwoDatesInMinutes(
                        _timeService.GetCurrentDay(),
                        _timeService.AddMinutesToTime(endTime.Value,
                            work.DurationInMinutes))
                    > 0)
                {
                    work.StartTime = endTime.Value;
                    work.EndTime =
                        _timeService.AddMinutesToTime(work.StartTime.Value, work.DurationInMinutes);
                }
                else
                {
                    time.AddOneDayToCurrent();
                    time.MoveToNextDay(userSettings.StartTime, userSettings.EndTime);

                    work.StartTime = _timeService.GetDateTime(time.GetStart());
                    work.EndTime =
                        _timeService.AddMinutesToTime(work.StartTime.Value, work.DurationInMinutes);
                }
            }

            await _worksRepository.Update(work);
        }
    }
}
