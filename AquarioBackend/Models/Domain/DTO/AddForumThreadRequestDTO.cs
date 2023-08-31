namespace AquarioBackend.Models.Domain.DTO
{
    public class addForumThreadRequestDTO
    {

        public required string Title { get; set; }

        public required string Content { get; set; }

        public required string Tag { get; set; }  
    }
}
