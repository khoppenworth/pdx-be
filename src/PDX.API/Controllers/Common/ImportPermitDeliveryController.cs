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
    public class ImportPermitDeliveryController : CrudBaseController<ImportPermitDelivery>
    {
        private readonly IImportPermitDeliveryService _service;
        public ImportPermitDeliveryController(IImportPermitDeliveryService importPermitDeliveryService)
        :base(importPermitDeliveryService)
        {
            _service = importPermitDeliveryService;
        }

         /// <summary>
        /// Get delivery ordered by name
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public override async Task<IEnumerable<ImportPermitDelivery>> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return result;
        }
    }
}
