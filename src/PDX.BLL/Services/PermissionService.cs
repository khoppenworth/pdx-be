using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
using PDX.BLL.Services.Interfaces; 
using PDX.DAL.Repositories; 
using PDX.Domain.Account; 

namespace PDX.BLL.Services {
    public class PermissionService:Service < RolePermission > , IPermissionService {
        private readonly IService < RolePermission > _rolePermissionService; 
        private readonly IUserService _userService;
        public PermissionService(IUnitOfWork unitOfWork, IService < RolePermission > rolePermissionService, IUserService userService):base(unitOfWork) {
            _rolePermissionService = rolePermissionService; 
            _userService = userService;
        }
        
        public async Task < IEnumerable < Permission >> GetPermissionByRoleAsync(int roleID) {
            var rolePermissions = await _rolePermissionService.FindByAsync(rp => rp.RoleID == roleID); 
            return rolePermissions.Select(rp => rp.Permission); 
        }

        public async Task<IEnumerable<Permission>> GetPermissionByUserAsync(int userID){
            var user = await _userService.GetAsync(userID);
            var roleIDs = user.Roles.Select(r => r.ID);
            var rolePermissions = await _rolePermissionService.FindByAsync(rp => roleIDs.Contains(rp.RoleID) && rp.IsActive); 
            var permissions = rolePermissions.Select( rp => rp.Permission).Distinct().ToList();
            return permissions;
        }

        public async Task < IEnumerable < Permission >> GetPermissionByRolesAsync(IEnumerable < int > roles) {
            List < Permission > permissions = new List < Permission > (); 

            foreach (int roleID in roles) {
                var rolePermissions = await _rolePermissionService.FindByAsync(rp => rp.RoleID == roleID && rp.IsActive); 
                permissions.AddRange(rolePermissions.Select(rp => rp.Permission).ToList()); 
            }
            return permissions; 
        }
    }
}