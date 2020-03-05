using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bakalaurinis.Infrastructure.Database.Models
{
    public class Invitations : BaseEntity
    {
        [Required]
        [ForeignKey("InvitedId")]
        public int InviterId { get; set; }
        public User Inviter { get; set; }
        [Required]
        [ForeignKey("RecieverId")]
        public int RecieverId { get; set; }
        public User Reciever { get; set; }
        [Required]
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
