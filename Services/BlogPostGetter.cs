using Entities;
using RepositoryContracts;
using ServiceContracts;

namespace Services
{
    public class BlogPostGetter : IBlogPostGetterService
    {
        private readonly IBlogPostsRepository _blogPostsRespository;

        public BlogPostGetter(IBlogPostsRepository blogPostsRepositoary)
        {
            _blogPostsRespository = blogPostsRepositoary;
        }
        public async Task<List<BlogPost>> GetAllPosts()
        {
            return await _blogPostsRespository.GetAllPosts();
        }

        public async Task<List<BlogPost>> GetFeaturedPosts()
        {
            return await _blogPostsRespository.GetFeaturedPosts();
        }

        public async Task<BlogPost?> GetPostByID(Guid? id)
        {

            if (id is null) return null;

            BlogPost? post = await _blogPostsRespository.GetPostByID(id);

            if (post is null) return null;

            return post;
        }

        public async Task<List<BlogPost>> CategoryFilteredPosts(List<string>? filterBy)
        {

            if (filterBy is null || filterBy.Count == 0) return await _blogPostsRespository.GetAllPosts();

            List<BlogPost> posts = [];
            List<BlogPost> filteredPosts = [];

            foreach (var category in filterBy)
            {
                posts = await _blogPostsRespository.GetFilteredPostsByCategory(temp => temp.Categories.Any(c => c.CategoryName == category));                
                filteredPosts.AddRange(posts);
            }

            return filteredPosts;
        }

        public async Task<List<BlogPost>> FilteredPosts(string? searchString, string searchBy)
        {
            List<BlogPost> filteredPosts;

                filteredPosts = searchBy switch
                {
                    nameof(BlogPost.Title) => await _blogPostsRespository.GetFilteredPosts(p => p.Title!.Contains(searchString ?? default!)),

                    _ => await _blogPostsRespository.GetAllPosts()
                };
            
            return filteredPosts;
        }
    }
}
