using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Account;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using PDX.BLL.Services;
using System.Linq;

namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class RolesController : BaseController<Role>
    {

        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;

        /// <summary>
        /// roles controller
        /// </summary>
        /// <param name="roleService"></param>
        /// <param name="permissionService"></param>
        public RolesController(IRoleService roleService, IPermissionService permissionService) : base(roleService)
        {
            _roleService = roleService;
            _permissionService = permissionService;
        }

        /// <summary>
        /// Get All roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public override async Task<IEnumerable<Role>> GetAllAsync()
        {
            var entities = await _roleService.GetAllAsync(true);
            return entities;
        }

        /// <summary>
        /// update exising role
        /// </summary>
        /// <param name="role">The role</param>
        /// <returns>bool</returns>
        [HttpPut]
        public async Task<bool> UpdateAsync([FromBody]Role role)
        {
            var result = await _roleService.UpdateAsync(role);
            return result;
        }

        /// <summary>
        /// Create Role Permission
        /// </summary>
        /// <param name="rolePermission">The role</param>
        /// <returns>bool</returns>
        [HttpPost]
        [Route("CreateRolePermission")]
        public async Task<bool> CreateRolePermission([FromBody]IList<RolePermission> rolePermission)
        {
            var result = await _permissionService.CreateOrUpdateAsync(rolePermission);
            return result;
        }

        /// <summary>
        /// Get Role Permissions
        /// </summary>
        /// <param name="roleId">The role</param>
        /// <returns>bool</returns>
        [HttpGet]
        [Route("Permissions/{roleId}")]
        public async Task<List<Permission>> GetRolePermission(int roleId)
        {
            var result = await _permissionService.GetPermissionByRoleAsync(roleId);
            return result.ToList();
        }
    }
}
