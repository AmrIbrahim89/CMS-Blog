using Entities;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts;
using Services.Helpers;

namespace Services
{
    public class CategoryAdder : ICategoryAdderService
    {
        private readonly ICategoryRepository _catrgoryRepository;
        private readonly ILogger<CategoryAdder> _logger;

        public CategoryAdder(ICategoryRepository catrgoryRepository, ILogger<CategoryAdder> logger)
        {
            _catrgoryRepository = catrgoryRepository;
            _logger = logger;
        }
        public async Task<Category> AddCategory(Category? category)
        {
            if(category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            ValidatorHelper.ModelValidation(category);
            return await _catrgoryRepository.AddCategory(category);
        }
    }
}
