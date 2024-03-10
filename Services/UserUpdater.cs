using Entities;
using Entities.IdentityEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts;
using Services.Helpers;
using System.Security.Claims;

namespace Services
{
    public class UserUpdater : IUserUpdaterService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBlogPostsRepository _blogPostsRepository;
        private readonly ILogger<UserUpdater> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserUpdater(IUsersRepository usersRepository, UserManager<ApplicationUser> userManager, ILogger<UserUpdater> logger,
            IHttpContextAccessor httpContextAccessor, IBlogPostsRepository blogPostsRepository)
        {
            _usersRepository = usersRepository;
            _httpContextAccessor = httpContextAccessor;
            _blogPostsRepository = blogPostsRepository;
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<bool> UpdateUserDetails(ApplicationUser? user)
        {
            if (user == null) return false;

            ValidatorHelper.ModelValidation(user);

            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = await _userManager.FindByIdAsync(userId!);

            currentUser!.FirstName = user.FirstName;
            currentUser.LastName = user.LastName;
            currentUser.PhoneNumber = user.PhoneNumber;

            //List<BlogPost> posts =  await _blogPostsRepository.GetAllPosts();
            List<BlogPost?> currentUserPosts = await _blogPostsRepository.GetPostsByAuthorID(Guid.Parse(userId!));

            if (currentUserPosts is not null)
            {
                foreach (var post in currentUserPosts)
                {
                    post!.AuthorName = currentUser.FirstName + " " + currentUser.LastName;
                    await _blogPostsRepository.UpdatePost(post);
                }
            }

            return await _usersRepository.UpdateUser(currentUser);
        }
    }
}
