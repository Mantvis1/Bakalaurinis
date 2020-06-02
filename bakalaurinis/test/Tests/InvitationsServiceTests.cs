using System.Linq;
using bakalaurinis.Dtos.Invitation;
using bakalaurinis.Infrastructure.Database;
using bakalaurinis.Infrastructure.Enums;
using bakalaurinis.Infrastructure.Repositories;
using bakalaurinis.Services;
using bakalaurinis.Services.Interfaces;
using Moq;
using Xunit;

namespace test.Tests
{
    public class InvitationsServiceTests
    {
        private readonly InvitationService _invitationService;
        private readonly int _count;
        private readonly DatabaseContext _context;
        public InvitationsServiceTests()
        {
            var setUp = new SetUp();
            setUp.Initialize();

            _context = setUp.DatabaseContext;
            var mapper = setUp.Mapper;
            _count = setUp.GetLength("invitation");

            var userRepository = new UsersRepository(_context);

            var mockMessageService = new Mock<IMessageService>();
            mockMessageService.Setup(x => x.GetMessageId(MessageTypeEnum.GotNewInvitation)).Returns(4);

            var messageTemplateRepository = new MessageTemplateRepository(_context);
            var messageFormationService = new Mock<IMessageFormationService>();
            var mockScheduleGenerationService = new Mock<IScheduleGenerationService>().Object;

            var invitationRepository = new InvitationRepository(_context);
            _invitationService = new InvitationService(invitationRepository, mapper, userRepository,
                mockMessageService.Object, messageTemplateRepository, messageFormationService.Object, mockScheduleGenerationService);
        }

        [Fact]
        public async void Create()
        {
            var newInvitationDto = new NewInvitationDto
            {
                WorkId = 1,
                SenderId = 2,
                ReceiverName = "test1"
            };

            var invitationId = await _invitationService.Create(newInvitationDto);
            var invitation = _context.Invitations.FirstOrDefault(x => x.Id == invitationId);

            Assert.NotNull(invitation);
        }

        [Theory]
        [InlineData(1, 1,InvitationStatusEnum.Accept)]
        [InlineData(1, 1, InvitationStatusEnum.Decline)]
        public async void Update(int id, int workId, InvitationStatusEnum status)
        {
            var invitationDto = await _context.Invitations.FindAsync(id);

            var updateInvitationDto = new UpdateInvitationDto
            {
                WorkId = workId,
                InvitationStatus = status
            };

            await _invitationService.Update(id, updateInvitationDto);

            Assert.True(invitationDto.InvitationStatus == (await _context.Invitations.FindAsync(id)).InvitationStatus);
        }

        [Theory]
        [InlineData(1)]
        public async void GetAllByReceiverId(int receiverId)
        {
            var actualCount = (await _invitationService.GetAllByReceiverId(receiverId)).Count;

            Assert.Equal(_count, actualCount);
        }

        [Theory]
        [InlineData(1)]
        public async void Delete(int id)
        {
            await _invitationService.Delete(id);
            var invitation = _context.Invitations.FirstOrDefault(x => x.Id == id);

            Assert.Null(invitation);
        }
    }
}
