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
    public class AccessoryTypeController : CrudBaseController<AccessoryType>
    {
        public AccessoryTypeController(IService<AccessoryType> service) : base(service)
        {
        }
    }
}