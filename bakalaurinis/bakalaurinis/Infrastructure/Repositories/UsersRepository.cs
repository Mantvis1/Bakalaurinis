using bakalaurinis.Dtos.User;
using bakalaurinis.Infrastructure.Database;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakalaurinis.Infrastructure.Repositories
{
    public class UsersRepository : IUserRepository
    {
        protected readonly DatabaseContext _context;

        public UsersRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> Create(User entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<bool> Delete(User entity)
        {
            _context.Users.Remove(entity);
           var changes = await _context.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<ICollection<User>> GetAll()
        {
            var users = await _context.Users.ToArrayAsync();

            return users;
        }

        public async Task<User> GetById(int id)
        {
            var user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

            return user;
        }

        public async Task<User> GetUserByNameAndPassword(AuthenticateDto authenticateDto)
        {
            var user = await _context.Users.Where(x => x.Username == authenticateDto.Username && x.Password == authenticateDto.Password).FirstOrDefaultAsync();

            return user;
        }

        public async Task<bool> Update(User entity)
        {
            _context.Users.Attach(entity);
            var changes = await _context.SaveChangesAsync();

            return changes > 0;
        }
    }
}
