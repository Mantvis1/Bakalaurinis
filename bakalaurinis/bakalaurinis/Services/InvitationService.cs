using AutoMapper;
using bakalaurinis.Dtos.Invitation;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IMapper _mapper;

        public InvitationService(IInvitationRepository invitationRepository, IMapper mapper)
        {
            _invitationRepository = invitationRepository;
            _mapper = mapper;
        }

        public async Task<bool> Update(int invitationId, UpdateInvitationDto updateInvitationDto)
        {
            var invitation = await _invitationRepository.GetById(invitationId);

            _mapper.Map(updateInvitationDto, invitation);

            return await _invitationRepository.Update(invitation);
        }

        public async Task<ICollection<InvitationDto>> GetAllByRecieverId(int recieverId)
        {
            var invitations = await _invitationRepository.GetAllByRecieverId(recieverId);
            var invitationsDto = _mapper.Map<InvitationDto[]>(invitations);

            return invitationsDto;
        }

        public async Task<ICollection<InvitationDto>> GetAllBySenderId(int senderId)
        {
            var invitations = await _invitationRepository.GetAllBySenderId(senderId);
            var invitationsDto = _mapper.Map<InvitationDto[]>(invitations);

            return invitationsDto;
        }

        public async Task<int> Create(NewInvitationDto newInvitationDto)
        {
            var invitation = _mapper.Map<Invitation>(newInvitationDto);

            return await _invitationRepository.Create(invitation);
        }
    }
}
