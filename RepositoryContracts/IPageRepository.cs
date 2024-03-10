using Entities;

namespace RepositoryContracts
{
    public interface IPageRepository
    {
        public Task<Page> AddPage(Page page);

        public Task<bool> DeletePage(Guid? pageID);

        public Task<List<Page>> GetAllPages();

        public Task<Page?> GetPageByID(Guid? pageID);

        public Task<Page> UpdatePage(Page page);
    }
}
