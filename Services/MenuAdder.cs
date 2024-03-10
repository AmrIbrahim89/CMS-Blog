using Entities;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts;
using Services.Helpers;
using System;

namespace Services
{
    public class MenuAdder : IMenuAdderService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly ILogger<MenuAdder> _logger;
        public MenuAdder(IMenuRepository repository, ILogger<MenuAdder> logger)
        {
            _menuRepository = repository;
            _logger = logger;
        }
        public async Task<Menu> AddMenu(Menu? menu)
        {
            if(menu is null)
            {
                throw new ArgumentNullException(nameof(menu));
            }

            ValidatorHelper.ModelValidation(menu);

            await _menuRepository.AddMenu(menu);
            return menu;
        }
    }
}
