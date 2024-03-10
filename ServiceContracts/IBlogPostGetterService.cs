using Entities;

namespace ServiceContracts
{
    public interface IBlogPostGetterService
    {
        public Task<List<BlogPost>> GetAllPosts();
        public Task<List<BlogPost>> GetFeaturedPosts();
        public Task<BlogPost?> GetPostByID(Guid? id);
        public Task<List<BlogPost>> CategoryFilteredPosts(List<string> filterBy);
        public Task<List<BlogPost>> FilteredPosts(string? searchString, string searchBy);
    }
}
