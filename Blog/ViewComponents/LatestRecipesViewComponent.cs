using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace TechBlog.ViewComponents
{
    [ViewComponent]
    public class LatestRecipesViewComponent : ViewComponent
    {
        private readonly IBlogPostGetterService _blogPostGetterService;

        public LatestRecipesViewComponent(IBlogPostGetterService blogPostGetterService)
        {
            _blogPostGetterService = blogPostGetterService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
           List<BlogPost> posts = await _blogPostGetterService.GetAllPosts();

            return View(posts);
        }
    }
}
