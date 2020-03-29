using bakalaurinis.Infrastructure.Enums;

namespace bakalaurinis.Dtos.Invitation
{
    public class UpdateInvitationDto 
    {
        public int Id { get; set; }
        public int WorkId { get; set; }
        public InvitationStatusEnum InvitationStatus { get; set; }
    }
}
