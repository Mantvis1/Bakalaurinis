using bakalaurinis.Infrastructure.Database;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Infrastructure.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {
        protected readonly DatabaseContext _context;

        public InvitationRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Task<int> Create(Invitation entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Invitation entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Invitation>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Invitation> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Invitation entity)
        {
            throw new NotImplementedException();
        }
    }
}
