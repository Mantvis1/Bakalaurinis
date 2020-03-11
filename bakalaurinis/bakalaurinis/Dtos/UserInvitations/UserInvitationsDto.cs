using bakalaurinis.Infrastructure.Enums;

namespace bakalaurinis.Dtos.UserActivities
{
    public class UserInvitationsDto
    {
        public int ReceiverId { get;set; }
        public string Username { get; set; }
        public InvitationStatusEnum InvitationStatus { get; set; }
    }
}
