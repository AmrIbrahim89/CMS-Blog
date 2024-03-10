using Entities;
using Entities.IdentityEntities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services
{
    public class BlogPostAdder : IBlogPostAdderService
    {
        private readonly IBlogPostsRepository _blogPostsRepository;
        private readonly ICategoryGetterService _categoryGetterService;
        public BlogPostAdder(IBlogPostsRepository blogPostsRepositoary, ICategoryGetterService categoryGetterService)
        {
            _blogPostsRepository = blogPostsRepositoary;
            _categoryGetterService = categoryGetterService;
        }

        public async Task<BlogPost> AddPost(BlogPostAddRequest? postAddRequest, ApplicationUser user)
        {
            if (postAddRequest is null)
            {
                throw new ArgumentNullException(nameof(postAddRequest));
            }

            ValidatorHelper.ModelValidation(postAddRequest);
            BlogPost post = postAddRequest.ConvertAddRequestToBlogPost();

            post.ImageData = await ImageFileToByteArray.Convert(postAddRequest.ImageFile!);
            post.PostID = Guid.NewGuid();

            List<Category> categories = await new MapCategoryToPost(_categoryGetterService).Confirm(postAddRequest);
            post.Categories = categories;

            post.Author = user;
            post.AuthorID = user?.Id;
            post.AuthorName = user?.FirstName + " " + user?.LastName;

            await _blogPostsRepository.AddPost(post);

            return post;
        }
    }
}
