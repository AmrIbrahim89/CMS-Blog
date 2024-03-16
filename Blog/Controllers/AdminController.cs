using Entities;
using Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace TechBlog.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IBlogPostAdderService _blogPostAdderService;
        private readonly IBlogPostGetterService _blogPostGetterService;
        private readonly IBlogPostDeleterService _blogPostDeleterService;
        private readonly IBlogPostUpdaterService _blogPostUpdaterService;
        private readonly IBlogPostSorterService _blogPostSorterService;
        private readonly IMediaAdderService _mediaAdderService;
        private readonly IMediaGetterService _mediaGetterService;
        private readonly IMediaDeleterService _mediaDeleterService;
        private readonly ICategoryAdderService _categoryAdderService;
        private readonly ICategoryGetterService _categoryGetterService;
        private readonly IPageAdderService _pageAdderService;
        private readonly IPageGetterService _pageGetterService;
        private readonly IPageDeleterService _pageDeleterService;
        private readonly IUsersRepository _usersRepository;
        private readonly IUserUpdaterService _userUpdaterService;
        private readonly ICommentGetterService _commentGetterService;
        private readonly IMenuAdderService _menuAdderService;
        private readonly IMenuGetterService _menuGetterService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDBContext _dBContext;
        private const int pageSize = 4;

        public AdminController(ILogger<AdminController> logger, UserManager<ApplicationUser> userManager,
              IBlogPostAdderService blogPostAdder, IBlogPostGetterService blogPostGetter,
              IBlogPostDeleterService blogPostDeleterService, IBlogPostUpdaterService blogPostUpdaterService, 
              IBlogPostSorterService blogPostSorterService, IUsersRepository usersRepository, IMediaAdderService mediaAdderService, 
              IMediaGetterService mediaGetterService, IMediaDeleterService mediaDeleterService, ICategoryAdderService categoryAdderService, 
              ICategoryGetterService categoryGetterService, IPageAdderService pageAdderService, ApplicationDBContext dBContext, 
              IPageGetterService pageGetterService, IPageDeleterService pageDeleterService, IUserUpdaterService userUpdaterService, 
              ICommentGetterService commentGetterService, IMenuAdderService menuAdderService, IMenuGetterService menuGetterService)
        {
            _logger = logger;
            _userManager = userManager;
            _blogPostAdderService = blogPostAdder;
            _blogPostGetterService = blogPostGetter;
            _blogPostDeleterService = blogPostDeleterService;
            _blogPostUpdaterService = blogPostUpdaterService;
            _blogPostSorterService = blogPostSorterService;
            _pageAdderService = pageAdderService;
            _pageGetterService = pageGetterService;
            _pageDeleterService = pageDeleterService;
            _usersRepository = usersRepository;
            _userUpdaterService = userUpdaterService;
            _mediaAdderService = mediaAdderService;
            _mediaGetterService = mediaGetterService;
            _mediaDeleterService = mediaDeleterService;
            _categoryAdderService = categoryAdderService;
            _categoryGetterService = categoryGetterService;
            _commentGetterService = commentGetterService;
            _menuAdderService = menuAdderService;
            _menuGetterService = menuGetterService;
            _dBContext = dBContext;
        }

        [HttpGet]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            var recentComments = await _commentGetterService.GetComments();
            return View(recentComments);
        }

        #region BlogPosts
        [HttpGet]
        [Route("AllPosts")]
        public async Task<IActionResult> AllPosts(int pageNumber = 1, string sortBy = nameof(BlogPost.Title), SortOrderOptions sortOrder = SortOrderOptions.Ascending)
        {
            List<BlogPost> posts = await _blogPostGetterService.GetAllPosts();
            List<BlogPost> sortedPosts = _blogPostSorterService.GetSortedPosts(posts, sortBy, sortOrder);

            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.TotalPages = (int)Math.Ceiling((double)posts.Count / pageSize);

            List<BlogPost> paginatedSortedList = sortedPosts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return View(paginatedSortedList);
        }

        [HttpGet]
        [Route("FeaturedPosts")]
        public async Task<IActionResult> FeaturedPosts()
        {
            List<BlogPost> posts = await _blogPostGetterService.GetFeaturedPosts();
            return View(posts);
        }

        [HttpGet]
        [Route("AddPost")]
        public async Task<IActionResult> AddPost()
        {
            var categories = await _categoryGetterService.GetAllCategories();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        [Route("AddPost")]
        public async Task<IActionResult> AddPost(BlogPostAddRequest postAddRequest)
        {
            if(!ModelState.IsValid)
            {
                var categories = await _categoryGetterService.GetAllCategories();
                ViewBag.Categories = categories;

                ViewBag.Errors = ModelState.Values.SelectMany(err => err.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }

            //current logged-in user
            ApplicationUser? currentUser = await _userManager.GetUserAsync(User);

            //convert AddRequestDTO to BlogPost Entity
            BlogPost post = postAddRequest.ConvertAddRequestToBlogPost();

            if(currentUser is not null)
            {
                await _blogPostAdderService.AddPost(postAddRequest, currentUser);
            }
        
            return RedirectToAction(nameof(PostAdded), "Admin");
        }

        [Route("DeletePost/{postID}")]
        public async Task<IActionResult> DeletePost(Guid postID)
        {
            bool isDeleted = await _blogPostDeleterService.DeletePost(postID);

            if (!isDeleted)
            {
                return BadRequest("Post was not deleted, Try Again Later.");
            }

            return RedirectToAction(nameof(Index), "Admin");
        }

        [HttpGet]
        [Route("UpdatePost/{ID}")]
        public async Task<IActionResult> UpdatePost(Guid ID)
        {
            BlogPost? post = await _blogPostGetterService.GetPostByID(ID);

            if (post is null)
            {
                throw new ArgumentException("Post not found");
            }

            return View(post);
        }

        [HttpPost]
        [Route("UpdatePost/{ID}")]
        public async Task<IActionResult> UpdatePost(BlogPost post)
        {
            BlogPost? postFound = await _blogPostGetterService.GetPostByID(post.PostID);

            if (postFound is null)
            {
                return RedirectToAction(nameof(Index), "Admin");
            }

            await _blogPostUpdaterService.UpdatePost(post);
            return RedirectToAction(nameof(Index), "Admin");
        }

        [HttpGet]
        [Route("PostAdded")]
        public IActionResult PostAdded()
        {
            return View();
        }
        #endregion


        #region Pages
        [HttpGet]
        [Route("AddPage")]
        public IActionResult AddPage()
        {
            return View();
        }

        [HttpPost]
        [Route("AddPage")]
        public async Task<IActionResult> AddPage(Page page, IFormFile imageFile)
        {
            await _pageAdderService.AddPage(page, imageFile);           
            return RedirectToAction(nameof(Index), "Admin");
        }

        public async Task<IActionResult> DeletePage(Guid id)
        {
            await _pageDeleterService.DeletePage(id);
            return RedirectToAction(nameof(Index), "Admin");
        }

        [HttpGet]
        [Route("AllPages")]
        public async Task<IActionResult> AllPages()
        {
            List<Page> pages = await _pageGetterService.GetAllPages();
            ViewBag.Menues = await _menuGetterService.GetMenu(Guid.Parse("fe5b7770-8825-4a8e-5109-08dc40aae96e"));
            return View(pages);
        }
        #endregion


        #region Users
        [HttpGet]
        [Route("Users")]
        public async Task<IActionResult> Users()
        {
            List<ApplicationUser> users = await _usersRepository.GetAllUsers();
            return View(users);
        }
        #endregion


        #region Media
        [HttpGet]
        [Route("UploadMedia")]
        public IActionResult UploadMedia()
        {
            return View();
        }

        [HttpPost]
        [Route("UploadMedia")]
        public async Task<IActionResult> UploadMedia(IFormFile file)
        {
            await _mediaAdderService.UploadMediaFiles(file);
            return RedirectToAction(nameof(Index), "Admin");
        }

        [HttpGet]
        [Route("AllMedia")]
        public async Task<IActionResult> AllMedia()
        {
           List<Media> media = await _mediaGetterService.GetAllMedia();
            return View(media);
        }

        [Route("DeleteMedia/{ID}")]
        public async Task<IActionResult> DeleteMedia(Guid ID)
        {
            bool isDeleted = await _mediaDeleterService.DeleteMediaFile(ID);

            if (!isDeleted)
            {
                return BadRequest("Media File was not deleted, Try Again Later.");
            }

            return RedirectToAction(nameof(Index), "Admin");
        }
        #endregion


        #region Categories
        [HttpGet]
        [Route("AllCategories")]
        public async Task<IActionResult> AllCategories()
        {   
            List<Category> allCategories = await _categoryGetterService.GetAllCategories();
            return View(allCategories);
        }

        [HttpGet]
        [Route("AddCategory")]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [Route("AddCategory")]
        public async Task<IActionResult> AddCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(err => err.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }

            await _categoryAdderService.AddCategory(category);
            return RedirectToAction(nameof(Index), "Admin");
        }
        #endregion


        #region Profile
        [HttpGet]
        [Route("Profile")]
        public IActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        [Route("Profile")]
        public async Task<IActionResult> Profile(ApplicationUser user)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(err => err.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }

            ApplicationUser? currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null) 
            {
                throw new InvalidOperationException("User was not found.");
            }

            await _userUpdaterService.UpdateUserDetails(user);
            return View(currentUser);
        }
        #endregion


        #region Messages
        [HttpGet]
        [Route("Messages")]
        public IActionResult Messages()
        {
            return View();
        }
        #endregion


        #region Comments
        [HttpGet]
        [Route("Comments")]
        public async Task<IActionResult> Comments()
        {
            List<CommentResponse> comments = await _commentGetterService.GetPendingComments();
            return View(comments);
        }
        #endregion


        #region Menues

        [HttpGet]
        [Route("AddMenu")]
        public IActionResult AddMenu()
        {
            return View();
        }

        [HttpPost]
        [Route("AddMenu")]
        public async Task<IActionResult> AddMenu(Menu? menu)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Errors = 
                ModelState.Values.SelectMany(property => property.Errors).Select(err => err.ErrorMessage).ToList();

                return View();
            }

            await _menuAdderService.AddMenu(menu);
            return View(menu);
        }
        #endregion
    }
}
