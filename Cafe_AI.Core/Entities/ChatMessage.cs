namespace Cafe_AI.Core.Entities
{
    public class ChatMessage
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Content { get; set; } = "";
        //public string SenderId { get; set; } = string.Empty;
        public string SenderName { get; set; } = "";
        public string? ReceiverName { get; set; }
        public string Mode { get; set; } = "general";
        //public string ReceiverId { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
    }
}
