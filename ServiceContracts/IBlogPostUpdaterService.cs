using Entities;

namespace ServiceContracts
{
    public interface IBlogPostUpdaterService
    {
        public Task<BlogPost> UpdatePost(BlogPost? blogPost);
    }
}
