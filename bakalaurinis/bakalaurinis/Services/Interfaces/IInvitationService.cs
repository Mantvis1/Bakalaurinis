using bakalaurinis.Dtos.Invitation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Interfaces
{
    public interface IInvitationService
    {
        Task<bool> Update(int invitationId, UpdateInvitationDto updateInvitationDto);
        Task<ICollection<InvitationDto>> GetAllByRecieverId(int recieverId);
        Task<int> Create(NewInvitationDto newInvitationDto);
        Task<bool> Delete(int id);
    }
}
