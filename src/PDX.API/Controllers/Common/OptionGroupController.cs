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
    public class OptionGroupController:BaseController<OptionGroup>
    {
        private readonly IService<OptionGroup> _optionGroupService;
        public OptionGroupController(IService<OptionGroup> optionGroupService)
        :base(optionGroupService)
        {
            _optionGroupService = optionGroupService;
        }
 
    }
}