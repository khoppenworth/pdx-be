using Microsoft.AspNetCore.Mvc;
using PDX.Domain.Commodity;
using PDX.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PDX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PharmacopoeiaStandardController : CrudBaseController<PharmacopoeiaStandard>
    {
        private readonly IService<PharmacopoeiaStandard> _pharmacopoeiaStandardService;
        public PharmacopoeiaStandardController(IService<PharmacopoeiaStandard> pharmacopoeiaStandardService)
        : base(pharmacopoeiaStandardService)
        {
            _pharmacopoeiaStandardService = pharmacopoeiaStandardService;
        }     

    }
}