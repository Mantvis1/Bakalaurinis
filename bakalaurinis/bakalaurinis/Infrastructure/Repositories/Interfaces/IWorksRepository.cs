using bakalaurinis.Dtos.Activity;
using bakalaurinis.Infrastructure.Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Infrastructure.Repositories.Interfaces
{
    public interface IWorksRepository : IRepository<Work>
    {
        Task<ICollection<Work>> FindAllByUserId(int id);
        Task<ICollection<Work>> FilterByUserIdAndTime(int id, DateTime today);
        Task<ICollection<Work>> FilterByUserIdAndStartTime(int id);
        Task<Work> FindLastByUserIdAndStartTime(int userId);
        Task<ICollection<Work>> FilterByUserIdAndStartTimeIsNotNull(int id);
    }
}
