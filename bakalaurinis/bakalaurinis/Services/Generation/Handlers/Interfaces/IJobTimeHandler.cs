using System.Threading.Tasks;

namespace bakalaurinis.Services.Generation.Interfaces
{
   public interface IJobTimeHandler<T> where T : class
    {
        IJobTimeHandler<T> SetNext(IJobTimeHandler<T> handler);
        Task Handle(T request, int userId, Time time);
    }
}
