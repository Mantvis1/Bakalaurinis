using System;
using bakalaurinis.Infrastructure.Repositories;
using bakalaurinis.Services;
using bakalaurinis.Services.Interfaces;
using Moq;
using Xunit;

namespace test.Tests
{
    public class ScheduleServiceTests
    {
        private readonly ScheduleService _scheduleService;

        public ScheduleServiceTests()
        {
            var setUp = new SetUp();
            setUp.Initialize();

            var context = setUp.DatabaseContext;
            var mapper = setUp.Mapper;

            var worksRepository = new WorksRepository(context);
            var userSettingsRepository = new UserSettingsRepository(context);
            var mockTimeService = new Mock<ITimeService>().Object;
            var mockMessageService = new Mock<IMessageService>().Object;

            var scheduleGenerationService = new ScheduleGenerationService(worksRepository, mockTimeService, mapper, userSettingsRepository, mockMessageService,null);
            var userSettingsService = new UserSettingsService(mapper, userSettingsRepository, scheduleGenerationService);
            _scheduleService = new ScheduleService(worksRepository, mapper, userSettingsService);
        }

        [Theory]
        [InlineData(3)]
        public async void GetAllByUserId_CountsAreEquals(int id)
        {
            var scheduleLength = (await _scheduleService.GetAllByUserIdFilterByDate(id, DateTime.MinValue)).works.Count;

            Assert.True(scheduleLength == 1);
        }

        [Theory]
        [InlineData(3, 50)]
        public async void GetDayBusyness_CountsAreEquals(int id, int expectedValue)
        {
            var busynessInPersentage = await _scheduleService.GetBusyness(id, DateTime.MinValue);

            Assert.True(expectedValue == busynessInPersentage);
        }
    }
}
