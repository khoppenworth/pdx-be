using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PDX.BLL.Services.Interfaces;
using PDX.API.Model;
using PDX.API.Middlewares.Token;
using Microsoft.Extensions.Options;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic; 
using PDX.BLL.Services.Interfaces.Email;
using DataTables.AspNet.Core;
using PDX.Domain.Views;
using PDX.Domain.Commodity;
using PDX.Domain.Customer;

namespace PDX.API.Controllers
{
    /// <summary>
    /// Public Controller
    /// </summary>
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class PublicController : Controller
    {
        private readonly IAgentService _agentService;
        private readonly IProductService _productService;

        /// <summary>
        /// Constructor for Public controller
        /// </summary>
        /// <param name="agentService"></param>
        /// <param name="productService"></param>
        public PublicController(IAgentService agentService, IProductService productService)
        {
            _agentService = agentService;
            _productService = productService;
        }

        /// <summary>
        /// Agent list for Datatable
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Agent/List")]
        public async Task<BLL.Model.DataTablesResult<vwAgent>> GetAgentDT([FromBody]IDataTablesRequest request)
        {
            var result = await _agentService.GetAgentPage(request);
            return result;
        }

        /// <summary>
        /// Get single agent by id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        [Route("Agent/{id}")]
        public async Task<Agent> GetAgentAsync(int id)
        {
            var result = await _agentService.GetAsync(id);
            return result;
        }

        [HttpGet]
        [Route("Agent/ByProduct/{productID}")]
        public async Task<IEnumerable<AgentSupplier>> GetAgentsByProductAsync(int productID){
            var result  = await _productService.GetAgentSupplierByProduct(productID);
            return result;
        }

        /// <summary>
        /// Product list for Datatable
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Product/List/{submoduleTypeCode?}")]
        public async Task<BLL.Model.DataTablesResult<vwProduct>> GetProductDT([FromBody]IDataTablesRequest request, string submoduleTypeCode=null)
        {
            var result = await _productService.GetProductPage(request, submoduleTypeCode);
            return result;
        }

        /// <summary>
        /// Get single product by id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        [Route("Product/{id}")]
        public virtual async Task<Product> GetProductAsync(int id)
        {
            var result = await _productService.GetAsync(id);
            return result;
        }

        /// <summary>
        /// Get Product By Agent
        /// </summary>
        /// /// <param name="agentID"></param>
        /// <returns></returns>
        [Route("Product/ByAgent/{agentID}")]
        [HttpGet]
        public async Task<List<Product>> GetProductByAgent(int agentID)
        {
            var result = await _productService.GetProductByAgent(agentID);
            return result;
        }

        /// <summary>
        /// Get Product By Supplier
        /// </summary>
        /// /// <param name="supplierID"></param>
        /// <returns></returns>
        [Route("Product/BySupplier/{supplierID}")]
        [HttpGet]
        public async Task<List<Product>> GetProductSupplier(int supplierID)
        {
            var result = await _productService.GetSupplierProduct(supplierID);
            return result;
        }

        /// <summary>
        /// Get product documents
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        [Route("Product/Documents/{id}")]
        public virtual async Task<IList<Domain.Document.Document>> GetProductDocuments(int id)
        {
            var result = await _productService.GetProductDocuments(id);
            return result;
        }

    }
}