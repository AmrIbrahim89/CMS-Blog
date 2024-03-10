using Entities;
using System.Linq.Expressions;

namespace RepositoryContracts
{
    public interface IBlogPostsRepository
    {
        public Task<BlogPost> AddPost(BlogPost post);

        public Task<bool> DeletePost(Guid ID);

        public Task<List<BlogPost>> GetAllPosts();

        public Task<List<BlogPost>> GetFeaturedPosts();

        public Task<BlogPost?> GetPostByID(Guid? postID);

        public Task<List<BlogPost>> GetPostsByAuthorID(Guid authorID);

        public Task<BlogPost> UpdatePost(BlogPost post);

        public Task<List<BlogPost>> GetFilteredPostsByCategory(Expression<Func<BlogPost, bool>> predicate);

        public Task<List<BlogPost>> GetFilteredPosts(Expression<Func<BlogPost, bool>> predicate);
    }
}
