using bakalaurinis.Infrastructure.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Infrastructure.Repositories.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<ICollection<Message>> GetAllByUserId(int userId);
    }
}
