using Entities;

namespace RepositoryContracts
{
    public interface ICommentRepository
    {
        public Task<Comment> AddCommentToDatabase(Comment comment);
        public Task<List<Comment>> GetComments();
        public Task<List<Comment>> GetApprovedComments();
        public Task<List<Comment>> GetPendingComments();
        public Task<Comment?> GetCommentByID(Guid id);
        public Task<bool> DismissComment(Guid id);
    }
}
