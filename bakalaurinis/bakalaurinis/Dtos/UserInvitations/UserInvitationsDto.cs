using bakalaurinis.Infrastructure.Enums;

namespace bakalaurinis.Dtos.UserInvitations
{
    public class UserInvitationsDto
    {
        public int Id { get; set; }
        public int ReceiverId { get; set; }
        public string Username { get; set; }
        public InvitationStatusEnum InvitationStatus { get; set; }
    }
}
