using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Category
    {
        [Key]
        public Guid? CategoryID { get; set; }

        [Required(ErrorMessage = "Category name cannot be empty.")]
        [StringLength(30)]
        public string? CategoryName { get; set; }

        [Required(ErrorMessage = "Category slug cannot be empty.")]
        public string? CategorySlug { get; set; }

        [Required(ErrorMessage = "Category description cannot be empty.")]
        [StringLength(100)]
        public string? CategoryDescription { get; set;}
        public ICollection<BlogPost> Posts { get; set; } = new HashSet<BlogPost>();
    }
}
