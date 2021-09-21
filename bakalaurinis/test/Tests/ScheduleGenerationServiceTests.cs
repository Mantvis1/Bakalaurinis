using System;
using System.Linq;
using bakalaurinis.Infrastructure.Database;
using bakalaurinis.Infrastructure.Repositories;
using bakalaurinis.Services;
using bakalaurinis.Services.Generation;
using bakalaurinis.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace test.Tests
{
    public class ScheduleGenerationServiceTests
    {
        private readonly ScheduleGenerationService _scheduleGenerationService;
        private readonly DatabaseContext _context;
        private readonly FreeSpaceSaver _freeSpaceSaver;

        public ScheduleGenerationServiceTests()
        {
            var setUp = new SetUp();
            setUp.Initialize();

            _context = setUp.DatabaseContext;
            var mapper = setUp.Mapper;
            var mockTimeService = new Mock<ITimeService>();
            mockTimeService.Setup(m => m.GetCurrentDay()).Returns(DateTime.MinValue);
            mockTimeService.Setup(m => m.GetDifferentBetweenTwoDatesInMinutes(DateTime.MinValue, DateTime.MaxValue)).Returns(50);

            var worksRepository = new WorksRepository(_context);
            var userSettingsRepository = new UserSettingsRepository(_context);
            var mockMessageService = new Mock<IMessageService>().Object;
            _freeSpaceSaver = new FreeSpaceSaver();
            var factoryService = new Factory(mockTimeService.Object);

            _scheduleGenerationService = new ScheduleGenerationService(worksRepository, mockTimeService.Object, userSettingsRepository, mockMessageService, factoryService, _freeSpaceSaver);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GenerateSchedule(int userId)
        {
            await _scheduleGenerationService.Generate(userId);

            var works = await _context.Works.Where(x => x.UserId == userId).ToListAsync();

            Assert.True(works.Count > 0);

            foreach (var work in works)
            {
                Assert.NotNull(work.StartTime);
                Assert.NotNull(work.EndTime);
            }
        }

       // [Fact]
        /*public void AddFreeSpaceIfTimeIsCorrect()
        {
            _scheduleGenerationService.AddFreeSpaceIfTimeIsCorrect(DateTime.MinValue, DateTime.MaxValue, new Time());

            Assert.NotEmpty(_freeSpaceSaver.GetAll());
        }*/
    }
}
