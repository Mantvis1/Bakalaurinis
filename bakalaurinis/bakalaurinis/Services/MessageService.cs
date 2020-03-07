using AutoMapper;
using bakalaurinis.Dtos.Message;
using bakalaurinis.Infrastructure.Database.Models;
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

        public async Task<int> Create(int userId, int messageType)
        {
            var messageTemplate = await _messageTempalateRepository.GetById(messageType);
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
            throw new System.NotImplementedException();
        }

        public async Task Delete(int userId, int messageId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ICollection<MessageDto>> GetAll(int userId)
        {
            var messages = await _messageRepository.GetAllByUserId(userId);
            var messagesDto = _mapper.Map<MessageDto[]>(messages);

            return messagesDto;
        }

        public async Task<MessageDto> GetById(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
