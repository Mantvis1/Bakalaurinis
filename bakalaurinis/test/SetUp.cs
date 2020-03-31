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

            _userSettings = new[] {
            new UserSettings
            {
                Id = 1,
                UserId = 1,
                StartTime = 8,
                EndTime = 10,
                ItemsPerPage = 5
            }
            };
            context.AddRange(_userSettings);

            _works = new[] {
            new Work
            {
                Id = 1,
                UserId = 1,
                Title = "testWork1"

            },
            new Work
            {
                Id = 2,
                UserId = 2,
                Title = "testWork2"
            }
            };
            context.AddRange(_works);

            _invitations = new[] {
            new Invitation
            {
                Id = 1,
                WorkId =1,
                SenderId =1,
                ReceiverId =2
            },
             new Invitation
            {
                Id = 2,
                WorkId =2,
                SenderId =2,
                ReceiverId =1
            }
            };
            context.AddRange(_invitations);

            _messageTemplates = new[]
                {
                new MessageTemplate
                {
                    Id =1,
                    TextTemplate = "Jūs sukūrėte nauja veiklą [activity]!",
                    TitleTemplate = "Veiklos sukūrimas"

                },
                new MessageTemplate
                {
                    Id =2,
                    TextTemplate = "Jūs pašalinote veiklą [activity]!",
                    TitleTemplate = "Veiklos šalinimas"
                },
                new MessageTemplate
                {
                    Id =3,
                    TextTemplate = "Sistema atliko naują tvarkaraščio generavimą!",
                    TitleTemplate = "Tvarkataščio generavimas atliktas"
                },
                new MessageTemplate
                {
                    Id =4,
                    TextTemplate = "Jūs sukūrėte nauja veiklą [activity]!",
                    TitleTemplate = "Naujas kvietimas gautas"
                },
                new MessageTemplate
                {
                    Id =5,
                    TextTemplate = "Vartotojas [user] atmetė jūsų pakvietimą į renginį [activity]!",
                    TitleTemplate = "Kvietimas atmestas"
                },
                new MessageTemplate
                {
                    Id =6,
                     TextTemplate = "Vartotojas [user] priėmė jūsų pakvietimą į renginį [activity]!",
                    TitleTemplate = "Kvietimas priimtas"
                },
                new MessageTemplate
                {
                    Id =7,
                     TextTemplate = "Jūs atmetėte kvietimą vartotojo [user] pakvietimą į renginį [activity]!",
                    TitleTemplate = "Jūs atmetėte kvietimą"
                },
                new MessageTemplate
                {
                    Id =8,
                    TextTemplate = "Jūs priėmėte kvietimą vartotojo [user] pakvietimą į renginį [activity]!",
                    TitleTemplate = "Jūs priėmėte kvietimą"
                },
                new MessageTemplate
                {
                    Id =9,
                    TextTemplate = "Vartotojas [user] pakvietė jus i veiką [activity]!",
                    TitleTemplate = "Naujas kvietimas iššiūstas"
                }
            };
            _context.MessageTemplates.AddRange(_messageTemplates);

            _messages = new[]
            {
                new Message
                {
                    Id = 1,
                    Text = "Some text",
                    Title = "Title",
                    UserId = 1,
                    CreatedAt = DateTime.Now
                }
            };
            _context.Messages.AddRange(_messages);

            context.SaveChanges();
        }

        public int GetLength(string type)
        {
            switch (type)
            {
                case "works": return _works.Length;
                case "users": return _users.Length;
                case "invitation": return _invitations.Length;
                case "messages":return _messages.Length;
                default: return 0;
            }
        }
    }
}
