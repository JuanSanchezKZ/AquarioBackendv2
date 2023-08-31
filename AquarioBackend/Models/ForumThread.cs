using System.ComponentModel.DataAnnotations;

namespace AquarioBackend.Models
{
    public class ForumThread
    {
        [Key]
        [Required]
        public int ThreadId { get; set; }

        public required string Title { get; set; }
 
        public required string Content { get; set; }

        public string Tag { get; set; }

        public DateTime TimeCreated { get; set; }

        public int ThreadLikes { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public ICollection<Reply> Reply { get; set; }




    }
}
