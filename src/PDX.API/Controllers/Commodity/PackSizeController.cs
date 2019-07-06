using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDX.BLL.Model;
using PDX.BLL.Services.Interfaces;
using PDX.Domain.Commodity;
using PDX.Domain.Common;

namespace PDX.API.Controllers {
    [Authorize]
    [Route ("api/[controller]")]
    public class PackSizeController : CrudBaseController<PackSize> {
        private readonly IService<PackSize> _packSizeService;
        private readonly IService<SubmoduleType> _submoduleTypeService;
        public PackSizeController (IService<PackSize> packSizeService, IService<SubmoduleType> submoduleTypeService) : base (packSizeService) {
            _packSizeService = packSizeService;
            _submoduleTypeService = submoduleTypeService;
        }
        /// <summary>
        /// Search entity
        /// </summary>
        /// <param name="searchRequest">The search request</param>
        /// <returns>bool</returns>
        [HttpPost]
        [Route ("Search")]
        public override async Task<DataTablesResult<PackSize>> SearchAsync ([FromBody] SearchRequest searchRequest) {
            var result = await _packSizeService.SearchAsync (searchRequest, true);
            return result;
        }

        /// <summary>
        /// create inn
        /// </summary>
        /// <param name="PackSize">PackSize</param>
        /// <returns>bool</returns>
        [HttpPost]
        public override async Task<bool> CreateAsync ([FromBody] PackSize packSize) {
            var submoduleType = await _submoduleTypeService.GetAsync (sb => sb.SubmoduleTypeCode == packSize.SubmoduleTypeCode);
            packSize.SubmoduleTypeID = submoduleType.ID;
            var result = await _packSizeService.CreateAsync (packSize);
            return result;
        }

    }
}