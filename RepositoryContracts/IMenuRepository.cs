using Entities;

namespace RepositoryContracts
{
    public interface IMenuRepository
    {
        public Task<Menu> AddMenu(Menu menu);
        public Task<bool> RemoveMenu(Guid id);
        public Task<Menu> GetMenu(Guid id);

    }
}
