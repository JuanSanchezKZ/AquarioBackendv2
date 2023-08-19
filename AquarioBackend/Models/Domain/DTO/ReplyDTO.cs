using System.ComponentModel.DataAnnotations;

namespace AquarioBackend.Models.Domain.DTO
{
    public class ReplyDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int ForumThreadId { get; set; }

        public int UserId { get; set; }
    }
}
