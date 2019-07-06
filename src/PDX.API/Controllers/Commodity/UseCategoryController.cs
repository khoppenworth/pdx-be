using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDX.BLL.Services.Interfaces;
using PDX.Domain.Commodity;

namespace PDX.API.Controllers {
    [Authorize]
    [Route ("api/[controller]")]
    public class UseCategoryController : BaseController<UseCategory> {
        private readonly IService<UseCategory> _useCatagoryService;
        public UseCategoryController (IService<UseCategory> useCatagoryService) : base (useCatagoryService) {
            _useCatagoryService = useCatagoryService;
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>List of <typeparamref name="T"/></returns>
        [HttpGet]
        public override async Task<IEnumerable<UseCategory>> GetAllAsync () {
            var entities = await _useCatagoryService.FindByAsync (s => s.SubmoduleType.SubmoduleTypeCode == "MDCN" && s.IsActive);
            return entities;
        }


        /// <summary>
        /// Get Use Category by its submodule type
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("BySubmoduleTypeCode")]
        public async Task<IEnumerable<UseCategory>> GetBySubmoduleTypeCode ([FromQuery]string submoduleTypeCode = "MDCN") {
            var result = await _useCatagoryService.FindByAsync (s => s.SubmoduleType.SubmoduleTypeCode == submoduleTypeCode);
            return result;
        }

    }
}