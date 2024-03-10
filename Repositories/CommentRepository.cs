using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.Enums;

namespace Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly ILogger<CommentRepository> _logger;

        public CommentRepository(ApplicationDBContext dBContext, ILogger<CommentRepository> logger)
        {
            _dbContext = dBContext;
            _logger = logger;
        }
        public async Task<Comment> AddCommentToDatabase(Comment comment)
        {
            _dbContext.Add(comment);
            await _dbContext.SaveChangesAsync();

            return comment;
        }


        public async Task<List<Comment>> GetComments()
        {
            return await _dbContext.Comments.ToListAsync();
        }

        public async Task<List<Comment>> GetApprovedComments()
        {
            return await _dbContext.Comments.Where(c => c.CommentStatus == CommentStatusOptions.Approved.ToString()).ToListAsync();
        }

        public async Task<List<Comment>> GetPendingComments()
        {
            return await _dbContext.Comments.Where(c => c.CommentStatus == CommentStatusOptions.Pending.ToString()).ToListAsync();
        }

        public async Task<Comment?> GetCommentByID(Guid id)
        {
            return await _dbContext.Comments.FirstOrDefaultAsync(c => c.CommentID == id);
        }

        public async Task<bool> DismissComment(Guid id)
        {
            _dbContext.Comments.RemoveRange(_dbContext.Comments.Where(c => c.CommentID == id));

            int rowsEffected = await _dbContext.SaveChangesAsync();
            return rowsEffected > 0;
        }
    }
}
