using Entities;
using Entities.IdentityEntities;
using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface IBlogPostAdderService
    {
        public Task<BlogPost> AddPost(BlogPostAddRequest? post, ApplicationUser user);
    }
}
