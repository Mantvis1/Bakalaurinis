using AutoMapper;
using bakalaurinis.Configurations;
using bakalaurinis.Dtos.User;
using bakalaurinis.Infrastructure.Database;
using bakalaurinis.Infrastructure.Repositories;
using bakalaurinis.Services;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace test.Tests
{
    public class UserTests
    {
        private readonly DatabaseContext _context;
        private readonly UserService _userService;
        private readonly IMapper _mapper;
        private readonly int usersCount;

        public UserTests()
        {
            var setUp = new SetUp();
            setUp.Initialize();

            _context = setUp.DatabaseContext;
            _mapper = setUp.Mapper;
            usersCount = setUp.GetLength("users");

            var userRepository = new UsersRepository(_context);
            var appSettings = Options.Create(new AppSettings());
            var userSettingsRepository = new UserSettingsRepository(_context);
            var invitationsRepository = new InvitationRepository(_context);
            var userSettingsService = new UserSettingsService(_mapper, userSettingsRepository);

            _userService = new UserService(appSettings, _mapper, userRepository, userSettingsService, invitationsRepository);
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

            Assert.True(usersDto.Count == usersCount);
        }

        [Theory]
        [InlineData("test1", "testPassword")]
        public async void Authenticate_UserId(string username, string password)
        {
            var authenticateDto = new AuthenticateDto()
            {
                Username = username,
                Password = password
            };

            var afterAutentificationDto = await _userService.Authenticate(authenticateDto);

            Assert.NotNull(afterAutentificationDto);
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

            var afterAutentificationDto = await _userService.Authenticate(authenticateDto);

            Assert.Null(afterAutentificationDto);
        }
    }
}
