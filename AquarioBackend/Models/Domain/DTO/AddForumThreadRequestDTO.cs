namespace AquarioBackend.Models.Domain.DTO
{
    public class addForumThreadRequestDTO
    {

        public required string Title { get; set; }

        public required string Content { get; set; }


        public int UserId { get; set; }
    }
}
