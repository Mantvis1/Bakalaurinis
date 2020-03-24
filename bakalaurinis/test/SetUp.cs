using AutoMapper;
using bakalaurinis.Configurations;
using bakalaurinis.Infrastructure.Database;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace test
{
    class SetUp
    {
        private TimeService _timeService = new TimeService();
        private Work[] _works;
        private User[] _users;
        private Invitation[] _invitations;
        private UserSettings[] _userSettings;
        private Message[] _messages;
        private MessageTemplate[] _messageTemplates;

        private DatabaseContext _context;
        public DatabaseContext DatabaseContext =>
            _context ??
            throw new InvalidOperationException("Run initialize method before accessing this property.");

        private IMapper _mapper;
        public IMapper Mapper =>
            _mapper ??
            throw new InvalidOperationException("Run initialize method before accessing this property.");

        public void Initialize()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            var configuration = GetConfiguration();
            _context = new DatabaseContext(options);
            Seed(_context);

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfiguration());
            });
            _mapper = config.CreateMapper();
        }

        public Microsoft.Extensions.Configuration.IConfiguration GetConfiguration()
        {
            var config = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .Build();
            return config;
        }

        private async void Seed(DatabaseContext context)
        {
            var config = GetConfiguration();

            _users = new[] {
            new User
            {
                Id = 1,
                Username = "test1",
                Password = "testPassword"
            },
            new User
            {
                Id = 2,
                Username = "test2",
                Password = "testPassword2"
            }
            };

            context.Users.AddRange(_users);

            context.SaveChanges();
        }

        public int GetLength(string type)
        {
            switch (type)
            {
                case "works": return _works.Length;
                case "users": return _users.Length;
                default: return 0;
            }
        }
    }
}
