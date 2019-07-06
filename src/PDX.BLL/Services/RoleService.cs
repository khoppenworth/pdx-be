using PDX.BLL.Services.Interfaces;
using PDX.DAL.Repositories;
using PDX.Domain.Account;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace PDX.BLL.Services
{
    public class RoleService : Service<Role>, IRoleService
    {
        private readonly IService<UserRole> _userRoleService;
        public RoleService(IUnitOfWork unitOfWork, IService<UserRole> userRoleService) : base(unitOfWork)
        {
            _userRoleService = userRoleService;
        }
        /// <summary>
        /// Get users Role implementation
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Role role)
        {
            var r = new Role(){
                ID = role.ID,
                MenuRoles = role.MenuRoles,
                Permissions = role.Permissions,
                ReportRoles = role.ReportRoles
            };
            var result = await base.UpdateAsync(r);
            return result;
        }

        public async Task<IEnumerable<Role>> GetRolesByUserAsync(int userID){
            var userRoles = await _userRoleService.FindByAsync(ua => ua.UserID == userID);
            var roles = userRoles.Select(ua => ua.Role);
            return roles;
        }
    }
}