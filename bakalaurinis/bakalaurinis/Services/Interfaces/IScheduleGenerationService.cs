using System.Threading.Tasks;

namespace bakalaurinis.Services.Interfaces
{
    public interface IScheduleGenerationService
    {
        Task<bool> Generate(int userId);
    }
}
