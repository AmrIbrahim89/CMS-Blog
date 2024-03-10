using Entities;
using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface ICommentGetterService
    {
        public Task<List<CommentResponse>> GetComments();
        public Task<List<CommentResponse>> GetApprovedComments();
        public Task<List<CommentResponse>> GetPendingComments();
        public Task <Comment?> GetCommentByID(Guid? id);
    }
}
