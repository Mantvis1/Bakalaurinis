using System.Threading.Tasks;
using bakalaurinis.Dtos.UserInvitations;

namespace bakalaurinis.Services.Interfaces
{
    public interface IUserInvitationService
    {
        Task<UserInvitationsDto[]> GetAllByActivityId(int id);
    }
}
