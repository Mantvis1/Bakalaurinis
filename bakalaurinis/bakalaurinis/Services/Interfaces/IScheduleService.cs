using System;
using System.Threading.Tasks;
using bakalaurinis.Dtos.Schedule;

namespace bakalaurinis.Services.Interfaces
{
    public interface IScheduleService
    {
        Task<GetScheduleDto> GetAllByUserIdFilterByDate(int id, DateTime date);
        Task<int> GetBusyness(int id, DateTime date);
    }
}
