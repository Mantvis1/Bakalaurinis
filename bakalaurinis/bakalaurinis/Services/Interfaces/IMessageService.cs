using bakalaurinis.Dtos.Message;
using bakalaurinis.Infrastructure.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Interfaces
{
    public interface IMessageService
    {
        Task<ICollection<MessageDto>> GetAll(int userId);
        Task Delete(int messageId);
        Task DeleteById(int userId);
        Task<int> Create(int userId, int activityId, MessageTypeEnum messageType);
        Task<MessageDto> GetById(MessageTypeEnum messageType);
        int GetMessageId(MessageTypeEnum messageType);
    }
}
