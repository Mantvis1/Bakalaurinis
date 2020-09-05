using bakalaurinis.Dtos.Work;
using bakalaurinis.Infrastructure.Database;
using bakalaurinis.Infrastructure.Repositories;
using bakalaurinis.Services;
using bakalaurinis.Services.Generation;
using bakalaurinis.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace test.Tests.GenerationTests
{
    public class NewJobGenerationServiceTests
    {
        private readonly NewJobGenerationService _newJobGenerationService;
        private readonly DatabaseContext _context;
        private readonly IWorksService _worksService;

        public NewJobGenerationServiceTests()
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
            _worksService = new WorksService(worksRepository, mapper, mockMessageService);
            var factoryService = new Factory(mockTimeService.Object);

            _newJobGenerationService = new NewJobGenerationService(worksRepository, mockTimeService.Object, mapper, userSettingsRepository);
        }

        [Theory]
        [InlineData(1)]
        public async void CalculateActivitiesTime(int userId)
        {
            var works = (await _worksService.GetByUserId(userId)).Where(x => x.StartTime == null ||
                x.StartTime.Value.Day == DateTime.MinValue.Day).ToList();

            var updateActivitiesDto = new UpdateWorkDto
            {
                Activities = new List<WorkDto>()
            };

            foreach (var work in works)
            {
                if (work.Id != 1)
                {
                    updateActivitiesDto.Activities.Add(work);
                }
            }

            updateActivitiesDto.Activities.Add(works.First());

            await _newJobGenerationService.CalculateActivitiesTime(userId, DateTime.MinValue, updateActivitiesDto);

            var worksAfterUpdate =
                (await _worksService.GetByUserId(userId)).Where(x => x.StartTime == null ||
                                                                     x.StartTime.Value.Day == DateTime.MinValue.Day).ToList();

            Assert.Equal(works.First().Title, worksAfterUpdate.First().Title);
        }
    }
}
