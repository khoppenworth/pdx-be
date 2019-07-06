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
    public class AgeGroupController : BaseController<AgeGroup>
    {
        private readonly IService<AgeGroup> _ageGroupService;
        public AgeGroupController(IService<AgeGroup> ageGroupService)
        : base(ageGroupService)
        {
            _ageGroupService = ageGroupService;
        }     

    }
}