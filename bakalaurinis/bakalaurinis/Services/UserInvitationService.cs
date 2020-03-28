using AutoMapper;
using bakalaurinis.Dtos.Invitation;
using bakalaurinis.Dtos.UserActivities;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System.Threading.Tasks;

namespace bakalaurinis.Services
{
    public class UserInvitationService : IUserInvitationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IInvitationRepository _invitationRepository;
        private readonly IMapper _mapper;

        public UserInvitationService(IInvitationRepository invitationRepository, IUserRepository userRepository, IMapper mapper)
        {
            _invitationRepository = invitationRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetInvitationDto[]> GetAllByActivityId(int id)
        {
            var invitations = await _invitationRepository.GetAllByActivityId(id);
            var userInvitationsDto = _mapper.Map<GetInvitationDto[]>(invitations);

            foreach(var item in userInvitationsDto)
            {
                item.ReceiverName = (await _userRepository.GetById(item.ReceiverId)).Username;
            }

            return userInvitationsDto;


        }
    }
}
