using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace TechBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelperController : Controller
    {
        private readonly IBlogPostGetterService _blogPostGetterService;
        private readonly ICommentAdderService _commentAdderService;
        private readonly ICommentGetterService _commentGetterService;
        private readonly ICommentDeleterService _commentDeleterService;
        private readonly IMenuAdderService _menuAdderService;
        private readonly IMenuGetterService _menuGetterService;
        private readonly IPageGetterService _pageGetterService;

        // we should move this to repository, don't update db here.
        private readonly ApplicationDBContext _dbContext;

        public HelperController(IBlogPostGetterService blogPostGetterService, ApplicationDBContext dBContext, 
            IPageGetterService pageGetterService, ICommentAdderService commentAdderService, ICommentGetterService commentGetter, 
            ICommentDeleterService commentDeleterService, IMenuGetterService menuGetterService, IMenuAdderService menuAdderService)
        {
            _blogPostGetterService = blogPostGetterService;
            _commentAdderService = commentAdderService;
            _commentGetterService = commentGetter;
            _commentDeleterService = commentDeleterService;
            _menuGetterService = menuGetterService;
            _menuAdderService = menuAdderService;
            _pageGetterService = pageGetterService;
            _dbContext = dBContext;
        }

        [HttpPatch("/api/blogposts/{postId}/feature")]
        //[Route("/api/blogposts/{postId}/feature")]
        public async Task<IActionResult> PostFeatured(Guid? postId)
        {
            BlogPost? post = await _blogPostGetterService.GetPostByID(postId);

            if(post is null)
            {
                return BadRequest("Error Updating Post");
            }

            if(!post.IsFeatured)
            {
                post.IsFeatured = true;
            }
            else
            {
                post.IsFeatured = false;
            }
            
            await _dbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("/api/AddPageToMenu")]
        public async Task<IActionResult> AddPageToMenu(Guid? pageID)
        {
            Page? page = await _pageGetterService.GetPageByID(pageID);

            if (page is null)
            {
                return BadRequest("Error adding page to main menu.");
            }

            string htmlMenuItem = $"<a href={page.Title.Substring(0, 5)}>{page.Title}</a>";
            return Content(htmlMenuItem, "text/html");

            //return Ok(page);
        }


        [HttpPost("/api/AddComment")]
        public async Task<IActionResult> AddComment([FromQuery] CommentAddRequest commentAddRequest)
        {
            await _commentAdderService.AddComment(commentAddRequest);
            return Ok();
        }

        
        [HttpPatch("/api/CommentApproval")]
        public async Task<IActionResult> CommentApproval([FromQuery] Guid commentID)
        {
            Comment? comment = await _commentGetterService.GetCommentByID(commentID);

            if(comment is null)
            {
                return NotFound();
            }

            comment.CommentStatus = CommentStatusOptions.Approved.ToString();
            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        [HttpDelete("/api/DismissComment")]
        public async Task<IActionResult> DismissComment([FromQuery] Guid commentID)
        {
            bool isDeleted = await _commentDeleterService.DeleteComment(commentID);

            if (!isDeleted) 
            {
                return BadRequest("ops..Something went wrong and the comment was not deleted.");
            }

            return Ok();
        }

        
        //[HttpPatch("/api/AddMenuItem")]
        //public async Task<IActionResult> AddMenuItem(Guid pageID)
        //{
        //    Menu? menu = await _menuGetterService.GetMenu(Guid.Parse("fe5b7770-8825-4a8e-5109-08dc40aae96e"));
        //    menu?.MenuElements?.Add(pageID);
        //    await _dbContext.SaveChangesAsync();

        //    return Ok();
        //}

        [HttpGet]
        [Route("Uploads")]
        public IActionResult Uploads()
        {
            return View();
        }
    }
}
