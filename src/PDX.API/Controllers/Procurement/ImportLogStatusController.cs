using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Procurement;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using PDX.BLL.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ImportLogStatusController : CrudBaseController<ImportPermitLogStatus>
    {
        private readonly IIpermitLogStatusService _service;
        public ImportLogStatusController(IIpermitLogStatusService importLogStatusService):base(importLogStatusService)
        {
            _service = importLogStatusService;
        }

          /// <summary>
        /// Get Import permit history
        /// </summary>
        /// /// <param name="ipermitId"></param>
        /// <returns></returns>
        [Route("IPermitHistory/{ipermitId}/{isAgent}")]
        [HttpGet]
         public async Task<IEnumerable<ImportPermitLogStatus>> GetIPermitStatusHistory(int ipermitId, bool isAgent)
        {
            var result = await _service.GetIPermitStatusHistory(ipermitId, isAgent);
            return result;
        }
    }
}