using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts
{
    public interface IBlogPostSorterService
    {
        public List<BlogPost> GetSortedPosts(List<BlogPost> posts, string? sortBy, SortOrderOptions sortOrder);
    }
}
