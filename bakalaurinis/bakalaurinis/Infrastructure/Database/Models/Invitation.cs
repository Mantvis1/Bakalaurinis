using bakalaurinis.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace bakalaurinis.Infrastructure.Database.Models
{
    public class Invitation : BaseEntity
    {
        [Required]
        public int SenderId { get; set; } 
        public User Sender { get; set; }
        [Required]
        public int ReceiverId { get; set; }
        [Required]
        public int WorkId { get; set; }
        [Required]
        public InvitationStatusEnum InvitationStatus { get; set; }
    }
}
