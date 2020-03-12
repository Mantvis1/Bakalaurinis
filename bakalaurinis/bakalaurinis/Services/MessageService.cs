using AutoMapper;
using bakalaurinis.Dtos.Message;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Enums;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bakalaurinis.Services
{
    public class MessageService : IMessageService
    {
        protected readonly IRepository<MessageTemplate> _messageTempalateRepository;
        protected readonly IMessageRepository _messageRepository;
        protected readonly IMapper _mapper;
        public MessageService(IRepository<MessageTemplate> messageTempalateRepository, IMessageRepository messageRepository, IMapper mapper)
        {
            _messageTempalateRepository = messageTempalateRepository;
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<int> Create(int userId, MessageTypeEnum messageType)
        {
            var messageId = SelectMessageId(messageType);
            var messageTemplate = await _messageTempalateRepository.GetById(messageId);

            var message = new Message
            {
                CreatedAt = DateTime.Now,
                Title = messageTemplate.TitleTemplate,
                UserId = userId,
                Text = EditMessage(messageTemplate.TextTemplate)
            };

            return await _messageRepository.Create(message);
        }

        private string EditMessage(string messageText)
        {
            var messageBuilder = new StringBuilder();

            return messageText;
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

        private int SelectMessageId(MessageTypeEnum messageType)
        {
            switch (messageType)
            {
                case MessageTypeEnum.NewActivity:
                    return 1;
                case MessageTypeEnum.DeleteActivity:
                    return 2;
                case MessageTypeEnum.Generation:
                    return 3;
                case MessageTypeEnum.GotNewInvitation:
                    return 4;
                case MessageTypeEnum.Decline:
                    return 5;
                case MessageTypeEnum.Accept:
                    return 6;
                case MessageTypeEnum.WasDeclined:
                    return 7;
                case MessageTypeEnum.WasAccepted:
                    return 8;
                case MessageTypeEnum.WasSent:
                    return 9;
                default:
                    return 0;
            }
        }
    }
}
