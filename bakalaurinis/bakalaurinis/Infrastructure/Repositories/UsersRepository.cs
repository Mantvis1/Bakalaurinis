using bakalaurinis.Infrastructure.Database;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakalaurinis.Infrastructure.Repositories
{
    public class UsersRepository : IUserRepository
    {
        protected readonly DatabaseContext _context;

        public async Task<int> Create(User entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public Task<bool> Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
