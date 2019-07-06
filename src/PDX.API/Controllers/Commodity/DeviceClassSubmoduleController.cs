using Microsoft.AspNetCore.Mvc;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using PDX.Domain.Commodity;

namespace PDX.API.Controllers.Commodity
{
    [Authorize]
    [Route("api/[controller]")]
    public class DeviceClassSubmoduleController : BaseController<DeviceClassSubmodule>
    {
        private readonly IService<DeviceClassSubmodule> _service;
        public DeviceClassSubmoduleController(IService<DeviceClassSubmodule> service) : base(service)
        {
            _service = service;
        }
    }
}