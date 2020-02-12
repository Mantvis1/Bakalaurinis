using bakalaurinis.Dtos.Activity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Interfaces
{
    public interface IActivitiesService
    {
        Task<ActivityDto> GetById(int id);
        Task<ICollection<ActivityDto>> GetAll();
        Task<int> Create(NewActivityDto newActivityDto);
        Task<bool> Delete(int id);
        Task Update(ActivityDto activityDto);
    }
}
