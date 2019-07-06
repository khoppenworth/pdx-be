using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Account;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class MenusController: BaseController<Menu>
    {
        private readonly IMenuService _menuservice;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuService"></param>
        public MenusController(IMenuService menuService): base(menuService)
        {
            _menuservice = menuService;
        }

        /// <summary>
        /// Get all menus
        /// </summary>
        [HttpGet]
        public override async Task<IEnumerable<Menu>> GetAllAsync()
        {
            var entities = await _menuservice.GetAllAsync(true);
            return entities;
        }

        /// <summary>
        /// Get user menus
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ByUser/{userID}")]
        public async Task<IEnumerable<Menu>> GetMenusByUserAsync(int userID)
        {
            var result = await _menuservice.GetMenusByUserAsync(userID);
            return result;
        }

        /// <summary>
        /// Save Menu Roles
        /// </summary>
        /// <param name="rolePermission">The role</param>
        /// <returns>bool</returns>
        [HttpPost]
        [Route("MenuRoles")]
        public async Task<bool> SaveMenuRoles([FromBody]IList<MenuRole> menuRoles)
        {
            var result = await _menuservice.SaveMenuRolesAsync(menuRoles);
            return result;
        }
        
    }
}