using Entities;

namespace ServiceContracts.DTO
{
    public class CommentResponse
    {    
        public Guid CommentID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? CommentBody { get; set; }
        public DateTime CommentDate { get; set; } = DateTime.Now;
        public string? CommentStatus { get; set; }
        public Guid? PostID { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if(obj.GetType() != typeof(CommentResponse)) return false;

            CommentResponse comment = (CommentResponse)obj;

            return CommentID == comment.CommentID && Name == comment.Name && Email == comment.Email && 
                CommentBody == comment.CommentBody && CommentDate == comment.CommentDate && CommentStatus == comment.CommentStatus && 
                PostID == comment.PostID;               
        }

        public override int GetHashCode() => base.GetHashCode();
        
    }

    public static class CommentExtensions
    {
        public static CommentResponse ToCommentResponse(this Comment comment)
        {
            return new CommentResponse() { CommentID = comment.CommentID, Name = comment.Name, Email = comment.Email, 
            CommentBody = comment.CommentBody, CommentDate = comment.CommentDate, CommentStatus = comment.CommentStatus, 
            PostID = comment.PostID};
        }
    }
}
