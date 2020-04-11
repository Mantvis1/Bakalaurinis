using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bakalaurinis.Dtos.Work;

namespace bakalaurinis.Services.Interfaces
{
    public interface IScheduleService
    {
        Task<ICollection<WorkDto>> GetAllByUserIdFilterByDate(int id, DateTime date);
    }
}
