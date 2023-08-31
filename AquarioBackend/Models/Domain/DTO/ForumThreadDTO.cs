namespace AquarioBackend.Models.Domain.DTO
{
    public class ForumThreadDTO
    {
        public int ThreadId { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Tag { get; set; }
        
        public ICollection<ReplyDTO>? Replies { get; set; }  
    }
}
