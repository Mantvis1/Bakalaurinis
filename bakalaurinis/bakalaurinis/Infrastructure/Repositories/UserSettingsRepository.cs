using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Infrastructure.Repositories
{
    public class UserSettingsRepository : IUserSettingsRepository
    {
        public Task<int> Create(UserSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(UserSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<UserSettings>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<UserSettings> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(UserSettings entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
