using Entities;
using Microsoft.AspNetCore.Http;

namespace ServiceContracts
{
    public interface IPageAdderService
    {
        public Task<Page> AddPage(Page? page, IFormFile img);
    }
}
