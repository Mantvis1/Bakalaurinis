using System.Threading.Tasks;

namespace bakalaurinis.Helpers.Interfaces
{
    public interface IWorkCopyService
    {
        Task CreateWorkCopy(int workId, int userId);
    }
}
