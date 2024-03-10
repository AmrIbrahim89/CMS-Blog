using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace TechBlog.ViewComponents
{
    [ViewComponent]
    public class LoadMoreBlogPostsViewComponent : ViewComponent
    {
        private readonly ILogger<LoadMoreBlogPostsViewComponent> _logger;
        private readonly IBlogPostGetterService _blogPostGetterService;
        private static int postsCount = 0;

        public LoadMoreBlogPostsViewComponent(IBlogPostGetterService blogPostGetterService, ILogger<LoadMoreBlogPostsViewComponent> logger)
        {
            _logger = logger;
            _blogPostGetterService = blogPostGetterService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int postsToSkip, string? filterBy)
        {
            var selectedCategories = filterBy?.Split(',').ToList();

            List<BlogPost> filteredPosts = await _blogPostGetterService.CategoryFilteredPosts(selectedCategories!);
            var remainingPosts = filteredPosts.Skip(postsToSkip).Take(3).ToList();

            return View(remainingPosts);
        }
    }
}
