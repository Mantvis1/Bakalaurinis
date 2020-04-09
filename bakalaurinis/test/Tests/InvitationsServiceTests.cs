using System;
using System.Collections.Generic;
using System.Text;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Repositories;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services;
using bakalaurinis.Services.Interfaces;
using Moq;

namespace test.Tests
{
    public class InvitationsServiceTests
    {
        private readonly InvitationService _invitationService;
        private readonly int _count;

        public InvitationsServiceTests()
        {
            var setUp = new SetUp();
            setUp.Initialize();

            var context = setUp.DatabaseContext;
            var mapper = setUp.Mapper;
            _count = setUp.GetLength("messages");

            var mockUserRepository = new Mock<IUserRepository>().Object;
            var mockMessageService = new Mock<IMessageService>().Object;
            var mockMessageRepository = new Mock<IRepository<MessageTemplate>>().Object;
            var mockMessageFormationService = new Mock<IMessageFormationService>().Object;
            var mockScheduleGenerationService = new Mock<IScheduleGenerationService>().Object;

            var invitationRepository = new InvitationRepository(context);
            _invitationService = new InvitationService(invitationRepository, mapper, mockUserRepository,
                mockMessageService, mockMessageRepository, mockMessageFormationService, mockScheduleGenerationService);
        }
    }
}
