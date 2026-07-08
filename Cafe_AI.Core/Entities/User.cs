namespace Cafe_AI.Core.Entities
{
    public class User
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsOnline { get; set; }
        public int UnreadCount { get; set; }
    }
}
