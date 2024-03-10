using Entities;

namespace RepositoryContracts
{
    public interface IMenuGetterService
    {
        public Task<Menu?> GetMenu(Guid? id);
    }
}
