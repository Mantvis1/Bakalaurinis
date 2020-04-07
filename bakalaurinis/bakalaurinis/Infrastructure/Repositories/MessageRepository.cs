using bakalaurinis.Infrastructure.Database;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakalaurinis.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DatabaseContext _context;
        public MessageRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Message entity)
        {
            _context.Messages.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<bool> Delete(Message entity)
        {
            _context.Messages.Remove(entity);
            var changes = await _context.SaveChangesAsync();

            return changes > 0;
        }

        public Task<ICollection<Message>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<ICollection<Message>> GetAllByUserId(int userId)
        {
            var messages = await _context.Messages.Where(x => x.UserId == userId).ToArrayAsync();

            return messages;
        }

        public async Task<Message> GetById(int id)
        {
            var message = await _context.Messages.FindAsync(id);

            return message;
        }

        public Task<bool> Update(Message entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
