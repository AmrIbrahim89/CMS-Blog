using Entities;
using Microsoft.AspNetCore.Mvc;

namespace TechBlog.ViewComponents
{
    [ViewComponent]
    public class LoadMoreCategoryPostsViewComponent : ViewComponent
    {
        private readonly ILogger<LoadMoreBlogPostsViewComponent> _logger;

        public LoadMoreCategoryPostsViewComponent(ILogger<LoadMoreBlogPostsViewComponent> logger)
        {
            _logger = logger;
        }

        public IViewComponentResult Invoke(List<BlogPost> filteredPosts)
        {

            //problem in this logic Skip(postsToSkip) when loading category posts, then editing or adding more categories.
            // removed int postsToSkip from here.
            var remainingPosts = filteredPosts.Take(6).ToList();

            return View(remainingPosts);
        }
    }
}
