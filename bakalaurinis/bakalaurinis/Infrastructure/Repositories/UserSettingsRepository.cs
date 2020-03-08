using bakalaurinis.Infrastructure.Database;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakalaurinis.Infrastructure.Repositories
{
    public class UserSettingsRepository : IUserSettingsRepository
    {
        protected readonly DatabaseContext _context;

        public UserSettingsRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<int> Create(UserSettings entity)
        {
            _context.UserSettings.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
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

        public async Task<UserSettings> GetByUserId(int userId)
        {
            var settings = await _context.UserSettings.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            return settings;
        }

        public async Task<bool> Update(UserSettings entity)
        {
            _context.UserSettings.Attach(entity);
            var changes = await _context.SaveChangesAsync();

            return changes > 0;
        }
    }
}
