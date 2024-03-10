using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Comment
    {
        [Key]
        public Guid CommentID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? CommentBody { get; set; }
        public DateTime CommentDate { get; set; } = DateTime.Now;
        public string? CommentStatus { get; set; } 

        //Navigation Property
        public Guid? PostID { get; set; }

        [ForeignKey("PostID")]
        public BlogPost? blogPost { get; set; }
    }
}
