using Entities;
using System;


namespace ServiceContracts
{
    public interface IMenuAdderService
    {
        public Task<Menu> AddMenu(Menu? menu);
    }
}
