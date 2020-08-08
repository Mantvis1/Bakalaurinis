using bakalaurinis.Dtos;
using bakalaurinis.Services.Generation.Interfaces;
using bakalaurinis.Services.Interfaces;

namespace bakalaurinis.Services.Generation
{
    public class Factory : IFactory
    {
        private readonly ITimeService _timeService;

        public Factory(ITimeService timeService)
        {
            _timeService = timeService;
        }

        public GeneratorFreeSpaceDto GetGeneratedFreeSpace(int[] time, int diferentBetweenTimes)
        {
            return new GeneratorFreeSpaceDto(_timeService.GetDateTime(time[0]), _timeService.GetDateTime(time[1]), diferentBetweenTimes);
        }
    }
}
