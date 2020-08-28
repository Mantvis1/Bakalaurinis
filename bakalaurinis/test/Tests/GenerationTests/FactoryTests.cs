using bakalaurinis.Infrastructure.Database;
using bakalaurinis.Services.Generation;
using bakalaurinis.Services.Interfaces;
using Moq;
using Xunit;

namespace test.Tests.GenerationTests
{
    public class FactoryTests
    {
        private readonly Factory _factory;

        public FactoryTests()
        {
            var setUp = new SetUp();
            setUp.Initialize();

            var mockTimeService = new Mock<ITimeService>();

            _factory = new Factory(mockTimeService.Object);
        }

        [Fact]
        public void GenerateObjectIsNotNull()
        {
            Assert.NotNull(_factory.GetGeneratedFreeSpace(new Time(22, 33), 0));
        }

    }
}
