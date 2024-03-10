using Entities;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts;

namespace Services
{
    public class CategoryGetter : ICategoryGetterService
    {
        private readonly ICategoryRepository _catrgoryRepository;
        private readonly ILogger<CategoryGetter> _logger;

        public CategoryGetter(ICategoryRepository catrgoryRepository, ILogger<CategoryGetter> logger)
        {
            _catrgoryRepository = catrgoryRepository;
            _logger = logger;
        }
        public async Task<List<Category>> GetAllCategories()
        {
            return await _catrgoryRepository.GetAllCategories();
        }

        public async Task<Category?> GetCategoryByID(Guid ID)
        {
            return await _catrgoryRepository.GetCategoryByID(ID);
        }

        public async Task<int> NumberOfPostsPerCategory(Guid? categoryID)
        {
            if(categoryID is null)
            {
                throw new ArgumentNullException(nameof(categoryID));
            }

            return await _catrgoryRepository.PostsPerCategory(categoryID.Value);
        }
    }
}
