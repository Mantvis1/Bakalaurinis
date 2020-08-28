using AutoMapper;
using bakalaurinis.Dtos.Message;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Enums;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<MessageTemplate> _messageTempalateRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly IMessageFormationService _messageFormationService;

        public MessageService(
            IRepository<MessageTemplate> messageTempalateRepository,
            IMessageRepository messageRepository,
            IMapper mapper,
            IMessageFormationService messageFormationService
            )
        {
            _messageTempalateRepository = messageTempalateRepository;
            _messageRepository = messageRepository;
            _mapper = mapper;
            _messageFormationService = messageFormationService;
        }

        public async Task<int> Create(int userId, int workId, MessageTypeEnum messageType)
        {
            var messageId = GetMessageId(messageType);
            var messageTemplate = await _messageTempalateRepository.GetById(messageId);

            var message = new Message
            {
                CreatedAt = DateTime.Now,
                Title = messageTemplate.TitleTemplate,
                UserId = userId,
                Text = await _messageFormationService.GetFormattedText(messageTemplate.TextTemplate, userId, workId)
            };

            return await _messageRepository.Create(message);
        }

        public async Task Delete(int userId)
        {
            var messages = await _messageRepository.GetAllByUserId(userId);

            foreach (var message in messages)
            {
                await _messageRepository.Delete(message);
            }
        }

        public async Task DeleteById(int messageId)
        {
            var message = await _messageRepository.GetById(messageId);

            await _messageRepository.Delete(message);
        }

        public async Task<ICollection<MessageDto>> GetAll(int userId)
        {
            var messages = await _messageRepository.GetAllByUserId(userId);
            var messagesDto = _mapper.Map<MessageDto[]>(messages);

            return messagesDto;
        }

        public int GetMessageId(MessageTypeEnum messageType)
        {
            return messageType switch
            {
                MessageTypeEnum.NewWork => 1,
                MessageTypeEnum.DeleteWork => 2,
                MessageTypeEnum.Generation => 3,
                MessageTypeEnum.GotNewInvitation => 4,
                MessageTypeEnum.Decline => 5,
                MessageTypeEnum.Accept => 6,
                MessageTypeEnum.WasDeclined => 7,
                MessageTypeEnum.WasAccepted => 8,
                MessageTypeEnum.WasSent => 9,
                _ => 0,
            };
        }
    }
}
