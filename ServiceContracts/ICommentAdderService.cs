using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface ICommentAdderService
    {
        public Task<CommentResponse> AddComment(CommentAddRequest? commentAddRequest);
    }
}
