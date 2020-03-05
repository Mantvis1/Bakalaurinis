using bakalaurinis.Dtos.Activity;
using bakalaurinis.Infrastructure.Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Infrastructure.Repositories.Interfaces
{
    public interface IActivitiesRepository : IRepository<Activity>
    {
        Task<ICollection<Activity>> FindAllByUserId(int id);
        Task<ICollection<Activity>> FilterByUserIdAndTime(int id, DateTime today);
        Task<ICollection<Activity>> FilterByUserIdAndStartTime(int id);
        Task<Activity> FindLastByUserIdAndStartTime(int userId);
    }
}
