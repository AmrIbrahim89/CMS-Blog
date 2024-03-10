using RepositoryContracts;
using ServiceContracts;

namespace Services
{
    public class BlogPostDeleter : IBlogPostDeleterService
    {
        private readonly IBlogPostsRepository _blogPostsRepositoary;

        public BlogPostDeleter(IBlogPostsRepository blogPostsRepositoary)
        {
            _blogPostsRepositoary = blogPostsRepositoary;
        }

        public async Task<bool> DeletePost(Guid id)
        {           
           return await _blogPostsRepositoary.DeletePost(id);
        }
    }
}
