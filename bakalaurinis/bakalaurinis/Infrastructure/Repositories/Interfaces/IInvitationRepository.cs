using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Infrastructure.Repositories.Interfaces
{
    public interface IInvitationRepository : IRepository<Invitation>
    {
        Task<ICollection<Invitation>> GetAllByReceiverId(int recieverId);
        Task<ICollection<Invitation>> GetAllByActivityId(int workId);
        Task<bool> IsUserAlreadyHaveInvitation(int senderId, int workId, int receiverId);
        Task<bool> IsWorkHavePendingInvitation(int workId);
        Task<ICollection<Invitation>> GetAllByIdAndStatus(int workId, InvitationStatusEnum status);
    }
}
