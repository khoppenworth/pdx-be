using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDX.BLL.Services.Interfaces;
using PDX.Domain.Commodity;
using PDX.Domain.Common;

namespace PDX.API.Controllers {
    //[Authorize]
    [Route ("api/[controller]")]
    public class ProductTypeController : CrudBaseController<ProductType> {
        private readonly IProductTypeService _productTypeService;
        private readonly IService<SubmoduleType> _submoduleTypeService;
        private readonly IService<Submodule> _submoduleService;
        public ProductTypeController (IProductTypeService productTypeService, IService<SubmoduleType> submoduleTypeService, IService<Submodule> submoduleService) : base (productTypeService) {
            _productTypeService = productTypeService;
            _submoduleTypeService = submoduleTypeService;
            _submoduleService = submoduleService;
        }
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>List of <typeparamref name="ProductType"/></returns>
        [HttpGet]
        public override async Task<IEnumerable<ProductType>> GetAllAsync () {
            var entities = await _productTypeService.FindByAsync (s => s.SubmoduleType.SubmoduleTypeCode == "MDCN" && s.IsActive);
            return entities;
        }

        /// <summary>
        /// Get Product Type by its submodule type
        /// </summary>
        /// <param name="submoduleTypeCode"></param>
        /// <param name="submoduleCode"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [Route ("BySubmoduleTypeCode")]
        public async Task<IEnumerable<ProductType>> GetBySubmoduleTypeCode ([FromQuery] string submoduleTypeCode = "MDCN", string submoduleCode = null) {
            var result = await _productTypeService.FindByAsync (s => s.SubmoduleType.SubmoduleTypeCode == submoduleTypeCode && ((submoduleCode == null || submoduleTypeCode == "MDCN") || (submoduleCode != null && s.Submodule.SubmoduleCode == submoduleCode)));
            return result;
        }

        /// <summary>
        /// create inn
        /// </summary>
        /// <param name="inn">inn</param>
        /// <returns>bool</returns>
        [HttpPost]
        public override async Task<bool> CreateAsync ([FromBody] ProductType productType) {
            var submoduleType = await _submoduleTypeService.GetAsync (sb => sb.SubmoduleTypeCode == productType.SubmoduleTypeCode);
            var submodule = await _submoduleService.GetAsync (sb => sb.SubmoduleCode == productType.SubmoduleCode);

            productType.SubmoduleTypeID = submoduleType.ID;
            productType.SubmoduleID = submodule.ID;
            var result = await _productTypeService.CreateAsync (productType);
            return result;
        }

    }
}