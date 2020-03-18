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
    public class InvitationRepository : IInvitationRepository
    {
        protected readonly DatabaseContext _context;

        public InvitationRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Invitation entity)
        {
            _context.Invitations.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<bool> Delete(Invitation entity)
        {
            _context.Invitations.Remove(entity);
            var changes = await _context.SaveChangesAsync();

            return changes > 0;
        }

        public Task<ICollection<Invitation>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Invitation>> GetAllByActivityId(int activityId)
        {
            var invitations = await _context.Invitations.Where(x => x.WorkId == activityId).ToArrayAsync();

            return invitations;
        }

        public async Task<ICollection<Invitation>> GetAllByRecieverId(int recieverId)
        {
            var invitations = await _context.Invitations.Where(x => x.ReceiverId == recieverId && x.InvitationStatus == 0).ToArrayAsync();

            return invitations;
        }

        public async Task<Invitation> GetById(int id)
        {
            var invitation = await _context.Invitations.FindAsync(id);

            return invitation;
        }

        public async Task<bool> IsUserAlreadyHaveInvitation(int senderId, int activityId, int receiverId)
        {
            var result = (await _context.Invitations.Where(x =>
             x.WorkId == activityId &&
             x.SenderId == senderId &&
             x.ReceiverId == receiverId
            ).ToArrayAsync()).Length > 0;

            return result;
        }

        public async Task<bool> Update(Invitation entity)
        {
            _context.Invitations.Attach(entity);
            var changes = await _context.SaveChangesAsync();

            return changes > 0;
        }
    }
}
