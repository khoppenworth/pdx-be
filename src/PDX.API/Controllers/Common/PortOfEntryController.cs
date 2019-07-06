using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Common;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PortOfEntryController:BaseController<PortOfEntry>
    {
        private readonly IPortOfEntryService _service;
        public PortOfEntryController(IPortOfEntryService portOfEntryService)
        :base(portOfEntryService)
        {
            _service = portOfEntryService;
        }

        /// <summary>
        /// Get shipping methods ports
        /// </summary>
        /// /// <param name="shippingID"></param>
        /// <returns></returns>
        [Route("ByShippment/{shippingID}")]
        [HttpGet]
        public async Task<IEnumerable<PortOfEntry>> GetShippingPortOfEntry(int shippingID)
        {
            var result = await _service.GetShippingPortOfEntry(shippingID);
            return result;
        }

        
    }
}