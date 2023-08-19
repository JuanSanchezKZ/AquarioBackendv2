using System.Text.Json.Serialization;

namespace AquarioBackend.Models.Domain.DTO
{
    
    public class AddReplyRequestDTO
    {
        public string Content { get; set; }

        public int ForumThreadId { get; set; }

        public int UserId { get; set; }
    }
}
