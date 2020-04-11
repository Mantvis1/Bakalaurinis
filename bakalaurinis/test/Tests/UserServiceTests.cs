using bakalaurinis.Configurations;
using bakalaurinis.Dtos.User;
using bakalaurinis.Infrastructure.Repositories;
using bakalaurinis.Services;
using Microsoft.Extensions.Options;
using Xunit;

namespace test.Tests
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly int _usersCount;

        public UserServiceTests()
        {
            var setUp = new SetUp();
            setUp.Initialize();

            var context = setUp.DatabaseContext;
            var mapper = setUp.Mapper;
            _usersCount = setUp.GetLength("users");

            var userRepository = new UsersRepository(context);
            var appSettings = Options.Create(new AppSettings());
            var userSettingsRepository = new UserSettingsRepository(context);
            var invitationsRepository = new InvitationRepository(context);
            var userSettingsService = new UserSettingsService(mapper, userSettingsRepository);

            _userService = new UserService(appSettings, mapper, userRepository, userSettingsService, invitationsRepository);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetNameById_UserNameDto(int id)
        {
            var userNameDto = await _userService.GetNameById(id);

            Assert.NotNull(userNameDto);
        }

        [Theory]
        [InlineData(-1)]
        public async void GetNameById_Null(int id)
        {
            var userNameDto = await _userService.GetNameById(id);

            Assert.Null(userNameDto);
        }

        [Fact]
        public async void Register_NewUserId()
        {
            var registrationDto = new RegistrationDto()
            {
                Email = "newEmail@gmail.com",
                Password = "newPassword",
                Username = "newUsername"
            };

            int userId = await _userService.Register(registrationDto);
            var user = await _userService.GetNameById(userId);

            Assert.NotNull(user);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetStatusById_UserNameDto(int id)
        {
            var userStatusDto = await _userService.GetStatusById(id);

            Assert.NotNull(userStatusDto);
        }

        [Theory]
        [InlineData(-1)]
        public async void GetStatusById_Null(int id)
        {
            var userStatusDto = await _userService.GetNameById(id);

            Assert.Null(userStatusDto);
        }

        [Fact]
        public async void GetAll_CountIsCorrect()
        {
            var usersDto = await _userService.GetAll();

            Assert.True(usersDto.Count == _usersCount);
        }

        [Theory]
        [InlineData(1)]
        public async void Authenticate_UserId(int id)
        {
            await _userService.Delete(id);

            Assert.Null(await _userService.GetNameById(id));
        }

        [Theory]
        [InlineData("wrongUsername", "wrongPassword")]
        public async void Authenticate_Null(string username, string password)
        {
            var authenticateDto = new AuthenticateDto()
            {
                Username = username,
                Password = password
            };

            var afterAuthenticationDto = await _userService.Authenticate(authenticateDto);

            Assert.Null(afterAuthenticationDto);
        }
    }
}
