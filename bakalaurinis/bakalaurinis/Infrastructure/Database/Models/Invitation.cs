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
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
