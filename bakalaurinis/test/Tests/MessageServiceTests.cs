using bakalaurinis.Infrastructure.Enums;
using bakalaurinis.Infrastructure.Repositories;
using bakalaurinis.Services;
using bakalaurinis.Services.Interfaces;
using Moq;
using Xunit;

namespace test.Tests
{
    public class MessageServiceTests
    {
        private readonly MessageService _messageService;
        private readonly int _count;

        public MessageServiceTests()
        {
            var setUp = new SetUp();
            setUp.Initialize();

            var context = setUp.DatabaseContext;
            var mapper = setUp.Mapper;
            _count = setUp.GetLength("messages");

            var messageTemplateRepository = new MessageTemplateRepository(context);
            var mockMessageFormationService = new Mock<IMessageFormationService>().Object;

            var messageRepository = new MessageRepository(context);
            _messageService = new MessageService(messageTemplateRepository, messageRepository, mapper, mockMessageFormationService);
        }

        [Theory]
        [InlineData(1)]
        public async void GetAllByUserId_CountsAreEqual(int id)
        {
            int actualValue = (await _messageService.GetAll(id)).Count;

            Assert.True(_count == actualValue);
        }

        [Theory]
        [InlineData(MessageTypeEnum.Accept, 6)]
        [InlineData(MessageTypeEnum.Decline, 5)]
        [InlineData(MessageTypeEnum.DeleteWork, 2)]
        [InlineData(MessageTypeEnum.Generation, 3)]
        [InlineData(MessageTypeEnum.GotNewInvitation, 4)]
        [InlineData(MessageTypeEnum.NewWork, 1)]
        [InlineData(MessageTypeEnum.WasAccepted, 8)]
        [InlineData(MessageTypeEnum.WasDeclined, 7)]
        [InlineData(MessageTypeEnum.WasSent, 9)]
        public void GetMessageId_CorrectIndex(MessageTypeEnum type, int expectedValue)
        {
            var actualValue = _messageService.GetMessageId(type);

            Assert.True(actualValue == expectedValue);
        }

        [Theory]
        [InlineData(1)]
        public async void DeleteMessageById_MessageWasDeleted(int id)
        {
            await _messageService.DeleteById(id);

            var countAfterDeletion = (await _messageService.GetAll(id)).Count;

            Assert.True(_count - 1 == countAfterDeletion);
        }

        [Theory]
        [InlineData(1)]
        public async void DeleteUserById_MessageWasDeleted(int id)
        {
            await _messageService.Delete(id);

            int countAfterDeletion = (await _messageService.GetAll(id)).Count;

            Assert.True(_count - 1 == countAfterDeletion);
        }

        [Theory]
        [InlineData(1, 1, MessageTypeEnum.Accept)]
        public async void Create_MessageWasCreated(int userId, int messageId, MessageTypeEnum type)
        {
            await _messageService.Create(userId, messageId, type);
            var actualValue = (await _messageService.GetAll(userId)).Count;

            Assert.True(actualValue == _count + 1);
        }
    }
}
