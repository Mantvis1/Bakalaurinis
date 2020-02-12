using bakalaurinis.Dtos.Activity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Interfaces
{
    public interface IActivitiesService
    {
        Task<GetActivityDto> GetById(int id);
        Task<ICollection<GetActivityDto>> GetAll();
        Task<int> Create(NewActivityDto newActivityDto);
        Task<bool> Delete(int id);
        Task Update(UpdateActivityDto updateActivityDto);
    }
}
