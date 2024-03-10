using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts;

namespace Services
{
    public class PageDeleter : IPageDeleterService
    {
        private readonly IPageRepository _pageRepository;
        private readonly ILogger<PageDeleter> _logger;

        public PageDeleter(IPageRepository pageRepository, ILogger<PageDeleter> logger)
        {
            _pageRepository = pageRepository;
            _logger = logger;
        }
        public async Task<bool> DeletePage(Guid? id)
        {
            if(id is null) throw new ArgumentNullException(nameof(id));

            return await _pageRepository.DeletePage(id);
        }
    }
}
