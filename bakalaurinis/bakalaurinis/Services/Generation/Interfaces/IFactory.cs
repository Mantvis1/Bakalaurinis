using bakalaurinis.Dtos;

namespace bakalaurinis.Services.Generation.Interfaces
{
    public interface IFactory
    {
        public GeneratorFreeSpaceDto GetGeneratedFreeSpace(int[] time, int diferentBetweenTimes);
    }
}
