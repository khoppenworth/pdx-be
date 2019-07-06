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
    public class TherapeuticGroupController : BaseController<TherapeuticGroup>
    {
        public TherapeuticGroupController(IService<TherapeuticGroup> therapeuticGroupService)
        : base(therapeuticGroupService)
        {
            
        }     

    }
}