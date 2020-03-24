using AutoMapper;
using bakalaurinis.Configurations;
using bakalaurinis.Infrastructure.Database;
using bakalaurinis.Infrastructure.Repositories;
using bakalaurinis.Services;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace test.Tests
{
    public class UserTests
    {
        private readonly DatabaseContext _context;
        private readonly UserService _userService;
        private readonly IMapper _mapper;


        public UserTests()
        {
            var setUp = new SetUp();
            setUp.Initialize();
            _context = setUp.DatabaseContext;

            var _mapper = setUp.Mapper;
            var userRepository = new UsersRepository(_context);
            var appSettingsMock = new Mock<IOptions<AppSettings>>().Object;
            var userSettingsRepository = new UserSettingsRepository(_context);
            var invitationsRepository = new InvitationRepository(_context);

            var userSettingsService = new UserSettingsService(_mapper, userSettingsRepository);

            _userService = new UserService(appSettingsMock, _mapper, userRepository, userSettingsService, invitationsRepository);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetUserNameDtoById(int id)
        {
            var userNameDto = await _userService.GetNameById(id);

            Assert.NotNull(userNameDto);
        }
    }
}
