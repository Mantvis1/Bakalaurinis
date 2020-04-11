using AutoMapper;
using bakalaurinis.Configurations;
using bakalaurinis.Infrastructure.Database;
using bakalaurinis.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using bakalaurinis.Infrastructure.Enums;
using static bakalaurinis.Infrastructure.Enums.ActivityPriorityEnum;

namespace test
{
    internal class SetUp
    {
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

            _context = new DatabaseContext(options);
            Seed(_context);

            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperConfiguration()); });
            _mapper = config.CreateMapper();
        }

        private void Seed(DatabaseContext context)
        {

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
            },
            new User
            {
                Id = 3,
                Username = "test3",
                Password = "testPassword3"
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
            },
            new UserSettings
            {
                Id = 2,
                UserId = 2,
                StartTime = 8,
                EndTime = 10,
                ItemsPerPage = 5
            }
            };
            context.UserSettings.AddRange(_userSettings);

            _works = new[] {
            new Work
            {
                Id = 1,
                UserId = 1,
                Title = "testWork1",
                StartTime = DateTime.MinValue.AddHours(8),
                EndTime = DateTime.MinValue.AddHours(9),
                DurationInMinutes = 60,
                ActivityPriority = High
            },
            new Work
            {
                Id = 2,
                UserId = 2,
                Title = "testWork2",
                DurationInMinutes = 15,
                ActivityPriority = Medium,
                StartTime = null,
                EndTime = null
            },
            new Work
            {
                Id = 3,
                UserId = 1,
                Title = "testWork3",
                DurationInMinutes = 45,
                ActivityPriority = Medium,
                StartTime = DateTime.MinValue.AddHours(9).AddMinutes(15),
                EndTime = DateTime.MinValue.AddHours(10)
            },
            new Work
            {
                Id = 4,
                UserId = 1,
                Title = "testWork4",
                DurationInMinutes = 15,
                ActivityPriority = Medium
            },
            new Work
            {
                Id = 5,
                UserId = 1,
                Title = "testWork5",
                DurationInMinutes = 45,
                ActivityPriority = Medium,
                StartTime = DateTime.MinValue.AddDays(1).AddHours(8).AddMinutes(15),
                EndTime = DateTime.MinValue.AddDays(1).AddHours(10)
            },
            new Work
            {
                Id = 6,
                UserId = 1,
                Title = "testWork6",
                DurationInMinutes = 15,
                ActivityPriority = Medium
            },
            new Work
            {
                Id = 7,
                UserId = 1,
                Title = "testWork7",
                DurationInMinutes = 45,
                ActivityPriority = Medium,
                StartTime = DateTime.MinValue.AddDays(2).AddHours(8).AddMinutes(15),
                EndTime = DateTime.MinValue.AddDays(2).AddHours(10)
            },
            new Work
            {
                Id = 8,
                UserId = 1,
                Title = "testWork6",
                DurationInMinutes = 15,
                ActivityPriority = Medium
            },
            new Work
            {
                Id = 9,
                UserId = 3,
                Title = "testWork8",
                DurationInMinutes = 45,
                ActivityPriority = Medium,
                StartTime = DateTime.MinValue.AddHours(8),
                EndTime = DateTime.MinValue.AddHours(10)
            },
            new Work
            {
                Id = 10,
                UserId = 1,
                Title = "testWork10",
                DurationInMinutes = 15,
                ActivityPriority = Medium
            },
            new Work
            {
                Id = 11,
                UserId = 1,
                Title = "testWork11",
                DurationInMinutes = 45,
                ActivityPriority = Medium,
                StartTime = DateTime.MinValue.AddDays(3).AddHours(8),
                EndTime = DateTime.MinValue.AddDays(3).AddHours(9).AddMinutes(45)
            },
            new Work
            {
                Id = 12,
                UserId = 1,
                Title = "testWork12",
                DurationInMinutes = 15,
                ActivityPriority = Medium,
                StartTime = DateTime.MinValue.AddDays(4).AddHours(8),
                EndTime = DateTime.MinValue.AddDays(4).AddHours(8).AddMinutes(15)

            },
            new Work
            {
                Id = 13,
                UserId = 1,
                Title = "testWork13",
                DurationInMinutes = 60,
                ActivityPriority = Medium,
                StartTime = DateTime.MinValue.AddDays(4).AddHours(9),
                EndTime = DateTime.MinValue.AddDays(4).AddHours(10)
            },
            new Work
            {
                Id = 14,
                UserId = 1,
                Title = "testWork14",
                DurationInMinutes = 30,
                ActivityPriority = Medium,
                StartTime = DateTime.MinValue.AddDays(4).AddHours(8).AddMinutes(30),
                EndTime = DateTime.MinValue.AddDays(4).AddHours(9)
            },
            new Work
            {
                Id = 15,
                UserId = 1,
                Title = "testWork15",
                DurationInMinutes = 60,
                ActivityPriority = High,
                StartTime = null,
                EndTime = null
            },
            new Work
            {
                Id = 16,
                UserId = 1,
                Title = "testWork16",
                DurationInMinutes = 22,
                ActivityPriority = Low,
                StartTime = null,
                EndTime = null
            },
            new Work
            {
                Id = 17,
                UserId = 1,
                Title = "testWork14",
                DurationInMinutes = 30,
                ActivityPriority = Medium,
                StartTime = DateTime.MinValue.AddDays(4).AddHours(8),
                EndTime = DateTime.MinValue.AddDays(4).AddHours(9).AddMinutes(30)
            },
            new Work
            {
                Id = 18,
                UserId = 1,
                Title = "testWork14",
                DurationInMinutes = 30,
                ActivityPriority = Medium,
                StartTime = DateTime.MinValue.AddDays(4).AddHours(8).AddMinutes(30),
                EndTime = DateTime.MinValue.AddDays(4).AddHours(9)
            },
            new Work
            {
                Id = 19,
                UserId = 1,
                Title = "testWork14",
                DurationInMinutes = 30,
                ActivityPriority = Medium,
                StartTime = DateTime.MinValue.AddDays(5).AddHours(9),
                EndTime = DateTime.MinValue.AddDays(5).AddHours(9).AddMinutes(30)
            },
            new Work
            {
                Id = 20,
                UserId = 1,
                Title = "testWork14",
                DurationInMinutes = 30,
                ActivityPriority = Medium,
                StartTime = DateTime.MinValue.AddDays(5).AddHours(9).AddMinutes(30),
                EndTime = DateTime.MinValue.AddDays(5).AddHours(10)
            },
            };

            context.Works.AddRange(_works);

            _invitations = new[] {
            new Invitation
            {
                Id = 1,
                WorkId =1,
                SenderId =2,
                ReceiverId =1,
                InvitationStatus = InvitationStatusEnum.Pending
            },
             new Invitation
            {
                Id = 2,
                WorkId =3,
                SenderId =2,
                ReceiverId =1,
                InvitationStatus = InvitationStatusEnum.Pending
            }
            };
            context.Invitations.AddRange(_invitations);

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
            if (type == "works")
                return _works.Length;
            else if (type == "users")
                return _users.Length;
            else if (type == "invitation")
                return _invitations.Length;
            else if (type == "messages")
                return _messages.Length;
            else
                return 0;
        }
    }
}
