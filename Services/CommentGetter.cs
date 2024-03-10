using Entities;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CommentGetter : ICommentGetterService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger<CommentGetter> _logger;

        public CommentGetter(ICommentRepository commentRepository, ILogger<CommentGetter> logger)
        {
            _commentRepository = commentRepository;
            _logger = logger;
        }

        public async Task<List<CommentResponse>> GetComments()
        {
            var comments = await _commentRepository.GetComments();

            return comments.Select(c => c.ToCommentResponse()).ToList();
        }

        public async Task<List<CommentResponse>> GetApprovedComments()
        {
            var comments = await _commentRepository.GetApprovedComments();

            return comments.Select(c => c.ToCommentResponse()).ToList();
        }

        public async Task<List<CommentResponse>> GetPendingComments()
        {
            var comments = await _commentRepository.GetPendingComments();

            return comments.Select(c => c.ToCommentResponse()).ToList();
        }

        public async Task<Comment?> GetCommentByID(Guid? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            return await _commentRepository.GetCommentByID(id.Value);
        }
    }
}
