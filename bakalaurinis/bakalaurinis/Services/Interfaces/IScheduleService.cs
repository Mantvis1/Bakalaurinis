using bakalaurinis.Dtos.Activity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Interfaces
{
    public interface IScheduleService
    {
        Task<ICollection<ActivityDto>> GetAllByUserIdFilterByDate(int id);
    }
}
