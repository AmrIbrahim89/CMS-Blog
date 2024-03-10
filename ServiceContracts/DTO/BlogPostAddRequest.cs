using Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    public class BlogPostAddRequest
    {
        [Required(ErrorMessage = "Title Cannot Be Empty!")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Description is Required!")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Content Cannot Be Empty!")]
        public string? Content { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string? ImageTitle { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public IFormFile? ImageFile { get; set; }

        [Required(ErrorMessage = "Recipe Must Have Ingredients!")]
        public string? Ingredients { get; set; }

        [Required(ErrorMessage = "Please Select A Category.")]
        public List<Guid>? CategoriesID { get; set; }

        public BlogPost ConvertAddRequestToBlogPost()
        {
            return new BlogPost { Title = this.Title, Description = this.Description, Content = this.Content, Date = this.Date, ImageTitle = this.ImageTitle, 
                                  Ingredients = Ingredients?.Split("-").ToList() };
        }
    }
}
