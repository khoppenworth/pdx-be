using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Customer;
using PDX.Domain.Views;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using DataTables.AspNet.Core;
using PDX.Domain.Logging;

namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class SupplierController: CrudBaseController<Supplier>
    {
        private readonly ISupplierService _supplierService;
        private readonly IStatusLogService _statusLogService;
        public SupplierController(ISupplierService supplierService, IStatusLogService statusLogService): base(supplierService)
        {
            _supplierService = supplierService;
            _statusLogService = statusLogService;
        }

        /// <summary>
        /// Supplier list for Datatable
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("List")]
        public async Task<BLL.Model.DataTablesResult<vwSupplier>> GetSupplierDT([FromBody]IDataTablesRequest request)
        {
            var result = await _supplierService.GetSupplierPage(request);
            return result;
        }

        /// <summary>
        /// get suppliers by agent
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ByAgent/{agentID}/{agentLevelCode}")]        
         public async Task<IEnumerable<Supplier>> GetSuppliersByAgent(int agentID, string agentLevelCode = null)
        {
            var result = await _supplierService.GetSuppliersByAgent(agentID, agentLevelCode=="null"?null:agentLevelCode);
            return result;
        }

        /// <summary>
        /// get suppliers by agent and productType
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ByAgent/{agentID}/{agentLevelCode}/{productTypeCode}")]        
         public async Task<IEnumerable<Supplier>> GetSuppliersByAgentAndProductType(int agentID, string agentLevelCode = null, string productTypeCode = null)
        {
            var result = await _supplierService.GetSuppliersByAgent(agentID, productTypeCode=="null"?null:productTypeCode,  agentLevelCode=="null"?null:agentLevelCode);
            return result;
        }

        /// <summary>
        /// Get Agent History
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        [Route("History/{supplierID}")]
        [HttpGet]
        public async Task<IList<StatusLog>> GetHistory(int supplierID)
        {
            var result = await _statusLogService.GetStatusLogByEntityID(nameof(Supplier) ,supplierID);
            return result;
        }

    }
}