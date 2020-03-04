using bakalaurinis.Dtos.Activity;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Interfaces
{
    public interface IScheduleGenerationService
    {
        Task<bool> Generate(int userId);
        Task UpdateWhenExtendActivity(int userId, int activityId);
        Task UpdateWhenFinishActivity(int userId, int activityId);
        Task CalculateActivitiesTime(UpdateActivitiesDto updateActivitiesDto);
    }
}
