using Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services.Helpers
{
    public class MapCategoryToPost
    {
        private readonly ICategoryGetterService _categoryGetterService;

        public MapCategoryToPost(ICategoryGetterService categoryGetterService)
        {
            _categoryGetterService = categoryGetterService;
        }
        public async Task<List<Category>> Confirm(BlogPostAddRequest postAddRequest)
        {
            List<Category> categories = new();

            foreach (var id in postAddRequest.CategoriesID!)
            {
                Category? category = await _categoryGetterService.GetCategoryByID(id);

                if (category is not null)
                {
                    categories.Add(category);
                }
            }

            return categories;
        }
    }
}
