using Entities;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services
{
    public class CommentAdder : ICommentAdderService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger<CommentAdder> _logger;

        public CommentAdder(ICommentRepository commentRepository, ILogger<CommentAdder> logger)
        {
            _commentRepository = commentRepository;
            _logger = logger;
        }
        public async Task<CommentResponse> AddComment(CommentAddRequest? commentAddRequest)
        {
            if(commentAddRequest is null) throw new ArgumentNullException(nameof(commentAddRequest));

            ValidatorHelper.ModelValidation(commentAddRequest);

            Comment comment = commentAddRequest.ToComment();

            await _commentRepository.AddCommentToDatabase(comment);

            return comment.ToCommentResponse();
        }
    }
}
