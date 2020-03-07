using System;

namespace bakalaurinis.Dtos.Message
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}