using bakalaurinis.Helpers;
using bakalaurinis.Infrastructure.Database;
using bakalaurinis.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace test.Tests.HelpersTests
{
    public class WorkCopyTests
    {
        private readonly WorkCopyService _workCopyService;
        private readonly DatabaseContext _context;

        public WorkCopyTests()
        {
            var setUp = new SetUp();
            setUp.Initialize();

           _context = setUp.DatabaseContext;

            var worksRepository = new WorksRepository(_context);
            _workCopyService = new WorkCopyService(worksRepository);
        }

        [Fact]
        public async void CheckIfCopyIsNotASameObject()
        {
            var work = (await _context.Works.ToArrayAsync()).FirstOrDefault();
            var workCopy = work.DeepCopy();
            workCopy.Id = 100000;

            Assert.NotEqual(work.Id, workCopy.Id);
        }

        [Theory]
        [InlineData(1, 1)]
        public async void CreateWorkCopy(int userId, int workId)
        {
            var work = _context.Works.Find(workId);
            await _workCopyService.CreateWorkCopy(userId, workId);

            var newWork = _context.Works.LastOrDefault();

            Assert.NotNull(newWork);
            Assert.Equal(work.Title, newWork.Title);
        }
    }
}
