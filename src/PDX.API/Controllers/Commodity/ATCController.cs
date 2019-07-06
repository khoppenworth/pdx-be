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
    public class ATCController : BaseController<ATC>
    {
        private readonly IService<ATC> _atcService;
        public ATCController(IService<ATC> atcService)
        : base(atcService)
        {
            _atcService = atcService;
        }     

    }
}