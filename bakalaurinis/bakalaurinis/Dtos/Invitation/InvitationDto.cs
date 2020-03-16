using bakalaurinis.Infrastructure.Enums;

namespace bakalaurinis.Dtos.Invitation
{
    public class InvitationDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public InvitationStatusEnum InvitationStatus { get; set; }
    }
}
