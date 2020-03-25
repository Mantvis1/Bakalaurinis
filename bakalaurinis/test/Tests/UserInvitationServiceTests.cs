using AutoMapper;
using bakalaurinis.Dtos.UserSettings;
using bakalaurinis.Infrastructure.Database;
using bakalaurinis.Infrastructure.Repositories;
using bakalaurinis.Services;
using Xunit;

namespace test.Tests
{
    public class UserInvitationServiceTests
    {
        private readonly DatabaseContext _context;
        private readonly UserInvitationService _userInvitationService;

        public UserInvitationServiceTests()
        {
            var setUp = new SetUp();
            setUp.Initialize();

            _context = setUp.DatabaseContext;

            var _mapper = setUp.Mapper;
            var userRepository = new UsersRepository(_context);
            var invitationsRepository = new InvitationRepository(_context);

            _userInvitationService = new UserInvitationService(invitationsRepository, userRepository, _mapper);
        }

        [Theory]
        [InlineData(1, 1)]
        public async void GetAllByActivityId_ArrayOfInvitation(int id, int count)
        {
            var realCount = (await _userInvitationService.GetAllByActivityId(id)).Length;

            Assert.True(count == realCount);
        }

        [Theory]
        [InlineData(1, "test2")]
        public async void GetAllByActivityId_CorrectUserName(int id, string expected)
        {
            var actual = (await _userInvitationService.GetAllByActivityId(id))[0].Username;

            Assert.Equal(expected, actual);
        }


    }
}
