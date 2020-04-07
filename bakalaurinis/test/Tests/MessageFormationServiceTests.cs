using bakalaurinis.Infrastructure.Repositories;
using bakalaurinis.Services;
using Xunit;

namespace test.Tests
{
    public class MessageFormationServiceTests
    {
        private readonly MessageFormationService _messageFormationService;
        public MessageFormationServiceTests()
        {
            var setUp = new SetUp();
            setUp.Initialize();

            var context = setUp.DatabaseContext;

            var userRepository = new UsersRepository(context);
            var worksRepository = new WorksRepository(context);

            _messageFormationService = new MessageFormationService(userRepository, worksRepository);
        }

        [Theory]
        [InlineData("Vartotojas [user] pakvietė jus i veiką [activity]!", 1, 1,
            "Vartotojas test1 pakvietė jus i veiką testWork1!")]
        [InlineData("Vartotojas [user] atmetė jūsų pakvietimą į renginį [activity]!", 1, 1,
            "Vartotojas test1 atmetė jūsų pakvietimą į renginį testWork1!")]
        [InlineData("Vartotojas [user] priėmė jūsų pakvietimą į renginį [activity]!", 1, 1,
            "Vartotojas test1 priėmė jūsų pakvietimą į renginį testWork1!")]
        [InlineData("Jūs atmetėte kvietimą vartotojo [user] pakvietimą į renginį [activity]!", 1, 1,
            "Jūs atmetėte kvietimą vartotojo test1 pakvietimą į renginį testWork1!")]
        [InlineData("Jūs priėmėte kvietimą vartotojo [user] pakvietimą į renginį [activity]!", 1, 1,
            "Jūs priėmėte kvietimą vartotojo test1 pakvietimą į renginį testWork1!")]
        [InlineData("Jūs pakvietėtę [user] į veiką [activity]!", 1, 1,
            "Jūs pakvietėtę test1 į veiką testWork1!")]
        public async void GetFormattedText_MessageText(string message, int userId, int activityId, string resultMessage)
        {
            var messageAfterFormation = await _messageFormationService.GetFormattedText(message, userId, activityId);

            Assert.Equal(messageAfterFormation, resultMessage);
        }

    }
}
