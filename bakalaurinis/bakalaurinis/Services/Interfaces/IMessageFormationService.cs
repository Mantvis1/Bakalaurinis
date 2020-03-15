using System.Threading.Tasks;

namespace bakalaurinis.Services.Interfaces
{
    public interface IMessageFormationService
    {
        Task<string> GetFormattedText(string message, int userId, int activityId);
    }
}
