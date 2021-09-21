using bakalaurinis.Helpers;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Generation.Handlers
{
    public class FirstHandler : JobTimeHandler<Work>
    {
        private readonly IWorksRepository _worksRepository;
        private readonly ITimeService _timeService;

        public FirstHandler(
           IWorksRepository worksRepository,
           ITimeService timeService
           )
        {
            _worksRepository = worksRepository;
            _timeService = timeService;
        }

        public override async Task Handle(Work work, int userId, Time time)
        {
            var works = (await _worksRepository.FilterByUserIdAndStartTimeIsNotNull(userId)).ToList();

            if (CompareValues.IsEqual(works.Count, 0))
            {
                work.StartTime = _timeService.GetDateTime(time.GetStart());
                work.EndTime = _timeService.AddMinutesToTime(work.StartTime.Value, work.DurationInMinutes);

                await _worksRepository.Update(work);

            }
            else
            {
                await base.Handle(work, userId, time);
            }
        }
    }
}
