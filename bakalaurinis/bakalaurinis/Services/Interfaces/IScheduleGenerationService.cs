using bakalaurinis.Dtos.Activity;
using System;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Interfaces
{
    public interface IScheduleGenerationService
    {
        Task<bool> Generate(int userId);
        Task CalculateActivitiesTime(int id, DateTime date, UpdateActivitiesDto updateActivitiesDto);
        Task GenerateTimeByWorkId(int workId);
    }
}
