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
        private readonly IScheduleGenerationService _scheduleGenerationService;

        public InvitationService(
            IInvitationRepository invitationRepository,
            IMapper mapper,
            IUserRepository userRepository,
            IMessageService messageService,
            IRepository<MessageTemplate> messageTemplateRepository,
            IMessageFormationService messageFormationService,
            IScheduleGenerationService scheduleGenerationService
            )
        {
            _invitationRepository = invitationRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _messageService = messageService;
            _messageTemplateRepository = messageTemplateRepository;
            _messageFormationService = messageFormationService;
            _scheduleGenerationService = scheduleGenerationService;
        }

        public async Task<bool> Update(int invitationId, UpdateInvitationDto updateInvitationDto)
        {
            var invitation = await _invitationRepository.GetById(invitationId);

            if (updateInvitationDto.InvitationStatus == InvitationStatusEnum.Accept)
            {
                await _messageService.Create(invitation.SenderId, invitation.WorkId, MessageTypeEnum.Accept);
                await _messageService.Create(invitation.ReceiverId, invitation.WorkId, MessageTypeEnum.WasAccepted);

                await _scheduleGenerationService.CreateWorkCopy(invitation.WorkId, invitation.ReceiverId);
            }
            else if (updateInvitationDto.InvitationStatus == InvitationStatusEnum.Decline)
            {
                await _messageService.Create(invitation.SenderId, invitation.WorkId, MessageTypeEnum.Decline);
                await _messageService.Create(invitation.ReceiverId, invitation.WorkId, MessageTypeEnum.WasDeclined);
            }

            invitation = _mapper.Map<Invitation>(updateInvitationDto);

            return await _invitationRepository.Update(invitation);
        }

        public async Task<ICollection<InvitationDto>> GetAllByReceiverId(int receiverId)
        {
            var invitations = (await _invitationRepository.GetAllByReceiverId(receiverId)).ToArray();
            var invitationsDto = _mapper.Map<InvitationDto[]>(invitations);

            for (int i = 0; i < invitations.Length; i++)
            {
                invitationsDto[i].Message = await _messageFormationService
                    .GetFormattedText((
                    await _messageTemplateRepository.GetById(_messageService.GetMessageId(MessageTypeEnum.GotNewInvitation))).TextTemplate,
                    invitations[i].ReceiverId,
                    invitations[i].WorkId);
            }

            return invitationsDto;
        }

        public async Task<int> Create(NewInvitationDto newInvitationDto)
        {
            var user = await _userRepository.GetByName(newInvitationDto.ReceiverName);

            if (user == null)
            {
                throw new ArgumentNullException();
            }

            var invitation = _mapper.Map<Invitation>(newInvitationDto);
            invitation.ReceiverId = user.Id;

            await _messageService.Create(invitation.ReceiverId, invitation.WorkId, MessageTypeEnum.GotNewInvitation);
            await _messageService.Create(invitation.SenderId, invitation.WorkId, MessageTypeEnum.WasSent);

            return await _invitationRepository.Create(invitation);
        }

        public async Task<bool> Delete(int id)
        {
            var invitation = await _invitationRepository.GetById(id);

            return await _invitationRepository.Delete(invitation);
        }
    }
}
