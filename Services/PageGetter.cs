using Entities;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts;

namespace Services
{
    public class PageGetter : IPageGetterService
    {
        private readonly ILogger<PageGetter> _logger;
        private readonly IPageRepository _pageRepository;
        public PageGetter(ILogger<PageGetter> logger, IPageRepository pageRepository)
        {
            _logger = logger;
            _pageRepository = pageRepository;
        }
        public async Task<List<Page>> GetAllPages()
        {
            return await _pageRepository.GetAllPages();
        }

        public async Task<Page?> GetPageByID(Guid? ID)
        {
            if(ID == null) return null;

           Page? page = await _pageRepository.GetPageByID(ID);

            if(page is null) return null;

            return page;

        }
    }
}
