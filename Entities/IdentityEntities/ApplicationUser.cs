using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Entities.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        /*
          Claims-based identity is a way to keep information about a user. A claim is a key-value pair that is assigned to a user
          by a trusted source. A claim defines what the user is, in contrast to what the user can do. 
          An identity can contain multiple claims, examples are the user’s id, name, and email or custom one's like
          FirstName, LastName etc..
         */
        [Required(ErrorMessage = "FirstName is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string? LastName { get; set; }
        public List<BlogPost>? BlogPosts { get; set; } 
    }
}
