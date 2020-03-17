using AutoMapper;
using bakalaurinis.Dtos.Invitation;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Enums;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakalaurinis.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IMessageService _messageService;
        private readonly IRepository<MessageTemplate> _messageTemplateRepository;
        private readonly IMessageFormationService _messageFormationService;

        public InvitationService(
            IInvitationRepository invitationRepository,
            IMapper mapper,
            IUserRepository userRepository,
            IMessageService messageService,
            IRepository<MessageTemplate> messageTemplateRepository,
            IMessageFormationService messageFormationService
            )
        {
            _invitationRepository = invitationRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _messageService = messageService;
            _messageTemplateRepository = messageTemplateRepository;
            _messageFormationService = messageFormationService;
        }

        public async Task<bool> Update(int invitationId, UpdateInvitationDto updateInvitationDto)
        {
            var invitation = await _invitationRepository.GetById(invitationId);

            _mapper.Map(updateInvitationDto, invitation);

            if (updateInvitationDto.InvitationStatus == InvitationStatusEnum.Accept)
            {
                await _messageService.Create(invitation.SenderId, invitation.ActivityId, MessageTypeEnum.Accept);
                await _messageService.Create(invitation.ReceiverId, invitation.ActivityId, MessageTypeEnum.WasAccepted);
            }
            else if (updateInvitationDto.InvitationStatus == InvitationStatusEnum.Decline)
            {
                await _messageService.Create(invitation.SenderId, invitation.ActivityId, MessageTypeEnum.Decline);
                await _messageService.Create(invitation.ReceiverId, invitation.ActivityId, MessageTypeEnum.WasDeclined);

            }

            return await _invitationRepository.Update(invitation);
        }

        public async Task<ICollection<InvitationDto>> GetAllByRecieverId(int recieverId)
        {
            var invitations = (await _invitationRepository.GetAllByRecieverId(recieverId)).ToArray();
            var invitationsDto = _mapper.Map<InvitationDto[]>(invitations);

            for (int i = 0; i < invitations.Length; i++)
            {
                invitationsDto[i].Message = await _messageFormationService
                    .GetFormattedText((
                    await _messageTemplateRepository.GetById(_messageService.GetMessageId(MessageTypeEnum.GotNewInvitation))).TextTemplate,
                    invitations[i].ReceiverId,
                    invitations[i].ActivityId);
            }

            return invitationsDto;
        }

        public async Task<int> Create(NewInvitationDto newInvitationDto)
        {
            var user = await _userRepository.GetByName(newInvitationDto.ReceiverName);

            if (user == null)
            {
                throw new ArgumentNullException("User does not exist");
            }

            var invitation = _mapper.Map<Invitation>(newInvitationDto);
            invitation.ReceiverId = user.Id;

            await _messageService.Create(invitation.ReceiverId, invitation.ActivityId, MessageTypeEnum.GotNewInvitation);
            await _messageService.Create(invitation.SenderId, invitation.ActivityId, MessageTypeEnum.WasSent);

            return await _invitationRepository.Create(invitation);
        }
    }
}
