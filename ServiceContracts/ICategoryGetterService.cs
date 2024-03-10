using Entities;

namespace ServiceContracts
{
    public interface ICategoryGetterService
    {
        public Task<List<Category>> GetAllCategories();
        public Task<Category?> GetCategoryByID(Guid ID);
        public Task<int> NumberOfPostsPerCategory(Guid? categoryID);
    }
}
