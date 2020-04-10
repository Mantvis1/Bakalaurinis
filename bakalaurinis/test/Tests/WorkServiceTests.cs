using bakalaurinis.Dtos.Activity;
using bakalaurinis.Infrastructure.Repositories;
using bakalaurinis.Services;
using bakalaurinis.Services.Interfaces;
using Moq;
using Xunit;

namespace test.Tests
{
    public class WorkServiceTests
    {
        private readonly WorksService _worksService;
        private readonly int _count;

        public WorkServiceTests()
        {
            var setUp = new SetUp();
            setUp.Initialize();

            var context = setUp.DatabaseContext;
            var mapper = setUp.Mapper;
            _count = setUp.GetLength("works");

            var worksRepository = new WorksRepository(context);
            var scheduleGenerationService = new Mock<IScheduleGenerationService>().Object;
            var mockMessageService = new Mock<IMessageService>().Object;

            _worksService = new WorksService(worksRepository, mapper, scheduleGenerationService, mockMessageService);
        }

        [Fact]
        public async void GetAll_CountsAreEqual()
        {
            var realCount = (await _worksService.GetAll()).Count;

            Assert.True(_count == realCount);
        }

        [Theory]
        [InlineData(1)]
        public async void GetById_WorkNotNull(int id)
        {
            var workDto = await _worksService.GetById(id);

            Assert.NotNull(workDto);
        }

        [Theory]
        [InlineData(-1)]
        public async void GetById_WorkNotExist(int id)
        {
            var workDto = await _worksService.GetById(id);

            Assert.Null(workDto);
        }

        [Fact]
        public async void Create_WorkWasCreated()
        {
            var newWorkDto = new NewActivityDto()
            {
                UserId = 2,
                DurationInMinutes = 20,
                Title = "title",
                Description = "description",
                ActivityPriority = bakalaurinis.Infrastructure.Enums.ActivityPriorityEnum.Low
            };

            var workId = await _worksService.Create(newWorkDto);
            var workDto = await _worksService.GetById(workId);

            Assert.NotNull(workDto);
        }

        [Theory]
        [InlineData(2, 1)]
        public async void GetByUserId_BothLengthsAreEqual(int id, int expectedCount)
        {
            var worksDto = await _worksService.GetByUserId(id);

            Assert.True(worksDto.Count == expectedCount);
        }

        [Theory]
        [InlineData(1)]
        public async void Delete_CountReducedByOne(int id)
        {
            var expectedCount = (await _worksService.GetAll()).Count;
            await _worksService.Delete(id);

            var realValue = (await _worksService.GetAll()).Count;

            Assert.True(expectedCount - 1 == realValue);
        }

        [Theory]
        [InlineData(1, "updatedTitle")]
        public async void Update_ValueHasChanged(int id, string title)
        {
            var currentWork = await _worksService.GetById(id);

            var newWork = new NewActivityDto
            {
                UserId = id,
                Title = "updatedTitle"
            };

            await _worksService.Update(id, newWork);

            var updatedWork = await _worksService.GetById(id);

            Assert.NotEqual(currentWork.Title, updatedWork.Title);
            Assert.Equal(updatedWork.Title, title);
        }
    }
}
