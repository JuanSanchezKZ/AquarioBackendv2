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

        public int RepliesLikes { get; set; }
        public int ForumThreadId { get; set; }
        public string UserId { get; set; }

        public string UserName { get; set; }
        public virtual ForumThread ForumThread { get; set; }


}
}
