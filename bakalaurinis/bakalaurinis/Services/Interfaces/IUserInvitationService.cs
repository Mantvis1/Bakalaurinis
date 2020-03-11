using bakalaurinis.Dtos.UserActivities;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Interfaces
{
    public interface IUserInvitationService
    {
        Task<UserInvitationsDto[]> GetAllByActivityId(int id);
    }
}
