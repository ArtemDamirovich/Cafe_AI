namespace Cafe_AI.Core.Entities
{
    public class ChatMessage
    {
        public string Id { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string SenderId { get; set; } = string.Empty;
        public string? SenderName { get; set; }
        public string ReceiverId { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
    }
}
