using bakalaurinis.Infrastructure.Enums;

namespace bakalaurinis.Dtos.Invitation
{
    public class InvitationDto : NewInvitationDto
    {
        public int Id { get; set; }
        public InvitationStatusEnum InvitationStatus { get; set; }
    }
}
