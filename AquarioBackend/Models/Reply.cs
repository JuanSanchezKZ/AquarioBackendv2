using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AquarioBackend.Models
{
    public class Reply
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime TimeCreated { get; set; }
        public int ForumThreadId { get; set; }
        public int UserId { get; set; }
        public virtual ForumThread ForumThread { get; set; }


}
}
