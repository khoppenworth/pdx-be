using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDX.API.Helpers;
using PDX.BLL.Services.Interfaces;
using PDX.Domain.Common;

namespace PDX.API.Controllers.Common {
    [Route ("api/[controller]")]
    public class SubmoduleTypeController : CrudBaseController<SubmoduleType> {
        private readonly IService<SubmoduleType> _submoduleTypeService;
        private readonly IUserService _userService;
        public SubmoduleTypeController (IService<SubmoduleType> submoduleTypeService, IUserService userService) : base (submoduleTypeService) {
            _submoduleTypeService = submoduleTypeService;
            _userService = userService;
        }
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>List of <typeparamref name="T"/></returns>
        [HttpGet]
        public override async Task<IEnumerable<SubmoduleType>> GetAllAsync () {
            var entities = await _submoduleTypeService.FindByAsync (st => st.IsActive);
            return entities;
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>List of <typeparamref name="T"/></returns>
        [HttpGet]
        [Route ("ByUserPrivilage")]
        public async Task<IEnumerable<SubmoduleType>> GetAllUserSubmoduleTypeAsync () {
            var user = await _userService.GetAsync (us => us.ID == this.HttpContext.GetUserID ());
            var submoduleTypes = user.UserRoles.SelectMany(ur => ur.UserSubmoduleTypes.Select (us => us.SubmoduleType)).Where (st => st.IsActive);
            //var entities = await _submoduleTypeService.FindByAsync (st => st.IsActive);
            return submoduleTypes;
        }
    }
}