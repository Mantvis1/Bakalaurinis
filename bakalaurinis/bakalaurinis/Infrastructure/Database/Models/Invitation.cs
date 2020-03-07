using System.ComponentModel.DataAnnotations.Schema;

namespace bakalaurinis.Infrastructure.Database.Models
{
    public class Invitation : BaseEntity
    {
        [ForeignKey("Sender"), Column(Order = 0)]
        public int? SenderId { get; set; }
        public User Sender { get; set; }
        [ForeignKey("Reciever"), Column(Order = 1)]
        public int? ReceiverId { get; set; }
        public User Reciever { get; set; }
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
