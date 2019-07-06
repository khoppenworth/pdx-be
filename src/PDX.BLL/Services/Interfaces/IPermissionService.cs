using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.Domain.Account;

namespace PDX.BLL.Services.Interfaces
{
    public interface IPermissionService:IService<RolePermission>
    {
        Task<IEnumerable<Permission>> GetPermissionByRoleAsync(int roleID);
        Task<IEnumerable<Permission>> GetPermissionByUserAsync(int userID);
        Task<IEnumerable<Permission>> GetPermissionByRolesAsync(IEnumerable<int> roles);
    }
}