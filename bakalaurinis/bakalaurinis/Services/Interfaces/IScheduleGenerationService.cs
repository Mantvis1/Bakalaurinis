using System;
using System.Threading.Tasks;
using bakalaurinis.Dtos.Work;

namespace bakalaurinis.Services.Interfaces
{
    public interface IScheduleGenerationService
    {
        Task<bool> Generate(int userId);
        Task CalculateActivitiesTime(int id, DateTime date, UpdateWorkDto updateActivitiesDto);
        Task CreateWorkCopy(int workId, int userId);
    }
}
