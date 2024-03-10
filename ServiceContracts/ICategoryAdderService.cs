using Entities;

namespace ServiceContracts
{
    public interface ICategoryAdderService
    {
        public Task<Category> AddCategory(Category? category);
    }
}
