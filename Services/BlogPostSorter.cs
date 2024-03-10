using Entities;
using ServiceContracts;
using ServiceContracts.Enums;

namespace Services
{
    public class BlogPostSorter : IBlogPostSorterService
    {
        public List<BlogPost> GetSortedPosts(List<BlogPost> posts, string? sortBy, SortOrderOptions sortOrderOptions)
        {
            if (string.IsNullOrEmpty(sortBy)) return posts;

            List<BlogPost> sortedPosts = (sortBy, sortOrderOptions) switch
            {
                (nameof(BlogPost.Title), SortOrderOptions.Ascending) => posts.OrderBy(t => t.Title, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(BlogPost.Title), SortOrderOptions.Descending) => posts.OrderByDescending(t => t.Title, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(BlogPost.AuthorName), SortOrderOptions.Ascending) => posts.OrderBy(a => a.AuthorName, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(BlogPost.AuthorName), SortOrderOptions.Descending) => posts.OrderByDescending(a => a.AuthorName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(BlogPost.Date), SortOrderOptions.Ascending) => posts.OrderBy(d => d.Date).ToList(),
                (nameof(BlogPost.Date), SortOrderOptions.Descending) => posts.OrderByDescending(d => d.Date).ToList(),

                (nameof(BlogPost.PostViews), SortOrderOptions.Ascending) => posts.OrderBy(v => v.PostViews).ToList(),
                (nameof(BlogPost.PostViews), SortOrderOptions.Descending) => posts.OrderByDescending(v => v.PostViews).ToList(),

                _ => posts
            }; ;

            return sortedPosts;
        }
    }
}
