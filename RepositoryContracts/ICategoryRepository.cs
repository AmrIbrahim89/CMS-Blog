using Entities;

namespace RepositoryContracts
{
    public interface ICategoryRepository
    {
        public Task<Category> AddCategory(Category category);
        public Task<List<Category>> GetAllCategories();
        public Task<Category?> GetCategoryByID(Guid ID);
        public Task<int> PostsPerCategory(Guid categoryID);
    }
}
