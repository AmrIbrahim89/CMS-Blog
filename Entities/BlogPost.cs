using Entities.IdentityEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class BlogPost
    {
        [Key]
        public Guid? PostID { get; set; }

        [StringLength(200)]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public DateTime Date { get; set; } 
        public string? ImageTitle { get; set; }
        public byte[]? ImageData { get; set; }
        public List<string>? Ingredients { get; set; }
        public bool IsFeatured { get; set; } = false;
        public int PostViews { get; set; } = 0;

        //Post Comments
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        //Category Navigation
        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();

        //Navigation Properties
        public Guid? AuthorID { get; set; }
        public string? AuthorName { get; set; }

        [ForeignKey("AuthorID")]
        public ApplicationUser? Author { get; set; }

    }
}
