using Entities;

namespace ServiceContracts
{
    public interface IPageGetterService
    {
        public Task<List<Page>> GetAllPages();

        public Task<Page?> GetPageByID(Guid? pageID);
    }
}
