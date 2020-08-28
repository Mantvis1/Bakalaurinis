using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System.Threading.Tasks;

namespace bakalaurinis.Services
{
    public class MessageFormationService : IMessageFormationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IWorksRepository _activitiesRepository;

        public MessageFormationService(IUserRepository userRepository, IWorksRepository activitiesRepository)
        {
            _userRepository = userRepository;
            _activitiesRepository = activitiesRepository;
        }

        public async Task<string> GetFormattedText(string message, int userId, int workId)
        {
            if (message.Contains("[work]"))
            {
                message = await AddWorkName(message, workId);
            }

            if (message.Contains("[user]"))
            {
                message = await AddUsername(message, userId);
            }

            return message;
        }

        private async Task<string> AddUsername(string message, int userId)
        {
            return message.Replace("[user]", (await _userRepository.GetById(userId)).Username);
        }

        private async Task<string> AddWorkName(string message, int activityId)
        {
            return message.Replace("[work]", (await _activitiesRepository.GetById(activityId)).Title);
        }
    }
}
