using Entities;

namespace TechBlog.Models
{
    public class BlogPostViewModel
    {
        public BlogPost? BlogPost { get; set; }
        public Comment? CommentInput { get; set; }
    }
}
