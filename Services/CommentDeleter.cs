using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts;

namespace Services
{
    public class CommentDeleter : ICommentDeleterService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger<CommentDeleter> _logger;

        public CommentDeleter(ICommentRepository commentRepository, ILogger<CommentDeleter> logger)
        {
            _commentRepository = commentRepository;
            _logger = logger;
        }

        public async Task<bool> DeleteComment(Guid id)
        {
            return await _commentRepository.DismissComment(id);
        }
    }
}
