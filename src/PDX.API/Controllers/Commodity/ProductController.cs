using System.Collections.Generic;
using System.Threading.Tasks;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDX.API.Helpers;
using PDX.BLL.Model;
using PDX.BLL.Services.Interfaces;
using PDX.Domain.Commodity;
using PDX.Domain.Views;

namespace PDX.API.Controllers {
    // [Authorize]
    [Route ("api/[controller]")]
    public class ProductController : BaseController<Product> {
        private readonly IProductService _productService;
        public ProductController (IProductService productService) : base (productService) {
            _productService = productService;
        }

        /// <summary>
        /// Create product
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route ("CreateProduct")]
        [HttpPost]
        public async Task<ApiResponse> CreateProduct ([FromBody] ProductBusinessModel product) {
            var result = await _productService.CreateProduct (product);
            return result;
        }

        /// <summary>
        /// Get Product By Agent
        /// </summary>
        /// /// <param name="agentID"></param>
        /// <returns></returns>
        [Route ("ByAgent/{agentID}")]
        [HttpGet]
        public async Task<List<Product>> GetProductByAgent (int agentID) {
            var result = await _productService.GetProductByAgent (agentID);
            return result;
        }

        /// <summary>
        /// Get Product List by Agent
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route ("ByAgent/List/{submoduleTypeCode?}")]
        [HttpPost]
        public async Task<DataTablesResult<vwProduct>> GetProductByAgent ([FromBody] IDataTablesRequest request, string submoduleTypeCode=null) {
            int userID = this.HttpContext.GetUserID ();
            if (userID == 0) return null;
            var result = await _productService.GetProductByUserPage (request, userID, submoduleTypeCode);
            return result;
        }

        /// <summary>
        /// Get Product By Supplier
        /// </summary>
        /// /// <param name="supplierID"></param>
        /// <returns></returns>
        [Route ("BySupplier/{supplierID}")]
        [HttpGet]
        public async Task<List<Product>> GetProductSupplier (int supplierID) {
            var result = await _productService.GetSupplierProduct (supplierID);
            return result;
        }

        /// <summary>
        /// Get Product By Supplier and productTypeCode
        /// </summary>
        /// /// <param name="supplierID"></param>
        /// <returns></returns>
        [Route ("BySupplierForIpermit/{supplierID}/{productTypeCode}")]
        [HttpGet]
        public async Task<List<Product>> GetProductSupplierForIPermit (int supplierID, string productTypeCode = null) {
            var result = await _productService.GetSupplierProductForIPermit (supplierID, productTypeCode == "null" ? null : productTypeCode);
            return result;
        }

        /// <summary>
        /// Get MD-Product By Supplier and productTypeCode
        /// </summary>
        /// /// <param name="supplierID"></param>
        /// <returns></returns>
        [Route ("MDBySupplierForIpermit/{supplierID}/{productTypeCode}")]
        [HttpGet]
        public async Task<IEnumerable<MDModelSize>> GetMDProductSupplierForIPermit (int supplierID, string productTypeCode = null) {
            var result = await _productService.GetMDSupplierProductForIPermit (supplierID, productTypeCode == "null" ? null : productTypeCode);
            return result;
        }

        /// <summary>
        /// Search product
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route ("Search/{query}")]
        [HttpPost]
        public async Task<List<vwProduct>> SearchProduct (string query) {
            var result = await _productService.SearchProduct (query);
            return result;
        }

        /// <summary>
        /// Search product
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route ("SearchAll/{query}/{productTypeCode}/{importPermitTypeCode}/{supplierID?}")]
        [HttpPost]
        public async Task<List<vwAllProduct>> SearchAllProduct (string query, string productTypeCode, string importPermitTypeCode, int? supplierID=null) {
            var result = await _productService.SearchAllProduct (query, productTypeCode,importPermitTypeCode,supplierID);
            return result;
        }

    }
}