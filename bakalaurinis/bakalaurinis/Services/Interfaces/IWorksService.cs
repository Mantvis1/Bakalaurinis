using System.Collections.Generic;
using System.Threading.Tasks;
using bakalaurinis.Dtos.Work;

namespace bakalaurinis.Services.Interfaces
{
    public interface IWorksService
    {
        Task<WorkDto> GetById(int id);
        Task<ICollection<WorkDto>> GetByUserId(int id);
        Task<ICollection<WorkDto>> GetAll();
        Task<int> Create(NewWorkDto newActivityDto);
        Task<bool> Delete(int id);
        Task<bool> Update(int id, NewWorkDto activityDto);
    }
}
