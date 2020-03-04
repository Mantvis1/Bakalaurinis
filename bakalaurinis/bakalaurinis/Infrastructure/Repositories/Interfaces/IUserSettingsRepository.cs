using bakalaurinis.Infrastructure.Database.Models;
using System.Threading.Tasks;

namespace bakalaurinis.Infrastructure.Repositories.Interfaces
{
    public interface IUserSettingsRepository : IRepository<UserSettings>
    {
        Task<UserSettings> GetByUserId(int userId);
    }
}
