using bakalaurinis.Infrastructure.Database;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Infrastructure.Repositories
{
    public class MessageTemplateRepository : IRepository<MessageTemplate>
    {
        protected readonly DatabaseContext _context;

        public MessageTemplateRepository(DatabaseContext context)
        {
            _context = context;
        }
        public Task<int> Create(MessageTemplate entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(MessageTemplate entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<MessageTemplate>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<MessageTemplate> GetById(int id)
        {
            var messageTempalate = await _context.MessageTemplates.FindAsync(id);

            return messageTempalate;
        }

        public Task<bool> Update(MessageTemplate entity)
        {
            throw new NotImplementedException();
        }
    }
}
