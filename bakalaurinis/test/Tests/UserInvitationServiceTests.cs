using bakalaurinis.Infrastructure.Repositories;
using bakalaurinis.Services;
using Xunit;

namespace test.Tests
{
    public class UserInvitationServiceTests
    {
        private readonly UserInvitationService _userInvitationService;

        public UserInvitationServiceTests()
        {
            var setUp = new SetUp();
            setUp.Initialize();

            var context = setUp.DatabaseContext;
            var mapper = setUp.Mapper;
            var userRepository = new UsersRepository(context);
            var invitationsRepository = new InvitationRepository(context);

            _userInvitationService = new UserInvitationService(invitationsRepository, userRepository, mapper);
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
