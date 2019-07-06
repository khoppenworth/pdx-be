using PDX.BLL.Services.Interfaces;
using PDX.DAL.Repositories;
using PDX.Domain.Account;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace PDX.BLL.Services
{
    public class MenuService : Service<Menu>, IMenuService
    {
        private readonly IUserService _userService;
        private readonly IService<MenuRole> _menuRoleService;
        public MenuService(IUnitOfWork unitOfWork, IUserService userService, IService<MenuRole> menuRoleService) : base(unitOfWork)
        {
            _userService = userService;
            _menuRoleService = menuRoleService;
        }
        /// <summary>
        /// Get users menu implementation
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Menu>> GetMenusByUserAsync(int userID)
        {
            var user = await _userService.GetAsync(userID);
            var roleIDs = user.Roles.Select(r => r.ID);
            var menus = await FindByAsync(x => x.MenuRoles.Any(y => y.IsActive && roleIDs.Contains(y.RoleID)));
            menus = menus.Distinct().OrderBy(m => m.Priority);
            return menus;
        }

        public async Task<bool> SaveMenuRolesAsync(IList<MenuRole> menuRoles)
        {
            var result = true;
            foreach (var mr in menuRoles)
            {
                var existingMenuRole = await _menuRoleService.GetAsync(mr.ID);
                result = result &&  (existingMenuRole != null ? await _menuRoleService.UpdateAsync(mr) : await _menuRoleService.CreateAsync(mr));
            }
            return result;
        }
    }
}