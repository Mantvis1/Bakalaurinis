using System;
using System.Threading.Tasks;
using bakalaurinis.Dtos.Work;

namespace bakalaurinis.Services.Interfaces
{
    public interface IScheduleGenerationService
    {
        Task<bool> Generate(int userId);
        Task RecalculateWorkTimeWhenUserChangesSettings(int userId);
    }
}
