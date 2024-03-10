using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoryContracts;

namespace Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private ILogger<MenuRepository> _logger;

        public MenuRepository(ApplicationDBContext context, ILogger<MenuRepository> logger)
        {
            _dbContext = context;
            _logger = logger;
        }
        public async Task<Menu> AddMenu(Menu menu)
        {
            _dbContext.Add(menu);
            await _dbContext.SaveChangesAsync();
            return menu;
        }

        public async Task<Menu> GetMenu(Guid id)
        {
            return await _dbContext.Menues.Where(m => m.MenuID == id).FirstAsync();
        }

        public async Task<bool> RemoveMenu(Guid id)
        {
            _dbContext.RemoveRange(_dbContext.Menues.Where(m => m.MenuID == id));
            int rowsAffected = await _dbContext.SaveChangesAsync();

            return rowsAffected > 0;
        }
    }
}
