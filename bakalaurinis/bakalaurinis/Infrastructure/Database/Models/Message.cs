namespace bakalaurinis.Infrastructure.Database.Models
{
    public class Message : BaseEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int ReceiverId { get; set; }
        public User User { get; set; }
    }
}
