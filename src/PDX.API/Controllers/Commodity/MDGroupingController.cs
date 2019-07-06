using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Commodity;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PDX.API.Controllers.Commodity
{
    [Authorize]
    [Route("api/[controller]")]
    public class MDGroupingController: CrudBaseController<MDGrouping>
    {
         public MDGroupingController(IService<MDGrouping> service) : base(service)
        {
        }
    }
}