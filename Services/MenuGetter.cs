using Entities;
using Microsoft.Extensions.Logging;
using RepositoryContracts;

namespace Services
{
    public class MenuGetter : IMenuGetterService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly ILogger<MenuGetter> _logger;

        public MenuGetter(IMenuRepository repository, ILogger<MenuGetter> logger)
        {
            _menuRepository = repository;
            _logger = logger;
        }
        public async Task<Menu?> GetMenu(Guid? id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));

            return await _menuRepository.GetMenu(id.Value);
        }
    }
}
