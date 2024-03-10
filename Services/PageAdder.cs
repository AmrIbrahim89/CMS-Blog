using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts;
using Services.Helpers;

namespace Services
{
    public class PageAdder : IPageAdderService
    {
        private readonly ILogger<PageAdder> _logger;
        private readonly IPageRepository _pageRepository;
        public PageAdder(IPageRepository pageRepository, ILogger<PageAdder> logger)
        {
            _logger = logger;
            _pageRepository = pageRepository;
        }
        public async Task<Page> AddPage(Page? page, IFormFile image)
        {
            if (page is null)
            {
                throw new ArgumentNullException(nameof(Page));
            }

            ValidatorHelper.ModelValidation(page);
            page.ImageData = await ImageFileToByteArray.Convert(image);

            await _pageRepository.AddPage(page);
            return page;
        }
    }
}
