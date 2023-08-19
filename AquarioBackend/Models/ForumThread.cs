using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AquarioBackend.Models
{
    public class ForumThread
    {
        [Key]
        [Required]
        public int ThreadId { get; set; }

        public required string Title { get; set; }
 
        public required string Content { get; set; }

        public DateTime TimeCreated { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
        public ICollection<Reply> Reply { get; set; }




    }
}
