using bakalaurinis.Services.Generation.Interfaces;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Generation
{
    public abstract class JobTimeHandler<T> : IJobTimeHandler<T> where T : class
    {
        private IJobTimeHandler<T> Next { get; set; }

        public JobTimeHandler()
        {
        }

        public virtual async Task Handle(T request, int userId, Time time)
        {
            await Next?.Handle(request, userId,time);
        }

        public IJobTimeHandler<T> SetNext(IJobTimeHandler<T> next)
        {
            Next = next;

            return Next;
        }
    }
}
