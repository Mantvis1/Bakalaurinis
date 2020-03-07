using bakalaurinis.Dtos.Message;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Interfaces
{
    public interface IMessageService
    {
        Task<ICollection<MessageDto>> GetAll(int userId);
        Task<MessageDto> GetById(int userId);
        Task Delete(int userId, int messageId);
        Task Delete(int userId);
        Task<int> Create(int userId, int messageType);
    }
}
