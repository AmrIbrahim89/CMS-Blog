using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoryContracts;

namespace Repositories
{
    public class PageRepository : IPageRepository
    {
        private readonly ILogger<PageRepository> _logger;
        private readonly ApplicationDBContext _dbContext;
        public PageRepository(ApplicationDBContext dBContext, ILogger<PageRepository> logger)
        {
            _dbContext = dBContext;
            _logger = logger;
        }
        public async Task<Page> AddPage(Page page)
        {
            _dbContext.Add(page);
            await _dbContext.SaveChangesAsync();
            return page;
        }

        public async Task<bool> DeletePage(Guid? ID)
        {
            _dbContext.RemoveRange(_dbContext.Pages.Where(p => p.PageID == ID));

            int rowsAffected = await _dbContext.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<List<Page>> GetAllPages()
        {
            return await _dbContext.Pages.ToListAsync();
        }

        public async Task<Page?> GetPageByID(Guid? ID)
        {
            return await _dbContext.Pages.FirstOrDefaultAsync(page => page.PageID == ID);
        }

        public async Task<Page> UpdatePage(Page page)
        {
            _dbContext.Update(page);
            await _dbContext.SaveChangesAsync();

            return page;
        }
    }
}
