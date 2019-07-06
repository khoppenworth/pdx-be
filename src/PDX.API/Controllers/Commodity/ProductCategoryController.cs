using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Commodity;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProductCategoryController : BaseController<ProductCategory>
    {
        private readonly IService<ProductCategory> _productCategoryService;
        public ProductCategoryController(IService<ProductCategory> productCategoryService)
        : base(productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }   

        /// <summary>
        /// Get Product Catagory by its submodule type
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("BySubmoduleTypeCode/{submoduleTypeCode?}")]
        public async Task<IEnumerable<ProductCategory>> GetBySubmoduleTypeCode (string submoduleTypeCode = "MDCN") {
            var result = await _productCategoryService.FindByAsync (s => s.SubmoduleType.SubmoduleTypeCode == submoduleTypeCode);
            return result;
        }  

    }
}