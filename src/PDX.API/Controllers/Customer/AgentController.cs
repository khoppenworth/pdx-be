using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PDX.BLL.Services.Interfaces;
using System.Collections.Generic;
using PDX.Domain.Customer;
using System.Web.Http;
using Microsoft.AspNetCore.Authorization;
using DataTables.AspNet.Core;
using PDX.Domain.Account;
using PDX.Domain.Views;
using PDX.Domain.Logging;
using PDX.API.Helpers;

namespace PDX.API.Controllers
{
    /// <summary>
    /// Agent Controller
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class AgentController : CrudBaseController<Agent>
    {
        private readonly IAgentService _service;
        private readonly IStatusLogService _statusLogService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentService"></param>
        public AgentController(IAgentService agentService, IStatusLogService statusLogService) : base(agentService)
        {
            _service = agentService;
            _statusLogService   = statusLogService;
        }

        /// <summary>
        /// Agent list for Datatable
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("List")]
        public async Task<BLL.Model.DataTablesResult<vwAgent>> GetAgentDT([FromBody]IDataTablesRequest request)
        {
            var result = await _service.GetAgentPage(request);
            return result;
        }

        /// <summary>
        /// Get Agents by User
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [Route("ByUser/{userID}")]
        [HttpGet]
        public async Task<List<Agent>> GetAgentByUser(int userID)
        {

            var result = await _service.GetAgentByUser(userID);
            return result;
        }

         /// <summary>
        /// Get Agents by User
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [Route("ByType/{code}")]
        [HttpGet]
        public async Task<List<Agent>> GetAgentByType(string code)
        {

            var result = await _service.GetAgentsByTypeCode(code);
            return result;
        }

        /// <summary>
        /// Get Agents who has User
        /// </summary>
        /// <returns></returns>
        [Route("AgentsForIPermit")]
        [HttpGet]
        public async Task<List<Agent>> GetAgentWithUser()
        {

            var result = await _service.GetAgentsForIpermit();
            return result;
        }
        /// <summary>
        /// Get Agents by Supplier
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        [Route("BySupplier/{supplierID}")]
        [HttpGet]
        public async Task<List<Agent>> GetAgentBySupplier(int supplierID)
        {

            var result = await _service.GetAgentBySupplier(supplierID);
            return result;
        }

        /// <summary>
        /// Get AgentSuppliers by Supplier
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        [Route("AgentSupplier/BySupplier/{supplierID}")]
        [HttpGet]
        public async Task<List<AgentSupplier>> GetAgentSupplierBySupplier(int supplierID)
        {

            var result = await _service.GetAgentSuppliersBySupplier(supplierID);
            return result;
        }

        /// <summary>
        /// Get AgentSuppliers by Agent
        /// </summary>
        /// <param name="agentID"></param>
        /// <returns></returns>
        [Route("AgentSupplier/ByAgent/{agentID}")]
        [HttpGet]
        public async Task<List<AgentSupplier>> GetAgentSupplierByAgent(int agentID)
        {

            var result = await _service.GetAgentSuppliersByAgent(agentID);
            return result;
        }

        /// <summary>
        /// Get AgentSupplier by Supplier and Agent 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <param name="agentID"></param>
        /// <returns></returns>
        [Route("AgentSupplier/{supplierID}/{agentID}")]
        [HttpGet]
        public async Task<AgentSupplier> GetAgentSupplierByAgent(int supplierID, int agentID)
        {

            var result = await _service.GetAgentSuppliersBySupplierAndAgent(supplierID, agentID);
            return result;
        }

        /// <summary>
        /// Insert new User Agent
        /// </summary>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateUserAgent")]
        public async Task<bool> CreateUserAgent([FromBody] UserAgent userAgent)
        {
            var result = await _service.CreateUserAgent(userAgent);
            return result;
        }

        /// <summary>
        /// Insert new User Agent
        /// </summary>
        /// <param name="agentSupplier"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AgentSupplier/InsertOrUpdate")]
        public async Task<bool> InsertOrUpdateAgentSupplier([FromBody] AgentSupplier agentSupplier)
        {
            var result = await _service.CreateOrUpdateAgentSupplier(agentSupplier, this.HttpContext.GetUserID());
            return result;
        }

        /// <summary>
        /// Get Agent History
        /// </summary>
        /// <param name="agentID"></param>
        /// <returns></returns>
        [Route("History/{agentID}")]
        [HttpGet]
        public async Task<IList<StatusLog>> GetHistory(int agentID)
        {
            var result = await _statusLogService.GetStatusLogByEntityID(nameof(Agent) ,agentID);
            return result;
        }

        /// <summary>
        /// Get AgentSupplier History
        /// </summary>
        /// <param name="agentSupplierID"></param>
        /// <returns></returns>
        [Route("AgentSupplier/History/{agentSupplierID}")]
        [HttpGet]
        public async Task<IList<StatusLog>> GetAgentSupplierHistory(int agentSupplierID)
        {
            var result = await _statusLogService.GetStatusLogByEntityID(nameof(AgentSupplier) ,agentSupplierID);
            return result;
        }
    }
}