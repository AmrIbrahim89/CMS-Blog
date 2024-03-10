using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using System.Linq.Expressions;

namespace Repositories
{
    public class BlogPostsRepository : IBlogPostsRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<BlogPostsRepository> _logger;

        public BlogPostsRepository(ApplicationDBContext context, ILogger<BlogPostsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BlogPost> AddPost(BlogPost post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<bool> DeletePost(Guid Id)
        {
            _context.Posts.RemoveRange(_context.Posts.Where(p => p.PostID == Id));
            int rowsEffected =  await _context.SaveChangesAsync();

            return rowsEffected > 0;
        }

        public async Task<List<BlogPost>> GetAllPosts()
        {
            List<BlogPost> posts = await _context.Posts.Include("Author").Include("Categories").ToListAsync();
            return posts;
        }

        public async Task<List<BlogPost>> GetFeaturedPosts()
        {
            List<BlogPost> featuredPosts = await _context.Posts.Where(p => p.IsFeatured == true).Include("Author").Include("Categories").ToListAsync();

            return featuredPosts;
        }

        public async Task<BlogPost?> GetPostByID(Guid? postID)
        {           
           return await _context.Posts.Include(a => a.Author).Include(c => c.Categories).Include(c => c.Comments).FirstOrDefaultAsync(p => p.PostID == postID);
        }

        public async Task<BlogPost> UpdatePost(BlogPost post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<List<BlogPost>> GetFilteredPostsByCategory(Expression<Func<BlogPost, bool>> predicate)
        {
            return await _context.Posts.Include(a => a.Author)
                .Include(c => c.Categories)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<List<BlogPost>> GetFilteredPosts(Expression<Func<BlogPost, bool>> predicate)
        {
            return await _context.Posts.Include(a => a.Author)
                .Include(c => c.Categories) 
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<List<BlogPost>> GetPostsByAuthorID(Guid authorID)
        {
             return await _context.Posts.Include("Author").Include("Categories").Where(p => p.AuthorID == authorID).ToListAsync();
        }
    }
}
