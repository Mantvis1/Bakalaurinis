using bakalaurinis.Helpers.Interfaces;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace bakalaurinis.Helpers
{
    public class WorkCopyService : IWorkCopyService
    {
        private readonly IWorksRepository _worksRepository;

        public WorkCopyService(IWorksRepository worksRepository)
        {
            _worksRepository = worksRepository;
        }

        public Work GetCopy(Work work)
        {
            return work.DeepCopy();
        }

        public async Task CreateWorkCopy(int workId, int userId)
        {
            var work = await _worksRepository.GetById(workId);

            var newWork = work.DeepCopy();
            newWork.UserId = userId;

            await _worksRepository.Create(newWork);
        }
    }
}
