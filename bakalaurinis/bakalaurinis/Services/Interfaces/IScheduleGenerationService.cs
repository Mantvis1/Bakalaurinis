using System.Threading.Tasks;

namespace bakalaurinis.Services.Interfaces
{
    public interface IScheduleGenerationService
    {
        Task Generate(int userId);
    }
}
