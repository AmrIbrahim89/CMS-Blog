using Entities;
using RepositoryContracts;
using ServiceContracts;
using Services.Helpers;

namespace Services
{
    public class BlogPostUpdater : IBlogPostUpdaterService
    {
        private readonly IBlogPostsRepository _blogPostsRepositoary;

        public BlogPostUpdater(IBlogPostsRepository blogPostsRepositoary) 
        {
            _blogPostsRepositoary = blogPostsRepositoary;
        }
        public async Task<BlogPost> UpdatePost(BlogPost? blogPost)
        {
            if (blogPost is null)
            {
                throw new ArgumentNullException(nameof(blogPost));
            }

            ValidatorHelper.ModelValidation(blogPost);

            BlogPost? postFound = await _blogPostsRepositoary.GetPostByID(blogPost.PostID);

            if(postFound is null) throw new ArgumentNullException(nameof(postFound));

            if (blogPost.ImageData is not null)
            {
                postFound.ImageData = blogPost.ImageData;
            }

            postFound.Author = blogPost.Author;
            postFound.AuthorID = blogPost.AuthorID;
            postFound.Date = DateTime.Now;
            postFound.Title = blogPost.Title;
            postFound.Content = blogPost.Content;
            postFound.ImageTitle = blogPost.ImageTitle;

            await _blogPostsRepositoary.UpdatePost(postFound);

            return postFound;
        }
    }
}
