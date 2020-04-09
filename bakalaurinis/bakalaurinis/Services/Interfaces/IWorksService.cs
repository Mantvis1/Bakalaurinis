using bakalaurinis.Dtos.Activity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Interfaces
{
    public interface IWorksService
    {
        Task<ActivityDto> GetById(int id);
        Task<ICollection<ActivityDto>> GetByUserId(int id);
        Task<ICollection<ActivityDto>> GetAll();
        Task<int> Create(NewActivityDto newActivityDto);
        Task<bool> Delete(int id);
        Task<bool> Update(int id, NewActivityDto activityDto);
    }
}
