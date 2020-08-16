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

        public GeneratorFreeSpaceDto GetGeneratedFreeSpace(Time time, int diferentBetweenTimes)
        {
            return new GeneratorFreeSpaceDto(_timeService.GetDateTime(time.GetStart()), _timeService.GetDateTime(time.GetEnd()), diferentBetweenTimes);
        }
    }
}
