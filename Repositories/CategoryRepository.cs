using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoryContracts;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _dBcontext;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(ApplicationDBContext dBContext, ILogger<CategoryRepository> logger)
        {
            _dBcontext = dBContext;
            _logger = logger;
        }
        public async Task<Category> AddCategory(Category category)
        {
            _dBcontext.Add(category);
            await _dBcontext.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _dBcontext.categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByID(Guid ID)
        {
            return await _dBcontext.categories.FindAsync(ID);
        }

        public async Task<int> PostsPerCategory(Guid categoryID)
        {
            return await _dBcontext.categories.CountAsync(c => c.CategoryID == categoryID);
        }
    }
}
