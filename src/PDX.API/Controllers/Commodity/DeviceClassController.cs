using Microsoft.AspNetCore.Mvc;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using PDX.Domain.Commodity;
using System.Linq;

namespace PDX.API.Controllers.Commodity
{
    [Authorize]
    [Route("api/[controller]")]
    public class DeviceClassController : BaseController<DeviceClass>
    {
        private readonly IService<DeviceClass> _service;
        private readonly IService<DeviceClassSubmodule> _deviceClassSubmoduleService;

        public DeviceClassController(IService<DeviceClass> service,IService<DeviceClassSubmodule> deviceClassSubmoduleService) : base(service)
        {
            _service  = service;
            _deviceClassSubmoduleService = deviceClassSubmoduleService;
        }

                 /// <summary>
        /// Get Product Type by its submodule type
        /// </summary>
        /// <param name="submoduleCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("BySubmoduleCode")]
        public async Task<IEnumerable<DeviceClass>> GetBySubmoduleCode ([FromQuery]string submoduleCode = null) {
            var result = (await _deviceClassSubmoduleService.FindByAsync (s => submoduleCode == null || (submoduleCode != null && s.Submodule.SubmoduleCode == submoduleCode)))?.Select(dc=>dc.DeviceClass);
            return result;
        }
    }
}