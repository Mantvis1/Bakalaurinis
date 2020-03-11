using bakalaurinis.Infrastructure.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Infrastructure.Repositories.Interfaces
{
    public interface IInvitationRepository : IRepository<Invitation>
    {
        Task<ICollection<Invitation>> GetAllBySenderId(int senderId);
        Task<ICollection<Invitation>> GetAllByRecieverId(int recieverId);
        Task<ICollection<Invitation>> GetAllByActivityId(int activityId);
    }
}
