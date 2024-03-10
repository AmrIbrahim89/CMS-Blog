using Entities;
using ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    public class CommentAddRequest
    {
        [Required(ErrorMessage = "Please enter your name.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter your email.")]
        [EmailAddress(ErrorMessage = "Please enter valid email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please add your comment.")]
        public string? CommentBody { get; set; }
        public CommentStatusOptions CommentStatus { get; set; } = CommentStatusOptions.Pending;
        public Guid? PostID { get; set; }

        public Comment ToComment()
        {
            return new Comment { Name = this.Name, Email = this.Email, CommentBody = this.CommentBody, CommentStatus = this.CommentStatus.ToString(), PostID = this.PostID };
        }
    }
}
