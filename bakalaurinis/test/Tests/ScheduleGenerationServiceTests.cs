using System;
using System.Collections.Generic;
using System.Linq;
using bakalaurinis.Dtos.Work;
using bakalaurinis.Infrastructure.Database;
using bakalaurinis.Infrastructure.Repositories;
using bakalaurinis.Services;
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
        private readonly IWorksService _worksService;
        public ScheduleGenerationServiceTests()
        {
            var setUp = new SetUp();
            setUp.Initialize();

            _context = setUp.DatabaseContext;
            var mapper = setUp.Mapper;
            var mockTimeService = new Mock<ITimeService>();
            mockTimeService.Setup(m => m.GetCurrentDay()).Returns(DateTime.MinValue);

            var worksRepository = new WorksRepository(_context);
            var userSettingsRepository = new UserSettingsRepository(_context);
            var mockMessageService = new Mock<IMessageService>().Object;
            _worksService = new WorksService(worksRepository, mapper, mockMessageService);
            _scheduleGenerationService = new ScheduleGenerationService(worksRepository, mockTimeService.Object, mapper, userSettingsRepository, mockMessageService,null);
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

            await _scheduleGenerationService.CalculateActivitiesTime(userId, DateTime.MinValue, updateActivitiesDto);

            var worksAfterUpdate =
                (await _worksService.GetByUserId(userId)).Where(x => x.StartTime == null ||
                                                                     x.StartTime.Value.Day == DateTime.MinValue.Day).ToList();

            Assert.Equal(works.First().Title, worksAfterUpdate.First().Title);
        }

        [Theory]
        [InlineData(1, 1)]
        public async void CreateWorkCopy(int userId, int workId)
        {
            var work = _context.Works.Find(workId);
            await _scheduleGenerationService.CreateWorkCopy(userId, workId);

            var newWork = _context.Works.LastOrDefault();

            Assert.NotNull(newWork);
            Assert.Equal(work.Title, newWork.Title);
        }
    }
}
