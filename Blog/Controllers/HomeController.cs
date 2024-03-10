using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.Enums;

namespace TechBlog.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostGetterService _blogPostGetterService;
        private readonly IPageGetterService _pageGetterService;
        private readonly ICategoryGetterService _categoryGetterService;
        private readonly IBlogPostSorterService _blogPostSorterService;
        private readonly ISubscriberAdderService _subscriberAdderService;
        private readonly ApplicationDBContext _dbContext;
        private const int pageSize = 6;

        public HomeController(ILogger<HomeController> logger, IBlogPostGetterService blogPost, ApplicationDBContext dbContext,
            ICategoryGetterService categoryGetterService, IBlogPostSorterService blogPostSorterService, 
            IPageGetterService pageGetterService, ISubscriberAdderService subscriberAdderService)
        {
            _logger = logger;
            _blogPostGetterService = blogPost;
            _dbContext = dbContext;
            _categoryGetterService = categoryGetterService;
            _blogPostSorterService = blogPostSorterService;
            _pageGetterService = pageGetterService;
            _subscriberAdderService = subscriberAdderService;
        }

        [Route("/")]
        [Route("Index")]
        public async Task<IActionResult> Index(string sortBy = nameof(BlogPost.PostViews), SortOrderOptions sortOrderOptions = SortOrderOptions.Descending)
        {
            List<BlogPost> posts = await _blogPostGetterService.GetAllPosts();
            ViewBag.MostViewedPosts = _blogPostSorterService.GetSortedPosts(posts, sortBy, sortOrderOptions).Take(4);
            ViewBag.Categories = await _categoryGetterService.GetAllCategories();

            if (posts is null) return NotFound();

            return View(posts);
        }

        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        [HttpGet]
        [Route("post/{id}")]
        public async Task<IActionResult> Post(Guid id)
        {

            BlogPost? post = await _blogPostGetterService.GetPostByID(id);

            if (post is null) return NotFound("Post Not Found! Try Again Later..");

            ViewBag.FeaturedPosts = _blogPostGetterService.GetFeaturedPosts().Result.Where(p => p.PostID != id).Take(2);
            ViewBag.Categories = await _categoryGetterService.GetAllCategories();

            post.PostViews++;
            await _dbContext.SaveChangesAsync();

            return View(post);
        }

        #region Pages
        [HttpGet]
        [Route("page/{id}")]
        public async Task<IActionResult> Page(Guid id)
        {
            Page? page = await _pageGetterService.GetPageByID(id);

            //Later return 404 page
            if (page is null) return NotFound("Post Not Found! Try Again Later..");

            return View(page);
        }

        [HttpGet]
        [Route("About")]
        public IActionResult About()
        {
            //Page? page = await _pageGetterService.GetPageByID(id);

            ////Later return 404 page
            //if (page is null) return NotFound("Post Not Found! Try Again Later..");

            return View();
        }

        #endregion

        [HttpGet]
        [Route("AllRecipes")]
        public async Task<IActionResult> AllRecipes(List<string> filterBy)
        {
            var categories = await _categoryGetterService.GetAllCategories();
            ViewBag.Categories = categories;

            var filteredPosts = await _blogPostGetterService.CategoryFilteredPosts(filterBy);
            return View(filteredPosts);
        }

        //AllRecipes API
        [HttpGet("/AllRecipes")]
        public async Task<IActionResult> FilteredRecipes(string? categories)
        {
            List<Category> Categories = await _categoryGetterService.GetAllCategories();
            ViewBag.Categories = Categories;

            var selectedCategories = categories?.Split(',').ToList();
            var filteredPosts = await _blogPostGetterService.CategoryFilteredPosts(selectedCategories!);

            return ViewComponent("LoadMoreCategoryPosts", filteredPosts);
        }

        //Load More API
        [HttpGet("/Home/LoadMorePosts")]
        public IActionResult LoadMoreRecipes(int postsToSkip, string? filterBy)
        {
            return ViewComponent("LoadMoreBlogPosts", new { postsToSkip, filterBy });
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search(string? searchString, int pageNumber = 1)
        {
            var categories = await _categoryGetterService.GetAllCategories();
            ViewBag.Categories = categories;
            ViewBag.SearchString = searchString;

            List<BlogPost> posts = await _blogPostGetterService.FilteredPosts(searchString ?? "", nameof(BlogPost.Title));
            ViewBag.TotalPages = (int)Math.Ceiling((double)posts.Count / pageSize);

            List<BlogPost> paginatedList = posts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return View(paginatedList);
        }

        [HttpPost]
        [Route("Subscribe")]
        public async Task<IActionResult> Subscribe(Subscriber subscriber)
        {
            
            if(!ModelState.IsValid)
            {
                ViewBag.SubscriptionError = ModelState.Values.SelectMany(v => v.Errors).Select(err => err.ErrorMessage).ToList();
                return RedirectToAction(nameof(Index), "Home");
            }

            await _subscriberAdderService.AddSubscriber(subscriber);

            return RedirectToAction(nameof(Index), "Home");
        }

    }
}
